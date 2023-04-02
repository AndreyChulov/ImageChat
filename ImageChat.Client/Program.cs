using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageChat.Client
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (FileStream consoleStream = new FileStream("client_console.log", FileMode.Create))
            using (TextWriter consoleWriter = new StreamWriter(consoleStream))
            {
                Console.SetOut(consoleWriter);
                
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new ImageChatClientForm());
                
                consoleWriter.Flush();
                consoleStream.Flush();
            }
        }
    }
}