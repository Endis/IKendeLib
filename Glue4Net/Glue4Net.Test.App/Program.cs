using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glue4Net.Test.App
{
    class Program
    {
        static void Main(string[] args)
        {
            DomainAdapter da = new DomainAdapter(@"C:\Users\Administrator\Documents\GitHub\IKendeLib\Glue4Net\Glue4Net.Test.Demo\bin\Debug",
                "TEST",false);
            da.Log = new ConsoleLoger();
            da.Load();
            Demo.ITest test = (Demo.ITest)da.CreateProxyObject("test");
            int i = test.Add(2);
            Console.WriteLine(i);
            Console.Read();
        }
    }
    public class ConsoleLoger : MarshalByRefObject, IEventLog
    {

        public void Track(string value)
        {
            Console.WriteLine(value);
        }

        public void Track(string formater, params object[] data)
        {
            Console.WriteLine(formater,data);
        }

        public void Debug(string value)
        {
            Console.WriteLine(value);
        }

        public void Debug(string formater, params object[] data)
        {
            Console.WriteLine(formater,data);
        }

        public void Info(string value)
        {
            Console.WriteLine(value);
        }

        public void Info(string formater, params object[] data)
        {
            Console.WriteLine(formater,data);
        }

        public void Error(string value)
        {
            Console.WriteLine(value);
        }

        public void Error(string formater, params object[] data)
        {
            Console.WriteLine(formater,data);
        }
        public override object InitializeLifetimeService()
        {
            return null;
        }
    }
}
