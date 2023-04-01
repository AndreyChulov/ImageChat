using System;
using System.Windows.Forms;

namespace ImageChatServer
{
    public partial class ImageChatServerForm : Form
    {
        public ImageChatServerForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _serverLocatorService.Start();
        }

        private void ImageChatServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _serverLocatorService.Dispose();
        }
    }
}