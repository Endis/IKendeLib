using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BeetleProtobuf.Client
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private Beetle.IChannel mChannel = null;

        private void FrmMain_Load(object sender, EventArgs e)
        {
            Beetle.TcpUtils.Setup("beetle");
            Beetle.Protobuf.Package.Register(typeof(FrmMain).Assembly);
        }

        private void cmdConnect_Click(object sender, EventArgs e)
        {
            try
            {
                mChannel = Beetle.TcpServer.CreateClient<Beetle.Protobuf.Package>(
                    txtIPAddress.Text, 9321, OnReveive);
                mChannel.ChannelDisposed += OnDisposed;
                cmdRegister.Enabled = !(cmdConnect.Enabled = false);
                mChannel.BeginReceive();
            }
            catch (Exception e_)
            {
                MessageBox.Show(e_.Message);
            }
        }

        private void OnDisposed(object sender, Beetle.ChannelDisposedEventArgs e)
        {
            Invoke(new Action<Register>(o =>
            {
                cmdRegister.Enabled = !(cmdConnect.Enabled = true);
            }), null);
        }
        private void OnReveive(Beetle.PacketRecieveMessagerArgs e)
        {
            Invoke(new Action<Register>(o => {
                txtRegTime.Text = o.RegTime.ToString();
            }), e.Message);
        }

        private void cmdRegister_Click(object sender, EventArgs e)
        {
            Register reg = new Register();
            reg.UserName = txtName.Text;
            reg.EMail = txtEMail.Text;
            mChannel.Send(reg);
        }
    }
}
