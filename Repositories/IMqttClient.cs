using Adafruit_MQTT_Client.Models;
using MQTTnet;
using MQTTnet.Client.Receiving;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Adafruit_MQTT_Client.Repositories
{
    public interface IMqttClient
    {
        string ClientId { get; }

        IMqttApplicationMessageReceivedHandler MessageReceivedHandler { get; set; }

        Task ConnectAsync();

        Task ConnectAsync(CancellationToken cancellationToken);

        Task DisconnectAsync();

        Task DisconnectAsync(CancellationToken cancellationToken);

        void InitClient(ConnectionMode connectionMode, bool secureConnection);

        MQTTnet.Client.IMqttClient OnMessageReceived(Func<MqttApplicationMessageReceivedEventArgs, Task> handler);

        Task<PublishResult> PublishFeedAsync(string feedKey, string value);

        Task<PublishResult> PublishTopicAsync(string topic, string value);

        Task<SubscribeResult> SubscribeFeedAsync(string feedKey);

        Task<SubscribeResult> SubscribeTopicAsync(string topic);

        Task<UnSubscribeResult> UnSubscribeFeedAsync(params string[] feedKeys);

        Task<UnSubscribeResult> UnsubscribeTopicAsync(params string[] topics);
    }
}