using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace Network
{
    public enum DataType
    {
        NONE, INFO, STRING, IMAGE, FILE
    }

    [Serializable]
    public class Data
    {
        public DataType Type;
        public object InnerData;
        public ClientInfo Info;

        public Data(DataType t, object d, ClientInfo info)
        {
            this.Info = info;
            this.Type = t; this.InnerData = d;
        }

        public byte[] Serialize()
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(ms, this);

            return ms.ToArray();
        }

        public static Data Deserialize(byte[] data)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream ms = new MemoryStream(data);
            return (Data)formatter.Deserialize(ms);
        }
    }
}
