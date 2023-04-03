﻿using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using ImageChat.Protocol;
using ImageChat.Shared;

namespace ImageChat.Client.Client
{
    public class ServerLocatorSenderService : BaseThreadService
    {
        private readonly IPEndPoint _broadcastIpEndPoint;
        private readonly byte[] _broadcastDatagram;
        
        public ServerLocatorSenderService(TimeSpan loopDelay, int broadcastPort, int receiverPort) : base(loopDelay)
        {
            IPAddress broadcastAddress = IpAddressUtility.GetBroadcastAddress();
            
            _broadcastIpEndPoint = new IPEndPoint(broadcastAddress, broadcastPort);
            
            _broadcastDatagram =
                UdpSocketUtility.PrepareDatagramForSendingString(
                    Constants.UdpDatagramSize, 
                    $"[{IpAddressUtility.GetLocalIpAddress()}:{receiverPort}]Get image chat server IP&Port",
                    () => throw new ArgumentOutOfRangeException(
                        $"Can not send string [Follow the white rabbit!], data size exceeds datagram size")
                );
        }
        
        protected override Socket CreateServiceSocket()
        {
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            socket.EnableBroadcast = true;

            return socket;
        }

        protected override void ServiceWorkerLoop(Socket serviceSocket)
        {
            serviceSocket.BeginSendTo(_broadcastDatagram, 0, Constants.UdpDatagramSize, SocketFlags.None, 
                _broadcastIpEndPoint, SendToCallback,serviceSocket);
        }

        private void SendToCallback(IAsyncResult asyncData)
        {
            var serviceSocket = (Socket)asyncData.AsyncState;

            try
            {
                serviceSocket.EndSendTo(asyncData);
            }
            catch (ObjectDisposedException)// callback called while dispose/close call
            {
            }
            
            Console.WriteLine($@"{DateTime.Now.ToLongTimeString()} -> [ServerLocatorSenderService] " + 
                              "broadcast message sent to image chat server.");
        }

    }
}