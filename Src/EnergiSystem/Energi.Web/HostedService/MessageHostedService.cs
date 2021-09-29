using Energi.Service.DeviceService;
using Energi.Service.DeviceService.DTO;
using Energi.Service.HeatingService;
using Energi.Service.MessageService;
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
        private readonly MessageBusSettings _settings;        

        public MessageHostedService(IHubContext<DeviceHub> deviceHub, IMessageService messageService, IConfiguration Configuration, IDeviceService deviceService, IHeatingService heatingService)
        {
            _settings = Configuration.GetSection(nameof(MessageBusSettings)).Get<MessageBusSettings>();
            _deviceHub = deviceHub;
            _messageService = messageService;
            _deviceService = deviceService;
            _heatingService = heatingService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _messageService.Initialize(_settings, Consume);
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

                await _deviceService.UpdateClasseRoom(calssInfo);

                // Heat controll.
                List<StatusDeviceDTO> deviceList = await _heatingService.HeadControl(device);

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
    }
}
