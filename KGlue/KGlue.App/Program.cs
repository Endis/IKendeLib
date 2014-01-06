using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KGlue.App
{
    class Program
    {
        private static KGlue.Center mCenter;
        static void Main(string[] args)
        {
            mCenter = new Center("appSection");
            mCenter.LogEvent += (o, e) => {
                switch (e.Status)
                {
                    case ExecutingStatus.Error:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case ExecutingStatus.Success:
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    case ExecutingStatus.Warning:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                }
                Console.WriteLine("{0}\t{1}",DateTime.Now, e.Message);
                if (e.Error != null)
                {
                    Console.WriteLine(e.Error.Message);
                    Console.WriteLine(e.Error.StackTrace);
                }
            };
            mCenter.Start();
            System.Threading.Thread.Sleep(-1);
        }
    }
}
