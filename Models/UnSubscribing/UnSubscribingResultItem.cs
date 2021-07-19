using System;
using System.Collections.Generic;
using System.Text;

namespace Adafruit.MQTT.Client.Models.UnSubscribing
{
    public class UnSubscribingResultItem
    {
        public string Topic { get; private set; }

        public MQTTnet.Client.Unsubscribing.MqttClientUnsubscribeResultCode ResultCode { get; private set; }

        public UnSubscribingResultItem(MQTTnet.Client.Unsubscribing.MqttClientUnsubscribeResultItem result)
        {
            Topic = result.TopicFilter;

            ResultCode = result.ReasonCode;
        }
    }
}
