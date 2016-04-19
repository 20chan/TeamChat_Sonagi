using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Network;

namespace Sonagi_Server
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            NetworkServer n = new NetworkServer(8085);
            System.Threading.Thread t = new System.Threading.Thread(n.Listen);
            t.Start();

            while(true)
            {
                string s = Console.ReadLine();
                n.SendAll(new Data(DataType.INFO, "서버 : " + s, null));
            }
        }
    }
}
