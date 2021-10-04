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
        private bool _configReceived;

        Func<double, int, Task> methodAsDouble;
        Func<string, int, Task> methodAsString;

        public async Task Initialize(IOTSettings settings, Func<double, int, Task> callbackDouble, Func<string, int, Task> callbackStr)
        {
            _settings = settings;
            methodAsDouble = callbackDouble;
            methodAsString = callbackStr;

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

        public async Task SendConfig(ConfigMessage config)
        {
            string message = $"#{Convert.ToInt32(config.VentilationFan)}{Convert.ToInt32(config.RecyclingFan)}{Convert.ToInt32(config.VentilationValveStatus)}{Convert.ToInt32(config.Radiator)}00#";

            // Wait for config file is received, or try again.
            int times = 0;
            while (times < 5)
            {
                for (int i = 0; i < 10; i++)
                {
                    Publish(_settings.PubTopic + "/" + config.Id.ToString(), message);
                    await Task.Delay(500);
                }

                if (_configReceived)
                {
                    break;
                }
                else
                {
                    times++;
                }                
            }            
        }

        private async void MessageReceived(object sender, MqttMsgPublishEventArgs e)
        {           
            int id = int.Parse(e.Topic.Split('/').ToList().Last());

            string rawStr = Encoding.Default.GetString(e.Message); 

            if (rawStr.Contains("Temp"))
            {
                string message = rawStr.Split('=').ToList().Last();

                int startindex = 0;
                int Endindex = message.IndexOf('#');

                double temperature = Convert.ToDouble(message.Substring(startindex, Endindex).Replace(".", ","));

                await methodAsDouble(temperature, id);
            }
            else if(rawStr.Contains("Online"))
            {
                await methodAsString(Encoding.Default.GetString(e.Message), id);
            }
            else if (rawStr.Contains("Config"))
            {
                _configReceived = true;
            }
        }
    }
}
