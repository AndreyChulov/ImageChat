using System;
using System.IO;
using System.Windows.Forms;
using ImageChat.Server;

namespace ImageChat.Server
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (FileStream consoleStream = new FileStream("server_console.log", FileMode.Create))
            using (TextWriter consoleWriter = new StreamWriter(consoleStream))
            {
                Console.SetOut(consoleWriter);
                
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new ImageChatServerForm());
                
                consoleWriter.Flush();
                consoleStream.Flush();
            }
        }
    }
}