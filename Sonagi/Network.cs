using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using Network;

namespace Sonagi
{
    public class Network // For Client
    {
        int port;
        string ip;
        Socket sock;
        Socket serv;
        public Network(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        public void Connect()
        {
            try
            {
                sock.Connect(new IPEndPoint(IPAddress.Parse(ip), port));
                sock.Send((new Data(DataType.NONE, null).Serialize()));
            }
            catch { }
        }
        public void Send(Data data)
        {
            try
            {
                byte[] byte_buffer = data.Serialize();
                sock.Send(byte_buffer, SocketFlags.None);
            }
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        public void Receive()
        {
            Data d = new Data(DataType.STRING, "");
            SocketAsyncEventArgs _receiveArgs = new SocketAsyncEventArgs();
            _receiveArgs.UserToken = d;
            _receiveArgs.SetBuffer(d.Serialize(), 0, 4096);
            _receiveArgs.Completed += _receiveArgs_Completed;
            serv.ReceiveAsync(_receiveArgs);
        }

        private void _receiveArgs_Completed(object sender, SocketAsyncEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
