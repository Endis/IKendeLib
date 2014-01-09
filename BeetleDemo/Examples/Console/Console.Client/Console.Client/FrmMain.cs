using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Console.Client
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private  Beetle.IChannel channel;


        private void cmdConnect_Click(object sender, EventArgs e)
        {
            try
            {
                //连接到指定IP的端口服务
                channel = Beetle.TcpServer.CreateClient(txtIPAddress.Text, 9321);
                //绑定数据流接收事件
                channel.DataReceive = OnReceive;
                //连接断开事件
                channel.ChannelDisposed += OnDisposed;
                channel.ChannelError += (o, err) => {
                    System.Console.WriteLine(err.Exception.Message);
                };
                //开始接收数据
                channel.BeginReceive();
                cmdSend.Enabled = true;
                cmdConnect.Enabled = false;
            }
            catch (Exception e_)
            {
                MessageBox.Show(e_.Message);
            }

        }

        private void OnDisposed(object sender, Beetle.ChannelDisposedEventArgs e)
        {
            Invoke(new Action<object>(o => {
                cmdSend.Enabled = false;
                cmdConnect.Enabled = true;
            }),  new object());
        }

        private  void OnReceive(object sender, Beetle.ChannelReceiveEventArgs e)
        {

            string value = e.Channel.Coding.GetString(e.Data.Array, e.Data.Offset, e.Data.Count);
            this.Invoke(new Action<string>(s =>
            {
                richTextBox2.AppendText(value + "\r\n");
            }), value);

        }

        private void cmdSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(richTextBox1.Text))
                {
                    Beetle.StringMessage msg = new Beetle.StringMessage();
                    msg.Value = richTextBox1.Text;
                    channel.Send(msg);
                    richTextBox1.Text = "";
                    richTextBox1.Focus();
                    
                }
            }
            catch (Exception e_)
            {
                MessageBox.Show(e_.Message);
            }

        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            Beetle.TcpUtils.Setup("beetle");//初始化组件
        }
    }
}
