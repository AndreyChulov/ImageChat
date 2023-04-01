using System;
using System.Net;
using System.Net.Sockets;
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
            var socket = new Socket(SocketType.Dgram, ProtocolType.Udp);

            socket.Bind(new IPEndPoint(IPAddress.Any, _bindingPort));
            
            return socket;
        }

        protected override void ServiceWorkerLoop(Socket serviceSocket)
        {
            //throw new NotImplementedException();
        }
    }
}