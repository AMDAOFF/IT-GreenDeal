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
        private MqttClient client;
        private IOTSettings _settings;

        Func<double, int, Task> method;

        public async Task Initialize(IOTSettings settings, Func<double, int, Task> callback)
        {
            _settings = settings;
            method = callback;

            client = new MqttClient(_settings.Server);
            string clientId = Guid.NewGuid().ToString();
            client.Connect(clientId, _settings.UserName, _settings.Password);
        }

        public void Publish(string topic, string message)
        {
            client.Publish(topic, Encoding.UTF8.GetBytes(message), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
        }

        public void Subscribe(string topic)
        {
            client.MqttMsgPublishReceived += MessageReceived;

            client.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
        }

        public void SendConfig(ConfigMessage config)
        {
            string message = $"#{Convert.ToInt32(config.VentilationFan)}{Convert.ToInt32(config.RecyclingFan)}{Convert.ToInt32(config.VentilationValveStatus)}{Convert.ToInt32(config.Radiator)}00#";

            Publish(_settings.PubTopic + "/" + config.Id.ToString(), message);
        }

        private async void MessageReceived(object sender, MqttMsgPublishEventArgs e)
        {
            int id = int.Parse(e.Topic.Split('/').ToList().Last());

            string rawStr = Encoding.Default.GetString(e.Message).Split('=').ToList().Last();
            int startindex = 0;
            int Endindex = rawStr.IndexOf('#');

            double temperature = double.Parse(rawStr.Substring(startindex, Endindex - startindex - 1).Trim());

            await method(temperature, id);
        }
    }
}
