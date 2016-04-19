using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace Network
{
    public class Client
    {
        public Socket sock;
        public string NickName;
        public byte[] buffer = new byte[4024];
        public List<string> stringlist = new List<string>();

        public string IP
        {
            get { return sock.RemoteEndPoint.ToString().Split(':')[0]; }
        }

        public Client(Socket sock)
        {
            this.sock = sock;
        }
    }
}
