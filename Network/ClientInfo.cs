using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network
{
    [Serializable]
    public class ClientInfo
    {
        public ClientInfo(string ip, string name)
        {
            this.IP = ip;
            this.NickName = name;
        }

        public string IP;
        
        private string nickName;
        public string NickName
        {
            get
            {
                return nickName;
            }

            set
            {
                if (value.Length >= 20)
                    throw new Exception("닉네임이 너무 길다");
                nickName = value;
            }
        }
        
    }
}
