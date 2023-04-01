using System;
using System.IO;
using System.Net.Sockets;

namespace ImageChat.Protocol.Utilities
{
    internal static class SocketStringSender
    {
        public static void SendString(Socket socket, string dataToSend, Action onSendDataCheckFail)
        {
            using (Stream dataStream = new MemoryStream())
            using (BinaryWriter dataStreamWriter = new BinaryWriter(dataStream))
            {
                WriteDataPacketToStream(dataToSend, dataStreamWriter, dataStream);

                var sendDataBuffer = GetSendDataBuffer(onSendDataCheckFail, dataStream);

                socket.Send(sendDataBuffer);
            }
        }

        private static byte[] GetSendDataBuffer(Action onSendDataCheckFail, Stream dataStream)
        {
            dataStream.Seek(0, SeekOrigin.Begin);

            byte[] sendDataBuffer = new byte[dataStream.Position];
            int readBytesFromMemoryStream = dataStream.Read(sendDataBuffer, 0, sendDataBuffer.Length);

            if (readBytesFromMemoryStream != sendDataBuffer.Length)
            {
                onSendDataCheckFail();
            }

            return sendDataBuffer;
        }

        private static void WriteDataPacketToStream(string dataToSend, BinaryWriter dataStreamWriter, Stream dataStream)
        {
            /*
             * записываем пустышку вместо размера пакета данных,
             * на данном этапе мы не знаем размер отправляемых данных
             */
            dataStreamWriter.Write((long)0);

            dataStreamWriter.Write(dataToSend);
            dataStreamWriter.Flush();

            /*
             * Перезаписываем актуальный размер пакета данных,
             * теперь мы знаем его размер
             */
            dataStream.Seek(0, SeekOrigin.Begin);
            dataStreamWriter.Write(dataStream.Length - sizeof(long));
        }
    }
}