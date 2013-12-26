using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using BeetleNPDemo.Resources;

namespace BeetleNPDemo
{
    public partial class MainPage : PhoneApplicationPage,Beetle.NetPackage.INetClientHandler
    {
        // 构造函数
        public MainPage()
        {
            InitializeComponent();

            // 用于本地化 ApplicationBar 的示例代码
            //BuildLocalizedApplicationBar();
            mClient = new Beetle.NetPackage.NetClient("192.168.0.104", 9088, new NPPackage(), this);
            mClient.LittleEndian = false;
        }

        private Beetle.NetPackage.NetClient mClient ;

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
           
            mClient.Connect();  
        }

        // 用于生成本地化 ApplicationBar 的示例代码
        //private void BuildLocalizedApplicationBar()
        //{
        //    // 将页面的 ApplicationBar 设置为 ApplicationBar 的新实例。
        //    ApplicationBar = new ApplicationBar();

        //    // 创建新按钮并将文本值设置为 AppResources 中的本地化字符串。
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // 使用 AppResources 中的本地化字符串创建新菜单项。
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}

        public void ClientReceive(Beetle.NetPackage.NetClient client, object message)
        {
            if (message is Register)
            {
                Dispatcher.BeginInvoke(() =>
                {
                    Register reg = (Register)message;
                    txtRegtime.Text = reg.RegTime.ToString();
                });
            }
        }

        public void ClientError(Beetle.NetPackage.NetClient client, Exception e)
        {
            this.Dispatcher.BeginInvoke(() => {
                txtErrors.Text = e.Message;
            });
        }

        public void ClientDisposed(Beetle.NetPackage.NetClient client)
        {
            this.Dispatcher.BeginInvoke(() => {
                txtStatus.Text = "disposed";
            });
        }

        public void Connected(Beetle.NetPackage.NetClient client)
        {
            this.Dispatcher.BeginInvoke(() =>
            {
                txtStatus.Text = "connected!";
            });
        }

        private void cmdRegister_Click(object sender, RoutedEventArgs e)
        {
            Register reg = new Register();
            reg.Name = txtName.Text;
            reg.EMail = txtEMail.Text;
            reg.City = txtCity.Text;
            reg.Country = txtCountry.Text;
            mClient.Send(reg);
        }
    }
}