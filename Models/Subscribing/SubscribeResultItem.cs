using MQTTnet.Client.Subscribing;

namespace Adafruit.MQTT.Client.Models.Subscribing
{
    public class SubscribeResultItem
    {
        public string Topic { get; private set; }

        public MqttClientSubscribeResultCode ResultCode { get; }

        public SubscribeResultItem(MqttClientSubscribeResultItem result)
        {
            Topic = result.TopicFilter.Topic;
            ResultCode = result.ResultCode;
        }
    }

}
