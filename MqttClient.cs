using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Client.Receiving;

using Adafruit.MQTT.Client.Models;
using Adafruit.MQTT.Client.Models.Publishing;
using Adafruit.MQTT.Client.Models.Subscribing;
using Adafruit.MQTT.Client.Models.UnSubscribing;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;

namespace Adafruit.MQTT.Client
{
    public class MqttClient : Repositories.IMqttClient
    {
        private string _key;

        private string _userName;

        private string _clientId;

        private string _hostAddress = "io.adafruit.com";

        private int _secureTcpPort = 8883;

        private int _inSecureTcpPort = 1883;

        private readonly TimeSpan _keepAlivePeriod = TimeSpan.FromSeconds(60);

        private IMqttClient _client;

        private IMqttClientOptions _connectionOptions;


        public string ClientId { get => _clientId; }

        public bool IsConnected { get => _client.IsConnected; }

        public IMqttApplicationMessageReceivedHandler MessageReceivedHandler
        {
            get => _client.ApplicationMessageReceivedHandler;
            set => _client.ApplicationMessageReceivedHandler = value;
        }


        public MqttClient(
            string userName,
            string key,
            string clientId = null)
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

        public MqttClient(
            string hostAddress,
            string userName,
            string key,
            string clientId = null)
            : this(userName, key, clientId)
        {
            _hostAddress = hostAddress;
        }

        public MqttClient(
            string hostAddress,
            int secureTcpPort,
            int inSecureTcpPort,
            string userName,
            string key,
            string clientId = null)
            : this(hostAddress, userName, key, clientId)
        {
            _secureTcpPort = secureTcpPort;

            _inSecureTcpPort = inSecureTcpPort;
        }

        public void InitClient(ConnectionMode connectionMode, bool secureConnection)
        {
            int TcpPort;

            _client = new MQTTnet.MqttFactory().CreateMqttClient();

            var firstOptions =
                new MqttClientOptionsBuilder()
                .WithKeepAlivePeriod(_keepAlivePeriod)
                .WithClientId(_clientId)
                .WithCredentials(_userName, _key);

            if (secureConnection)
            {
                TcpPort = _secureTcpPort;
                firstOptions = firstOptions.WithTls();
            }
            else TcpPort = _inSecureTcpPort;


            if (connectionMode == ConnectionMode.Tcp)
            {
                _connectionOptions = firstOptions
                    .WithTcpServer(_hostAddress, TcpPort)
                    .Build();
            }
            else if (connectionMode == ConnectionMode.WebSocket)
            {
                _connectionOptions = firstOptions
                    .WithWebSocketServer(_hostAddress)
                    .Build();
            }

        }

        public async Task ConnectAsync()
        {
            await _client.ConnectAsync(_connectionOptions);
        }

        public async Task ConnectAsync(CancellationToken cancellationToken)
        {
            await _client.ConnectAsync(_connectionOptions, cancellationToken);
        }

        public async Task DisconnectAsync()
        {
            await _client.DisconnectAsync();
        }

        public async Task DisconnectAsync(CancellationToken cancellationToken)
        {
            await _client.DisconnectAsync(cancellationToken);
        }

        public async Task<PublishResult> PublishFeedAsync(string feedKey, string value)
        {
            string topic = GetTopicFromFeedKey(feedKey);

            return await PublishTopicAsync(topic, value);
        }

        public async Task<PublishResult> PublishTopicAsync(string topic, string value)
        {
            if (_client == null)
            {
                throw new Exception("Client not Init!");
            }

            var result = await _client.PublishAsync(topic, value);

            return new PublishResult(result);
        }

        public async Task<SubscribeResult> SubscribeFeedAsync(string feedKey)
        {
            string topic = GetTopicFromFeedKey(feedKey);

            return await SubscribeTopicAsync(topic);
        }

        public async Task<SubscribeResult> SubscribeTopicAsync(string topic)
        {
            if (_client == null)
            {
                throw new Exception("Client not Init!");
            }

            var result = await _client.SubscribeAsync(topic);

            return new SubscribeResult(result);
        }

        public async Task<UnSubscribeResult> UnSubscribeFeedAsync(params string[] feedKeys)
        {
            var topics = new List<string>();

            foreach (var FeedKey in feedKeys)
            {
                string topic = GetTopicFromFeedKey(FeedKey);

                topics.Add(topic);
            }

            return await UnsubscribeTopicAsync(topics.ToArray());
        }

        public async Task<UnSubscribeResult> UnsubscribeTopicAsync(params string[] topics)
        {
            if (_client == null)
            {
                throw new Exception("Client not Init!");
            }

            var result = await _client.UnsubscribeAsync(topics);

            return new UnSubscribeResult(result);
        }

        public IMqttClient OnConnected(Func<MqttClientConnectedEventArgs, Task> handler)
        {
            if (_client == null)
            {
                throw new Exception("Client not Init!");
            }
            return _client.UseConnectedHandler(handler);
        }

        public IMqttClient OnMessageReceived(Func<MqttApplicationMessageReceivedEventArgs, Task> handler)
        {
            if (_client == null)
            {
                throw new Exception("Client not Init!");
            }
            return _client.UseApplicationMessageReceivedHandler(handler);
        }

        public IMqttClient OnDisconnected(Func<MqttClientDisconnectedEventArgs, Task> handler)
        {
            if (_client == null)
            {
                throw new Exception("Client not Init!");
            }
            return _client.UseDisconnectedHandler(handler);
        }

        private string GetTopicFromFeedKey(string feedKey)
        {
            return $"{_userName}/feeds/{feedKey}";
        }

    }
}
