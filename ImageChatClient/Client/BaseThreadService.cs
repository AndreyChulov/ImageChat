using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace ImageChatClient.Client
{
    public abstract class BaseThreadService : IDisposable
    {
        private readonly TimeSpan _loopDelay;
        private bool _isStarted;
        private readonly Thread _serviceThread;
        private Socket _serviceSocket;

        protected BaseThreadService(TimeSpan loopDelay)
        {
            _loopDelay = loopDelay;
            _isStarted = false;
            _serviceThread = new Thread(ServiceWorker);
        }

        protected abstract Socket CreateServiceSocket();
        
        public virtual void Start()
        {
            _isStarted = true;

            _serviceThread.Start();
        }

        public virtual void Stop()
        {
            _isStarted = false;

            _serviceThread.Abort();
        }

        protected virtual void ServiceWorker()
        {
            _serviceSocket = CreateServiceSocket();

            while (_isStarted)
            {
                ServiceWorkerLoop(_serviceSocket);
                
                Task.Delay(_loopDelay);
            }
        }

        protected abstract void ServiceWorkerLoop(Socket serviceSocket);
        
        public virtual void Dispose()
        {
            Stop();
            _serviceSocket.Disconnect(false);
            _serviceSocket.Close();
            _serviceSocket.Dispose();
        }
    }
}