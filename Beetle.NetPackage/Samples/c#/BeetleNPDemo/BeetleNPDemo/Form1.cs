using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Beetle.NetPackage;

namespace BeetleNPDemo
{
    public partial class Form1 : Form,Beetle.NetPackage.INetClientHandler
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Beetle.NetPackage.NetClient mClient;

        private void cmdRegister_Click(object sender, EventArgs e)
        {
            Register reg = new Register();
            reg.Name = txtName.Text;
            reg.EMail = txtEMail.Text;
            reg.City = txtCity.Text;
            reg.Country = txtCountry.Text;
            mClient.Send(reg);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mClient = new Beetle.NetPackage.NetClient("127.0.0.1", 9088, new NPPackage(), this);
            mClient.LittleEndian = false;
            mClient.Connect();
        }

        public void ClientReceive(Beetle.NetPackage.NetClient client, object message)
        {
            if (message is Register)
            {
                Invoke(new Action<Register>(msg => {
                    txtRegTime.Text = msg.RegTime.ToString();
                }), message as Register);
            }
        }


        public void ClientError(Beetle.NetPackage.NetClient client, Exception e)
        {
            Invoke(new Action<Exception>(err =>
            {
                txtError.Text = err.Message;
            }),e);
        }

        public void ClientDisposed(Beetle.NetPackage.NetClient client)
        {
            Invoke(new Action<NetClient>(c =>
            {
                txtStatus.Text = "close!";
            }), client);
        }

        public void Connected(Beetle.NetPackage.NetClient client)
        {
            Invoke(new Action<NetClient>(c =>
            {
                txtStatus.Text = "connected!";
            }), client);
        }
    }
}
