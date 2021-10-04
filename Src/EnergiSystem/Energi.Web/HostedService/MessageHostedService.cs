using Energi.Service.DeviceService;
using Energi.Service.DeviceService.DTO;
using Energi.Service.HeatingService;
using Energi.Service.MessageService;
using Energi.Service.MQTTService;
using Energi.Web.Hubs;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static MessageBroker.Contracts.Contracts;

namespace Energi.Web.HostedService
{
    public class MessageHostedService : IHostedService, IConsumer<MessageAsString>
    {
        private readonly IHubContext<DeviceHub> _deviceHub;
        private readonly IMessageService _messageService;
        private readonly IDeviceService _deviceService;
        private readonly IHeatingService _heatingService;
        private readonly IMqttService _mqttService;
        private readonly MessageBusSettings _busSettings;
        private readonly IOTSettings _iotSettings;
        private Timer timer;
        private List<StatusDeviceDTO> onlineDevices;

        public MessageHostedService(IHubContext<DeviceHub> deviceHub, IMessageService messageService, IConfiguration Configuration, IDeviceService deviceService, IHeatingService heatingService, IMqttService mqttService)
        {
            _busSettings = Configuration.GetSection(nameof(MessageBusSettings)).Get<MessageBusSettings>();
            _iotSettings = Configuration.GetSection(nameof(IOTSettings)).Get<IOTSettings>();
            _deviceHub = deviceHub;
            _messageService = messageService;
            _deviceService = deviceService;
            _heatingService = heatingService;
            _mqttService = mqttService;
            onlineDevices = new List<StatusDeviceDTO>();

        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _messageService.Initialize(_busSettings, Consume);
            await _mqttService.Initialize(_iotSettings, IOTMessageReceived, IOTMessageReceived);
            _mqttService.Subscribe(_iotSettings.SubTopic);

            timer = new Timer(Tick, null, 0, 3000);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            foreach (var device in onlineDevices)
            {
                device.OnlineStatus = false;
                _deviceService.UpdateDevice(device);
            }

            await _messageService.StopListener();
        }

        // This is a device online check. This must be done through the lastwill message in the MQTT protocol in the future.
        private void Tick(object state)
        {
            bool deviceChange = false;
            List<StatusDeviceDTO> deviceUpdate = new List<StatusDeviceDTO>();

            foreach (var device in onlineDevices)
            {
                if (device.OnlinePing < DateTime.Now)
                {
                    device.OnlineStatus = false;
                    deviceUpdate.Add(device);                    
                    _deviceService.UpdateDevice(device);
                    deviceChange = true;
                }
            }

            if (deviceChange)
            {
                onlineDevices.RemoveAll(x => x.OnlineStatus == false);
                _deviceHub.Clients.All.SendAsync("UpdateDevice", deviceUpdate);
            }
        }

        public async Task Consume(ConsumeContext<MessageAsString> context)
        {
            int deviceId;

            // Cut string and create object.
            string message = context.Message.ToString();
            int startindex = message.IndexOf('=');
            int Endindex = message.IndexOf('}');
            string outputstring = message.Substring(startindex + 1, Endindex - startindex - 1).Trim();
            List<string> stringList = outputstring.Split(';').ToList();

            // A simple check, this data can still be invalid.
            if (stringList.Count == 3)
            {
                // Get and update device.
                ClassInfoDTO calssInfo = new ClassInfoDTO() { Classroom = stringList[0], PeopleCount = int.Parse(stringList[1]), TimeStamp = DateTime.Parse(stringList[2]) };
                StatusDeviceDTO device = await _deviceService.GetDeviceByClassNumber(calssInfo.Classroom);

                if (device == null)
                {
                    return;
                }

                calssInfo.Id = device.Id;
                device.PeopleCount = calssInfo.PeopleCount;
                await _deviceService.UpdateClasseRoom(calssInfo);

                // Heat control.
                List<StatusDeviceDTO> deviceList = await _heatingService.HeadControl(device, _mqttService);

                // Send to browser.
                if (deviceList == null)
                {
                    return;
                }
                _deviceHub.Clients.All.SendAsync("UpdateDevice", deviceList);
            }
            else
            {
                return;
            }

            Console.WriteLine("Classroom update: {0}", context.Message);
        }

        public async Task IOTMessageReceived(double temperature, int id)
        {
            StatusDeviceDTO device = await _deviceService.GetDeviceById(id);

            // Keep online status.
            if (onlineDevices.Where(x => x.Id == device.Id).FirstOrDefault() != null)
            {
                onlineDevices.Where(x => x.Id == device.Id).FirstOrDefault().OnlinePing = DateTime.Now + new TimeSpan(0, 0, 5);
            }
   
            device.Temperature = temperature;

            _deviceService.UpdateDevice(device);

            List<StatusDeviceDTO> deviceList = new List<StatusDeviceDTO>();
            deviceList.Add(device);

            _deviceHub.Clients.All.SendAsync("UpdateDevice", deviceList);

            return;

        }

        public async Task IOTMessageReceived(string Message, int id)
        {
            StatusDeviceDTO device = await _deviceService.GetDeviceById(id);

            // Add to online status list
            device.OnlineStatus = true;
            device.OnlinePing = DateTime.Now + new TimeSpan(0, 0, 5);
            onlineDevices.Add(device);          

            // Update device.
            _deviceService.UpdateDevice(device);

            _heatingService.SendLastConfig(_mqttService, device.Id);

            // Update browser
            List<StatusDeviceDTO> deviceList = new List<StatusDeviceDTO>();
            deviceList.Add(device);
            _deviceHub.Clients.All.SendAsync("UpdateDevice", deviceList);

            return;
        }
    }
}
