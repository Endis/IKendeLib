using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Beetle;
using System.Runtime.InteropServices;
using System.Net.Sockets;
namespace ChatRoom.Client
{
    
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }
        private Beetle.IChannel mChannel;
        StringBuilder mSumChat = new StringBuilder();
        private void FrmMain_Load(object sender, EventArgs e)
        {
            
            Beetle.Controllers.Controller.Register(this);
            TcpUtils.Setup("beetle");
            
            mSumChat.Append(@"{\rtf1\ansi\ansicpg936\deff0\deflang1033\deflangfe2052{\fonttbl{\f0\fnil\fcharset134 \'cb\'ce\'cc\'e5;}}");
            mSumChat.Append(@"{\colortbl ;\red51\green153\blue255;}");
            
            LoadFace();
            AddFace();
            richSay.Focus();
        }
        private void AddFace()
        {
            for (int i = 0; i < imglstFace.Images.Count; i++)
            {
                PictureBox pb = new PictureBox();
                pb.Width = 24;
                pb.Height = 24;
                pb.Tag = i;
                pb.Image = imglstFace.Images[i];
                pb.MouseEnter += (o, e1) =>
                {
                    ((PictureBox)o).BorderStyle = BorderStyle.FixedSingle;
                    if (mTmpImg != null)
                        mTmpImg.BorderStyle = BorderStyle.None;
                    mTmpImg = (PictureBox)o;
                };
                pb.MouseLeave += (o, e1) =>
                {
                    ((PictureBox)o).BorderStyle = BorderStyle.None;
                };
                pb.Click += (o, e1) =>
                {
                    int SelectIndex = (int)((PictureBox)o).Tag;
                    Clipboard.SetDataObject(imglstFace.Images[SelectIndex]);
                    DataFormats.Format format = DataFormats.GetFormat(DataFormats.Bitmap);
                    if (richSay.CanPaste(format))
                    {
                        richSay.Paste(format);
                    }
                };
                flowLayoutPanel1.Controls.Add(pb);
            }
        }
        private void cmdSend_Click(object sender, EventArgs e)
        {
            string tmp = richSay.Rtf;
            int startindex, endindex;
            startindex = tmp.IndexOf(@"\viewkind4");
            endindex = tmp.LastIndexOf("}");
            tmp = tmp.Substring(startindex, endindex - startindex);
            tmp= tmp.Replace("\\pard", "\\pard\\li220");
            string message = string.Format(@"\viewkind4\uc1\pard\sa200\sl276\slmult1\lang2052\f0\cf1\fs22 {0} \cf0 {1}  \cf0\line {2}",
               txtUserName.Text,  DateTime.Now, tmp);
            richSay.Rtf = "";
            richSay.Focus();
            addSay(message);
            Logic.Say say = new Logic.Say();
            say.Body = tmp;
            mChannel.Send(say);
        }
        private void addSay(string msg)
        {
            mSumChat.Append(msg);
            richTextBox1.Rtf = mSumChat.ToString() + "}";
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        } 
        PictureBox mTmpImg = null;
        private void LoadFace()
        {
            imglstFace.Images.Add(ResFaces._1329228341_7);
            imglstFace.Images.Add(ResFaces._1329228344_11);
            imglstFace.Images.Add(ResFaces._1329228345_8);
            imglstFace.Images.Add(ResFaces._1329228346_login_photo);
            imglstFace.Images.Add(ResFaces._1329228347_face_devilish);
            imglstFace.Images.Add(ResFaces._1329228351_face_sad);
            imglstFace.Images.Add(ResFaces._1329228352_face_smile);
            imglstFace.Images.Add(ResFaces._1329228354_face_devilish);
            imglstFace.Images.Add(ResFaces._1329228355_face_angel);
            imglstFace.Images.Add(ResFaces._1329228356_face_cool);
            imglstFace.Images.Add(ResFaces._1329228357_face_angry);
            imglstFace.Images.Add(ResFaces._1329228358_face_smile_big);
            imglstFace.Images.Add(ResFaces._1329228360_face_monkey);
            imglstFace.Images.Add(ResFaces._1329228378_face_sad);
            imglstFace.Images.Add(ResFaces._1329228380_face_plain);
            imglstFace.Images.Add(ResFaces._1329228381_face_worried);
            imglstFace.Images.Add(ResFaces._1329228382_face_crying);
            imglstFace.Images.Add(ResFaces._1329228383_face_kiss);
            imglstFace.Images.Add(ResFaces._1329228384_face_raspberry);
            imglstFace.Images.Add(ResFaces._1329228384_face_smirk);
            imglstFace.Images.Add(ResFaces._1329228385_face_uncertain);
            imglstFace.Images.Add(ResFaces._1329228386_face_grin);
            imglstFace.Images.Add(ResFaces._1329228390_face_plain);
            imglstFace.Images.Add(ResFaces._1329228391_face_tired);
            imglstFace.Images.Add(ResFaces._1329228392_face_wink);
            imglstFace.Images.Add(ResFaces._1329228394_face_surprise);
            imglstFace.Images.Add(ResFaces._1329228395_face_crying);
            imglstFace.Images.Add(ResFaces._1329228396_face_glasses_nerdy);
        }   
        private void button1_Click(object sender, EventArgs e)
        {
            richSay.SelectionColor = Color.BurlyWood;
        }
        private void onerror(object sender, Beetle.ChannelErrorEventArgs e)
        {
            Invoke(new Action<Exception>(o =>
            {
                string msg = e.Exception.Message;
                if (e.Exception.InnerException != null)
                    msg += "\t inner error:" + e.Exception.InnerException.Message;
                txtError.Text = msg;
            }), e.Exception);
        }
        private void ondisposed(object sender, Beetle.ChannelEventArgs e)
        {
            Invoke(new Action<object>(o =>
            {
                toolStrip2.Enabled = true;
                groupBox2.Enabled = false;
            }), new object());
        }
        
        public void _ReceiveSay(Beetle.IChannel channel, Logic.Say e)
        {
            string message = string.Format(@"\viewkind4\uc1\pard\sa200\sl276\slmult1\lang2052\f0\cf1\fs22 {0} \cf0 {2} IP:{1} \cf0\line {3}",
               e.User.Name, e.User.IP, DateTime.Now, e.Body);
            Invoke(new Action<string>(msg => { addSay(msg); }), message);
        }
        public void _OtherUnRegister(Beetle.IChannel channel, Logic.UnRegister e)
        {
            Invoke(new Action<Logic.UnRegister>(o =>
            {
                lstUsers.Items.Remove(o.User);
            }), e);
        }
        public void _OthreRegister(Beetle.IChannel channel, Logic.OnRegister e)
        {
            Invoke(new Action<Logic.OnRegister>(o =>
            {
                if (!lstUsers.Items.Contains(o.User))
                    lstUsers.Items.Add(o.User);
            }), e);
        }
        public void _OnLogin(Beetle.IChannel channel, Logic.RegisterResponse e)
        {
            Logic.ListUsers list = new Logic.ListUsers();
            channel.Send(list);
            Invoke(new Action<object>(o =>
            {
               
                toolStrip2.Enabled = false;
                groupBox2.Enabled = true;
            }), new object());
        }
        public void _OnList(Beetle.IChannel channel, Logic.ListUsersResponse e)
        {
            Invoke(new Action<Logic.ListUsersResponse>(o =>
            {
                lstUsers.Items.Clear();
                foreach (Logic.UserInfo item in o.Users)
                {
                    lstUsers.Items.Add(item);
                }
                
            }), e);
        }
        private void OnConnect()
        {
            try
            {
                txtError.Text = "";
                uint dummy = 0;
                byte[] inOptionValues = new byte[Marshal.SizeOf(dummy) * 3];
                BitConverter.GetBytes((uint)1).CopyTo(inOptionValues, 0);
                BitConverter.GetBytes((uint)5000).CopyTo(inOptionValues, Marshal.SizeOf(dummy));
                BitConverter.GetBytes((uint)5000).CopyTo(inOptionValues, Marshal.SizeOf(dummy) * 2);
                mChannel = TcpServer.CreateClient(txtIPAddress.Text, 9001);
                mChannel.Socket.IOControl(IOControlCode.KeepAliveValues, inOptionValues, null);
                mChannel.ChannelError += onerror;
                mChannel.ChannelDisposed += ondisposed;
                mChannel.SetPackage<Logic.HeadSizePage>();
                mChannel.Package.ReceiveMessage = OnReceive;
                mChannel.BeginReceive();
                Logic.Register register = new Logic.Register();
                register.Name = txtUserName.Text;
                mChannel.Send(register);
            }
            catch (Exception e_)
            {
                MessageBox.Show(this, e_.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void OnReceive(Beetle.PacketRecieveMessagerArgs e)
        {
            Beetle.Controllers.Controller.Invoke(e.Channel, e.Message);

        }
        private void cmdLogin_Click_1(object sender, EventArgs e)
        {
            OnConnect();
        }
    }
}
