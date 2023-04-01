using System;
using System.Net.Sockets;
using ImageChat.Protocol.Utilities;

namespace ImageChat.Protocol
{
    public static class SocketUtility
    {
        public static string ReceiveString(Socket socket, 
            Action onReceiveDataSizeCheckFail, Action onReceiveDataCheckFail)
        {
            return SocketStringReceiver.ReceiveString(socket, onReceiveDataSizeCheckFail, onReceiveDataCheckFail);
        }

        public static void SendString(Socket socket, string dataToSend, Action onSendDataCheckFail)
        {
            SocketStringSender.SendString(socket, dataToSend, onSendDataCheckFail);
        }
    }
}