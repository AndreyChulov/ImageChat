﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageChat.Client
{
    public partial class ImageChatClientForm : Form
    {
        public ImageChatClientForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _serverLocatorService.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _serverLocatorService.Stop();
            _serverLocatorService.Dispose();
        }
    }
}