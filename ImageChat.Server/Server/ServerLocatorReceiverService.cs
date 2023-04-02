using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using ImageChat.Protocol;
using ImageChat.Shared;

namespace ImageChat.Server.Server
{
    public class ServerLocatorReceiverService : BaseThreadService
    {
        private readonly int _bindingPort;
        
        public ServerLocatorReceiverService(TimeSpan loopDelay) : base(loopDelay)
        {
            _bindingPort = Constants.ServerLocatorBroadcastPort;
        }

        protected override Socket CreateServiceSocket()
        {
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            socket.EnableBroadcast = true;
            socket.Bind(new IPEndPoint(IPAddress.Any, _bindingPort));
            
            
            return socket;
        }

        protected override void ServiceWorkerLoop(Socket serviceSocket)
        {
            if (serviceSocket.Available == 0)
            {
                return;
            }

            byte[] buffer = new byte[Constants.UdpDatagramSize];
            int size = serviceSocket.Receive(buffer);
            string message = UdpSocketUtility.GetStringFromDatagram(buffer);
            Console.WriteLine($@"Server received broadcast message [{message}]");
        }
    }
}