using MQTTnet.Client.Unsubscribing;

using System.Collections.Generic;

namespace Adafruit.MQTT.Client.Models.UnSubscribing
{
    public class UnSubscribeResult : MqttClientUnsubscribeResult
    {
        public List<UnSubscribingResultItem> items { get; }

        public UnSubscribeResult(MqttClientUnsubscribeResult result)
        {
            items = new List<UnSubscribingResultItem>();

            foreach (var item in result.Items)
            {
                items.Add(new UnSubscribingResultItem(item));
            }
        }
    }
}
