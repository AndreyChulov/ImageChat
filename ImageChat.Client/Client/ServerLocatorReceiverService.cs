using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using ImageChat.Shared;

namespace ImageChat.Client.Client
{
    public class ServerLocatorReceiverService : BaseThreadService
    {
        public int UdpPort { get; }
        
        public ServerLocatorReceiverService(TimeSpan loopDelay) : base(loopDelay)
        {
            Random randomGenerator = new Random();
            UdpPort = Constants.ServerLocatorUdpPorts[randomGenerator.Next(0, Constants.ServerLocatorUdpPorts.Length)];
        }

        protected override Socket CreateServiceSocket()
        {
            var socket = new Socket(SocketType.Dgram, ProtocolType.Udp);
            
            socket.Bind(new IPEndPoint(IPAddress.Any, UdpPort));
            
            return socket;
        }

        protected override void ServiceWorkerLoop(Socket serviceSocket)
        {
            if (serviceSocket.Available > 0)
            {
                Task.Delay(TimeSpan.FromSeconds(1));
            }
        }
    }
}