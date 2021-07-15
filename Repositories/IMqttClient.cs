using System;
using System.Collections.Generic;
using System.Text;

namespace Adafruit_MQTT_Client.Repositories
{
    public interface IMqttClient
    {
        public void ConnectAsync();

        public void Disconnect();

        public void Publish();

        public void Subscribe();

        public void UnSubscribe();
    }
}
