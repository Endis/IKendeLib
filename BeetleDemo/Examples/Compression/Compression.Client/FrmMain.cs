using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Compression;
namespace Compression.Client
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }
        private Beetle.IChannel channel = null;
        private void cmdConnetc_Click(object sender, EventArgs e)
        {
            try
            {
                channel = Beetle.TcpServer.CreateClient<CompressionPackage>(txtIPAddress.Text, 9321, OnReceive);
                channel.ChannelDisposed += OnDisposed;
                channel.ChannelError += OnError;
                channel.BeginReceive();
                cmdRegister.Enabled = true;
            }
            catch (Exception e_)
            {
                MessageBox.Show(e_.Message);
            }
        }
        private void OnReceive(Beetle.PacketRecieveMessagerArgs e)
        {

        }
        private void OnDisposed(object sender, Beetle.ChannelEventArgs e)
        {
            Invoke(new Action<Beetle.ChannelEventArgs>(s =>
            {
                txtStatus.Text = "disconnect!";
            }), e);
        }
        private void OnError(object sender, Beetle.ChannelErrorEventArgs e)
        {
            Invoke(new Action<Beetle.ChannelErrorEventArgs>(r =>
            {
                txtStatus.Text = r.Exception.Message;
            }), e);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Beetle.TcpUtils.Setup("beetle");

        }
        private void cmdRegister_Click(object sender, EventArgs e)
        {
            FileContent content = new FileContent();
            content.Data = richTextBox1.Text;

            channel.Send(content);
        }
    }
}
