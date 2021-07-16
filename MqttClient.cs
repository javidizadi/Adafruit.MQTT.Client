using System;
using System.Collections.Generic;
using System.Text;

namespace Adafruit_MQTT_Client
{
    public class MqttClient
    {
        private string _key;

        private string _userName;

        private string _clientId;

        private const string _hostAddress = "io.adafruit.com";

        private readonly TimeSpan _keepAlivePeriod = TimeSpan.FromSeconds(60);


        public MqttClient(string userName, string key, string clientId = null)
        {
            _userName = userName;
            _key = key;

            if (string.IsNullOrEmpty(clientId))
            {
                _clientId = Guid.NewGuid().ToString();
            }
            else if (!string.IsNullOrWhiteSpace(clientId))
            {
                _clientId = clientId;
            }

        }

        private string GetTopicNameFromFeedKey(string feedKey)
        {
            return $"{_userName}/feeds/{feedKey}";
        }

    }
}
