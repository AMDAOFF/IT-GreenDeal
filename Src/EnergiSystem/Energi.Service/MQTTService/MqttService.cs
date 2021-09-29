using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Energi.Service.MQTTService
{
    public class MqttService : IMqttService
    {

        private static string name = "WebApplication";
        private static string Server = "127.0.0.1";
        private static string Username = "guest";
        private static string Password = "guest";
        private static string pubTopic = "device/settings";

        private MqttClient client;

        public MqttService()
        {
            client = new MqttClient(Server);
            string clientId = Guid.NewGuid().ToString();
            client.Connect(clientId, Username, Password);
        }

        public void Publish(string message)
        {
            client.Publish(pubTopic, Encoding.UTF8.GetBytes(message), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
        }

        public bool Subscribe()
        {
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

            client.Subscribe(new string[] { pubTopic }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });

            return true;
        }

        public void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string holder = Encoding.Default.GetString(e.Message);
        }
    }
}
