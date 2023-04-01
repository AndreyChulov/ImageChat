using System;
using System.Collections.Generic;

namespace ImageChat.Client.Client
{
    public class ServerLocatorService : IDisposable
    {
        private List<string> _servers;
        private readonly ServerLocatorSenderService _serverLocatorSenderService;
        private readonly ServerLocatorReceiverService _serverLocatorReceiverService;

        public ServerLocatorService()
        {
            _servers = new List<string>();
            _serverLocatorSenderService = new ServerLocatorSenderService(TimeSpan.FromSeconds(0.5f), 11111);
            _serverLocatorReceiverService = new ServerLocatorReceiverService(TimeSpan.FromSeconds(0.5f));
        }

        public void Start()
        {
            _serverLocatorSenderService.Start();
            _serverLocatorReceiverService.Start();
        }

        public void Stop()
        {
            _serverLocatorSenderService.Stop();
            _serverLocatorReceiverService.Stop();
        }

        public void Dispose()
        {
            _serverLocatorSenderService?.Dispose();
            _serverLocatorReceiverService?.Dispose();
        }
    }
}