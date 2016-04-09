using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonagi_Server
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Network n = new Network(8085);
            System.Threading.Thread t = new System.Threading.Thread(n.Listen);
            t.Start();

            while(true)
            {
                Console.ReadLine();
            }
        }
    }
}
