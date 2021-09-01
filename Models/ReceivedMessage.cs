using MQTTnet;
using MQTTnet.Protocol;

using System;
using System.Text;

namespace Adafruit.MQTT.Client.Models
{
    public class ReceivedMessageEventArgs : EventArgs
    {
        public string ClientId { get; }
        public bool ProcessingFailed { get; set; }
        public bool IsHandled { get; set; }
        public MqttApplicationMessageReceivedReasonCode ReasonCode { get; set; } = MqttApplicationMessageReceivedReasonCode.Success;


        public string Topic { get; set; }
        public byte[] Payload { get; set; }
        public MqttQualityOfServiceLevel QualityOfServiceLevel { get; set; }


        public string FeedKey { get; set; }
        public string ReceivedValue { get; set; }


        public ReceivedMessageEventArgs(MqttApplicationMessageReceivedEventArgs eventArgs)
        {
            ClientId = eventArgs.ClientId;
            ProcessingFailed = eventArgs.ProcessingFailed;
            IsHandled = eventArgs.IsHandled;
            ReasonCode = eventArgs.ReasonCode;

            Topic = eventArgs.ApplicationMessage.Topic;
            Payload = eventArgs.ApplicationMessage.Payload;
            QualityOfServiceLevel = eventArgs.ApplicationMessage.QualityOfServiceLevel;

            FeedKey = GetFeedKeyFromTopic(Topic);
            ReceivedValue = Encoding.UTF8.GetString(Payload);
        }

        private string GetFeedKeyFromTopic(string topic)
        {
            return Topic.Split('/')[2];
        }

    }
}