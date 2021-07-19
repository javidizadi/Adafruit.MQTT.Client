using Adafruit.MQTT.Client.Models.UnSubscribing;
using MQTTnet.Client.Unsubscribing;
using System.Collections.Generic;

namespace Adafruit.MQTT.Client.Models.UnSubscribing
{
    public class UnSubscribeResult : MqttClientUnsubscribeResult
    {
        public List<UnSubscribingResultItem> items { get; private set; }

        public UnSubscribeResult(MqttClientUnsubscribeResult result)
        {
            foreach (var item in result.Items)
            {
                items.Add(new UnSubscribingResultItem(item));
            }
        }
    }
}
