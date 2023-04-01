using System.Net.Sockets;
using System.Threading;

namespace ChatProtocol.Utilities
{
    internal static class SocketReceiver
    {
        public static void WaitDataFromSocket(Socket clientSocket)
        {
            WaitDataFromSocket(clientSocket, 1);
        }

        public static void WaitDataFromSocket(Socket clientSocket, int waitForBytesAvailable)
        {
            while (clientSocket.Available < waitForBytesAvailable)
            {
                Thread.Sleep(100);
            }
        }
    }
}