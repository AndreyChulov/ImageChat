using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using ImageChat.Shared;

namespace ImageChat.Client.Client
{
    public class ServerLocatorReceiverService : BaseThreadService
    {
        public ServerLocatorReceiverService(TimeSpan loopDelay) : base(loopDelay)
        {
        }

        protected override Socket CreateServiceSocket()
        {
            var socket = new Socket(SocketType.Dgram, ProtocolType.Udp);
            //socket.Bind(new IPEndPoint(IPAddress.Loopback, 22222));
            return socket;
        }

        protected override void ServiceWorkerLoop(Socket serviceSocket)
        {
            //throw new NotImplementedException();
        }
    }
}