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

            using (Connection client = DFSClien.UP("SFD"))
            {
                client.write(data);
                client.Write(file);
                client.read();
                client.readto(file);
            }

            Upload up = new Upload(data);
            string file = up.exeute();
            System.Threading.Thread.Sleep(-1);
        }
        
    }
}
