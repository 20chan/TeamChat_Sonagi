using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Network
{
    public delegate void msgProc(Data d);
    public class NetworkClient // For Client
    {
        public static string MYObEJCT = "";
        public event msgProc GetMessage;
        int port;
        public static string myIP;
        public static string myNick = "";
        public string serverIP;
        Socket sock;
        //Socket serv;
        public NetworkClient(string ip, int port)
        {
            this.serverIP = ip;
            this.port = port;
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Connect()
        {
            try
            {
                SocketAsyncEventArgs _args = new SocketAsyncEventArgs();
                _args.RemoteEndPoint = new IPEndPoint(IPAddress.Parse(serverIP), port);
                _args.Completed += _args_Completed;
                sock.ConnectAsync(_args);
                Send((new Data(DataType.NONE, null, new ClientInfo(myIP, myNick))));
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void _args_Completed(object sender, SocketAsyncEventArgs e)
        {
            sock = (Socket)sender;

            if (sock.Connected)
            {
                Data d = new Data(DataType.NONE, "", new ClientInfo(myIP, myNick));

                byte[] _data = new byte[1024];
                byte[] serialized = d.Serialize();

                for (int i = 0; i < 1024; i++)
                    _data[i] = 0;
                for (int i = 0; i < serialized.Length; i++)
                    _data[i] = serialized[i];

                SocketAsyncEventArgs _receiveArgs = new SocketAsyncEventArgs();
                _receiveArgs.UserToken = d;
                _receiveArgs.SetBuffer(_data, 0, 1024);
                _receiveArgs.Completed += _receiveArgs_Completed;
                sock.ReceiveAsync(_receiveArgs);
            }
            else
            {
                sock = null;
            }
        }

        private void _receiveArgs_Completed(object sender, SocketAsyncEventArgs e)
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(Receive), 1);
            t.Start(e);
        }

        private static object lockObject = "";
        private void Receive(object e)
        {
            lock (lockObject)
            {
                try
                {
                    Data d = Data.Deserialize(((SocketAsyncEventArgs)e).Buffer);
                    if (d.Type == DataType.NONE)
                    {
                        myIP = d.InnerData.ToString();
                    }
                    else if (GetMessage != null)
                        GetMessage(d);
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
                finally
                {
                    sock.ReceiveAsync(((SocketAsyncEventArgs)e));
                }
            }
        }

        public void Send(Data data)
        {
            try
            {
                lock (MYObEJCT)
                {
                    /*
                    byte[] byte_buffer = data.Serialize();
                    sock.Send(byte_buffer, SocketFlags.None); //동기
                    */
                    /*
                    byte[] _data = new byte[1024];


                    for (int i = 0; i < 1024; i++)
                        _data[i] = 0;
                    for (int i = 0; i < serialized.Length; i++)
                        _data[i] = serialized[i];
                    */

                    //byte[] serialized = data.Serialize(); //기존의 방식
                    System.Threading.Thread.Sleep(10);
                    byte[] serialized = data.Serialize();
                    SocketAsyncEventArgs args = new SocketAsyncEventArgs();
                    args.SetBuffer(serialized, 0, serialized.Length); // 기존의 방식
                                                                      //args.SetBuffer(_data, 0, 1024);
                    args.Completed += Args_Completed;
                    //args.UserToken = this;
                    if (myIP == null)
                        System.Threading.Thread.Sleep(100);
                    sock.SendAsync(args);
                }

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void Args_Completed(object sender, SocketAsyncEventArgs e)
        {
            //System.Windows.Forms.MessageBox.Show("보내졌으");
        }
    }
}
