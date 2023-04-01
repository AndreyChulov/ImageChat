using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using ChatProtocol;

namespace ImageChatClient.Client
{
    public class ServerLocatorService : IDisposable
    {
        private List<string> _servers;
        private bool _isStarted;
        private readonly Thread _serverLocatorSenderThread;
        private readonly Thread _serverLocatorReceiverThread;
        private readonly Socket _udpBroadcastSocket;

        public ServerLocatorService()
        {
            _servers = new List<string>();
            _isStarted = false;

            _serverLocatorSenderThread = new Thread(ServerLocatorSender);
            _serverLocatorReceiverThread = new Thread(ServerLocatorReciever);

            _udpBroadcastSocket = new Socket(SocketType.Dgram, ProtocolType.Udp);
            _udpBroadcastSocket.EnableBroadcast = true;
        }

        public void Start()
        {
            _isStarted = true;

            _serverLocatorSenderThread.Start();
            _serverLocatorReceiverThread.Start();
        }

        public void Stop()
        {
            _isStarted = false;

            Task.Delay(100).Wait();

            _serverLocatorReceiverThread.Abort();
            _serverLocatorSenderThread.Abort();
        }

        private void ServerLocatorSender()
        {
            IPAddress broadcastAddress = CreateBroadcastAddress();
            var broadcastIpEndPoint = new IPEndPoint(broadcastAddress, 11111);
            _udpBroadcastSocket.Connect(broadcastIpEndPoint);

            while (_isStarted)
            {
                SocketUtility.SendString(_udpBroadcastSocket, "Follow the white rabbit!", () => { });
                Task.Delay(1000).Wait();
            }
        }

        private static IPAddress CreateBroadcastAddress()
        {
            var localIpAddess = Dns
                                     .GetHostEntry(Dns.GetHostName())
                                     .AddressList
                                     .First(x => x.AddressFamily == AddressFamily.InterNetwork)
                                     .ToString();

            var localIpAddessNumbers = localIpAddess.Split('.');
            localIpAddessNumbers[3] = "255";
            var remoteIpAddressInString = localIpAddessNumbers
                .Aggregate("", (acc, value) => $"{acc}.{value}")
                .Substring(1);
            var broadcastAddress = IPAddress.Parse(remoteIpAddressInString);
            return broadcastAddress;
        }

        private void ServerLocatorReciever()
        {
            while (_isStarted)
            {
                Task.Delay(1000).Wait();
            }
        }

        public void Dispose()
        {
            Stop();
            _udpBroadcastSocket.Dispose();
        }
    }
}