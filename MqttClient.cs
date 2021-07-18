﻿using System;
using System.Collections.Generic;
using System.Text;
using MQTTnet.Client;
using MQTTnet.Client.Options;

namespace Adafruit_MQTT_Client
{
    public enum ConnectionMode
    {
        WebSocket,
        TCP
    }

    public class MqttClient
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
            string clientId = null,
            bool isSecure = true)
            : this(hostAddress, userName, key, clientId)
        {
            _secureTcpPort = secureTcpPort;

            _inSecureTcpPort = inSecureTcpPort;
        }

        private void InitClient(ConnectionMode connectionMode, bool secureConnection)
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


            if (connectionMode == ConnectionMode.TCP)
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

        private string GetTopicNameFromFeedKey(string feedKey)
        {
            return $"{_userName}/feeds/{feedKey}";
        }

    }
}
