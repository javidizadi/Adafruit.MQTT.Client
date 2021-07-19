using System.Collections.Generic;
using MQTTnet.Client.Subscribing;


namespace Adafruit.MQTT.Client.Models.Subscribing
{
    public class SubscribeResult
    {
        public List<SubscribeResultItem> SubscribeResults { get; private set; }

        public SubscribeResult(MqttClientSubscribeResult result)
        {
            foreach (var item in result.Items)
            {
                SubscribeResults.Add(new SubscribeResultItem(item));
            }
        }
    }

}
