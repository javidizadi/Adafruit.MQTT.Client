using System.Collections.Generic;
using MQTTnet.Client.Publishing;

namespace Adafruit.MQTT.Client.Models.Publishing
{
    public class PublishResult
    {
        public string ReasonString { get; private set; }

        public MqttClientPublishReasonCode ReasonCode { get; private set; }

        public ushort? PacketIdentifier { get; private set; }

        public List<UserProperty> UserProperties { get; set; }


        public PublishResult(MqttClientPublishResult result)
        {
            UserProperties = new List<UserProperty>();

            ReasonString = result.ReasonString;

            ReasonCode = result.ReasonCode;

            PacketIdentifier = result.PacketIdentifier;

            if (result.UserProperties != null)
            {

                foreach (var Property in result.UserProperties)
                {
                    UserProperties.Add(new UserProperty
                    {
                        Name = Property.Name,
                        Value = Property.Value
                    });
                }
            }
        }
    }
}
