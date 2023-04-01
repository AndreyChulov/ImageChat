using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using ImageChat.Protocol;

namespace ImageChatClient.Client
{
    public class ServerLocatorSenderService : BaseThreadService
    {
        private readonly int _broadcastPort;
        
        public ServerLocatorSenderService(TimeSpan loopDelay, int broadcastPort) : base(loopDelay)
        {
            _broadcastPort = broadcastPort;
        }
        
        protected override Socket CreateServiceSocket()
        {
            var socket = new Socket(SocketType.Dgram, ProtocolType.Udp);

            socket.EnableBroadcast = true;
            
            IPAddress broadcastAddress = CreateBroadcastAddress();
            IPEndPoint broadcastIpEndPoint = new IPEndPoint(broadcastAddress, _broadcastPort);

            using (SocketAsyncState socketAsyncState = new SocketAsyncState(socket))
            {
                socket.BeginConnect(broadcastIpEndPoint, BeginConnectCallback, socketAsyncState);
                
                socketAsyncState.ManualResetEvent.WaitOne();
            }
            
            return socket;
        }

        private void BeginConnectCallback(IAsyncResult asyncInfo)
        {
            var socketAsyncState = (SocketAsyncState)asyncInfo.AsyncState;

            try
            {
                socketAsyncState.Socket.EndConnect(asyncInfo);
            }
            catch (ObjectDisposedException)// callback called while dispose/close call
            {
            }

            socketAsyncState.ManualResetEvent.Set();
        }

        protected override void ServiceWorkerLoop(Socket serviceSocket)
        {
            SocketUtility.SendString(serviceSocket, "Follow the white rabbit!", () => { });
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
    }
}