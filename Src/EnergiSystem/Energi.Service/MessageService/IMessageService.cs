using Energi.Service.MessageService.DTO;
using System.Threading.Tasks;

namespace Energi.Service.MessageService
{
    public interface IMessageService
    {
        Task SendMessage(PublishMessageDTO message);        
        Task Initialize();
        Task StopListener();
    }
}