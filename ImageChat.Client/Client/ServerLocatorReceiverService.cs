using System;
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
            return new Socket(SocketType.Dgram, ProtocolType.Udp);
        }

        protected override void ServiceWorkerLoop(Socket serviceSocket)
        {
            //throw new NotImplementedException();
        }
    }
}