﻿using Adafruit.MQTT.Client.Models;
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

        Task ConnectAsync();

        Task ConnectAsync(CancellationToken cancellationToken);

        Task DisconnectAsync();

        Task DisconnectAsync(CancellationToken cancellationToken);

        void InitClient(ConnectionMode connectionMode, bool secureConnection);

        MQTTnet.Client.IMqttClient OnConnected(Func<MQTTnet.Client.Connecting.MqttClientConnectedEventArgs, Task> handler);

        MQTTnet.Client.IMqttClient OnMessageReceived(Func<MqttApplicationMessageReceivedEventArgs, Task> handler);

        MQTTnet.Client.IMqttClient OnDisconnected(Func<MQTTnet.Client.Disconnecting.MqttClientDisconnectedEventArgs, Task> handler);

        Task<PublishResult> PublishFeedAsync(string feedKey, string value);

        Task<PublishResult> PublishTopicAsync(string topic, string value);

        Task<SubscribeResult> SubscribeFeedAsync(string feedKey);

        Task<SubscribeResult> SubscribeTopicAsync(string topic);

        Task<UnSubscribeResult> UnSubscribeFeedAsync(params string[] feedKeys);

        Task<UnSubscribeResult> UnsubscribeTopicAsync(params string[] topics);
    }
}