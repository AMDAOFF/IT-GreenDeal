using uPLibrary.Networking.M2Mqtt.Messages;

namespace Energi.Service.MQTTService
{
    public interface IMqttService
    {
        void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e);
        void Publish(string message);
        bool Subscribe();
    }
}