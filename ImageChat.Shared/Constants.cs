using System;

namespace ImageChat.Shared
{
    public static class Constants
    {
        public static int ServerLocatorBroadcastPort => 11111;
        public static int UdpDatagramSize => 1024;
        public static TimeSpan ServerLocatorBroadcastDatagramSendTimeout => TimeSpan.FromSeconds(1);
        public static TimeSpan ServerLocatorBroadcastDatagramReceiveTimeout => TimeSpan.FromSeconds(0.3f);
    }
}