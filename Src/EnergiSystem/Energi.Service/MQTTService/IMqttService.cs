using System;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Energi.Service.MQTTService
{
    public interface IMqttService
    {
        Task Initialize(IOTSettings settings, Func<double, int, Task> callbackDouble, Func<string, int, Task> callbackStr);
        void Publish(string topic, string message);
        void Subscribe(string topic);
        Task SendConfig(ConfigMessage config);        
    }
}