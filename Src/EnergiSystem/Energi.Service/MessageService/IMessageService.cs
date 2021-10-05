using Energi.Service.MessageService.DTO;
using MassTransit;
using System;
using System.Threading.Tasks;
using static MessageBroker.Contracts.Contracts;

namespace Energi.Service.MessageService
{
    public interface IMessageService
    {
        Task SendMessage(PublishMessageDTO message);        
        Task Initialize(MessageBusSettings settings, Func<RoomUpdate, Task> callback);
        Task StopListener();
    }
}
