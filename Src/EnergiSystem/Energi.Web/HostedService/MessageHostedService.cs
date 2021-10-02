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
        private readonly IOTSettings _IotSettings;

        public MessageHostedService(IHubContext<DeviceHub> deviceHub, IMessageService messageService, IConfiguration Configuration, IDeviceService deviceService, IHeatingService heatingService, IMqttService mqttService)
        {
            _busSettings = Configuration.GetSection(nameof(MessageBusSettings)).Get<MessageBusSettings>();
            _IotSettings = Configuration.GetSection(nameof(IOTSettings)).Get<IOTSettings>();
            _deviceHub = deviceHub;
            _messageService = messageService;
            _deviceService = deviceService;
            _heatingService = heatingService;
            _mqttService = mqttService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _messageService.Initialize(_busSettings, Consume);
            await _mqttService.Initialize(_IotSettings, IOTMessageReceived, IOTMessageReceived);
            _mqttService.Subscribe(_IotSettings.SubTopic);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _messageService.StopListener();

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

            // Update device.
            device.OnlineStatus = true;
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
