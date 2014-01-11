using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Beetle.Tracker.TestImpl.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Register()
        {
            TestTackerHandler handler = new TestTackerHandler();
            TestProperties properties = new TestProperties();
            properties.Group = "TEST";
            properties.Node = "T1";
            properties.Host = "192.168.0.121";
            properties.Port = "8088";
            handler.Register(properties);
            AppHost host = handler.GetHost(new TestProperties { Group = "TEST" });
            Assert.AreEqual(properties.Host,host.Host );
            Assert.AreEqual(properties.Port, host.Port.ToString());
        }
        [TestMethod]
        public void GetInfo()
        {
            TestTackerHandler handler = new TestTackerHandler();
            TestProperties properties = new TestProperties();
            properties.Group = "TEST";
            properties.Node = "T1";
            properties.Host = "192.168.0.121";
            properties.Port = "8088";
            handler.Register(properties);
            properties = new TestProperties();
            properties.Group = "TEST";
            properties.Node = "T2";
            properties.Host = "192.168.0.122";
            properties.Port = "8088";
            handler.Register(properties);
            //Group group = (Group)handler.GetInfo(new TestProperties { Group = "TEST" });
            //Assert.AreEqual("T1", group.Nodes[0].Name);
            //Assert.AreEqual("T2", group.Nodes[1].Name);
        }
        [TestMethod]
        public void GetHost()
        {
            TestTackerHandler handler = new TestTackerHandler();
            TestProperties properties = new TestProperties();
            properties.Group = "TEST";
            properties.Node = "T1";
            properties.Host = "192.168.0.121";
            properties.Port = "8088";
            handler.Register(properties);
            properties = new TestProperties();
            properties.Group = "TEST";
            properties.Node = "T2";
            properties.Host = "192.168.0.122";
            properties.Port = "8088";
            handler.Register(properties);
            AppHost host = handler.GetHost(new TestProperties { Group = "TEST" });
            Assert.AreEqual("192.168.0.121",host.Host);
            Assert.AreEqual(8088, host.Port);
            host = handler.GetHost(new TestProperties { Group = "TEST" });
            Assert.AreEqual("192.168.0.122", host.Host);
            Assert.AreEqual(8088, host.Port);
        }
    }
}
