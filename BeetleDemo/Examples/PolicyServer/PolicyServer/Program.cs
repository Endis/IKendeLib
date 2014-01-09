using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Beetle;

namespace PolicyServer
{
    internal class Program : ServerBase
    {
        public enum ProlicyType
        {
            Flash,
            Silverlight
        }
        public Program.ProlicyType Type
        {
            get;
            set;
        }
        private static void Main(string[] args)
        {
            System.Console.WriteLine("Silverlight and Flash Prolicy Server Copyright @ henryfan 2012");
            System.Console.WriteLine("website:www.ikender.com");
            System.Console.WriteLine("email:henryfan@msn.com");
            try
            {
                TcpUtils.Setup("beetle");

                if (ProlicySection.Instance.FlashEnabled)
                {
                    new Program
                    {
                        Type = Program.ProlicyType.Flash
                    }.Open(ProlicySection.Instance.FlashPort);
                    System.Console.WriteLine("Start Flash Prolicy Server @" + ProlicySection.Instance.FlashPort);
                }
                if (ProlicySection.Instance.SLEnabled)
                {
                    new Program
                    {
                        Type = Program.ProlicyType.Silverlight
                    }.Open(ProlicySection.Instance.SLPort);
                    System.Console.WriteLine("Start Silverlight Prolicy Server @" + ProlicySection.Instance.SLPort);
                }
            }
            catch (Exception e_)
            {
                Console.WriteLine(e_.Message);
            }
            System.Threading.Thread.Sleep(-1);
        }
        protected override void OnConnected(object sender, ChannelEventArgs e)
        {
            base.OnConnected(sender, e);
            e.Channel.EnabledSendCompeletedEvent = true;
            e.Channel.SendMessageCompleted = delegate(object o, SendMessageCompletedArgs m)
            {
                m.Channel.Dispose();
            };
            System.Console.WriteLine("{0} connected!", e.Channel.EndPoint);
        }
        protected override void OnDisposed(object sender, ChannelDisposedEventArgs e)
        {
            base.OnDisposed(sender, e);
            System.Console.WriteLine("{0} disposed!", e.Channel.EndPoint);
        }
        protected override void OnError(object sender, ChannelErrorEventArgs e)
        {
            base.OnError(sender, e);
            System.Console.WriteLine("{0} error {1}!", e.Channel.EndPoint, e.Exception.Message);
        }
        protected override void OnReceive(object sender, ChannelReceiveEventArgs e)
        {
            base.OnReceive(sender, e);
            System.Console.WriteLine(e.Channel.Coding.GetString(e.Data.Array, e.Data.Offset, e.Data.Count));
            StringMessage stringMessage = new StringMessage();
            if (this.Type == Program.ProlicyType.Flash)
            {
                stringMessage.Value = Utils.GetFlashPolicy();
            }
            else
            {
                stringMessage.Value = Utils.GetSLPolicy();
            }
            e.Channel.Send(stringMessage);
        }
    }
    public class ProlicySection : ConfigurationSection
    {
        [System.CodeDom.Compiler.GeneratedCode("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        internal const string ProlicySectionSectionName = "prolicySection";
        [System.CodeDom.Compiler.GeneratedCode("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        internal const string XmlnsPropertyName = "xmlns";
        [System.CodeDom.Compiler.GeneratedCode("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        internal const string FlashPortPropertyName = "flashPort";
        [System.CodeDom.Compiler.GeneratedCode("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        internal const string FlashEnabledPropertyName = "flashEnabled";
        [System.CodeDom.Compiler.GeneratedCode("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        internal const string SLPortPropertyName = "sLPort";
        [System.CodeDom.Compiler.GeneratedCode("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        internal const string SLEnabledPropertyName = "sLEnabled";
        [System.CodeDom.Compiler.GeneratedCode("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        public static ProlicySection Instance
        {
            get
            {
                return (ProlicySection)ConfigurationManager.GetSection("prolicySection");
            }
        }
        [System.CodeDom.Compiler.GeneratedCode("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5"), ConfigurationProperty("xmlns", IsRequired = false, IsKey = false, IsDefaultCollection = false)]
        public string Xmlns
        {
            get
            {
                return (string)base["xmlns"];
            }
        }
        [System.CodeDom.Compiler.GeneratedCode("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5"), System.ComponentModel.Description("The FlashPort."), ConfigurationProperty("flashPort", IsRequired = true, IsKey = false, IsDefaultCollection = false, DefaultValue = 843)]
        public virtual int FlashPort
        {
            get
            {
                return (int)base["flashPort"];
            }
            set
            {
                base["flashPort"] = value;
            }
        }
        [System.CodeDom.Compiler.GeneratedCode("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5"), System.ComponentModel.Description("The FlashEnabled."), ConfigurationProperty("flashEnabled", IsRequired = true, IsKey = false, IsDefaultCollection = false, DefaultValue = true)]
        public virtual bool FlashEnabled
        {
            get
            {
                return (bool)base["flashEnabled"];
            }
            set
            {
                base["flashEnabled"] = value;
            }
        }
        [System.CodeDom.Compiler.GeneratedCode("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5"), System.ComponentModel.Description("The SLPort."), ConfigurationProperty("sLPort", IsRequired = true, IsKey = false, IsDefaultCollection = false, DefaultValue = 943)]
        public virtual int SLPort
        {
            get
            {
                return (int)base["sLPort"];
            }
            set
            {
                base["sLPort"] = value;
            }
        }
        [System.CodeDom.Compiler.GeneratedCode("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5"), System.ComponentModel.Description("The SLEnabled."), ConfigurationProperty("sLEnabled", IsRequired = true, IsKey = false, IsDefaultCollection = false, DefaultValue = true)]
        public virtual bool SLEnabled
        {
            get
            {
                return (bool)base["sLEnabled"];
            }
            set
            {
                base["sLEnabled"] = value;
            }
        }
        [System.CodeDom.Compiler.GeneratedCode("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        public override bool IsReadOnly()
        {
            return false;
        }
    }
    internal class Utils
    {
        private static string mSLPolicy;
        private static string mFlashPolicy;
        public static string GetSLPolicy()
        {
            if (Utils.mSLPolicy == null)
            {
                string path = System.AppDomain.CurrentDomain.BaseDirectory + "SLPolicy.xml";
                using (System.IO.StreamReader streamReader = new System.IO.StreamReader(path, System.Text.Encoding.UTF8))
                {
                    Utils.mSLPolicy = streamReader.ReadToEnd();
                }
            }
            return Utils.mSLPolicy;
        }
        public static string GetFlashPolicy()
        {
            if (Utils.mFlashPolicy == null)
            {
                string path = System.AppDomain.CurrentDomain.BaseDirectory + "FlashPolicy.xml";
                using (System.IO.StreamReader streamReader = new System.IO.StreamReader(path, System.Text.Encoding.UTF8))
                {
                    Utils.mFlashPolicy = streamReader.ReadToEnd() + "\0";
                }
            }
            return Utils.mFlashPolicy;
        }
    }
}
