using Adafruit.MQTT.Client.Models;
using MQTTnet;
using MQTTnet.Client.Receiving;
using System;
using System.Threading;
using System.Threading.Tasks;

using Adafruit.MQTT.Client.Models.Publishing;
using Adafruit.MQTT.Client.Models.Subscribing;
using Adafruit.MQTT.Client.Models.UnSubscribing;

namespace Adafruit.MQTT.Client.Repositories
{
    public interface IMqttClient
    {
        string ClientId { get; }
        IMqttApplicationMessageReceivedHandler MessageReceivedHandler { get; set; }

        #region Connecting
        Task ConnectAsync();
        Task ConnectAsync(CancellationToken cancellationToken);
        #endregion
        #region Disconnecting

        Task DisconnectAsync();
        Task DisconnectAsync(CancellationToken cancellationToken);
        #endregion
        void InitClient(ConnectionMode connectionMode, bool secureConnection);
        #region Publishing
        Task<PublishResult> PublishFeedAsync(string feedKey, string value);
        Task<PublishResult> PublishTopicAsync(string topic, string value);
        #endregion
        #region Subscribing
        Task<SubscribeResult> SubscribeFeedAsync(string feedKey);
        Task<SubscribeResult> SubscribeTopicAsync(string topic);
        #endregion
        #region Unsubscribing
        Task<UnSubscribeResult> UnSubscribeFeedAsync(params string[] feedKeys);
        Task<UnSubscribeResult> UnsubscribeTopicAsync(params string[] topics);
        #endregion
        #region OnConnected
        public delegate void ConnectedDelegate(MQTTnet.MqttApplicationMessageReceivedEventArgs eventArgs);
        public event ConnectedDelegate OnConnected;
        #endregion
        #region OnMessageReceived
        public delegate void MessageReceivedDelegate(MQTTnet.MqttApplicationMessageReceivedEventArgs eventArgs);
        public event MessageReceivedDelegate OnMessageReceived;
        #endregion
        #region OnDisconnected
        public delegate void DisconnectedDelegate(MQTTnet.MqttApplicationMessageReceivedEventArgs eventArgs);
        public event DisconnectedDelegate OnDisconnected;
        #endregion

    }
}