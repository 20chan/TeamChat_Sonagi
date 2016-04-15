using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Network;

namespace Sonagi
{
    public partial class Chat : UserControl
    {
        public Data ChatData;
        public Chat(Data data)
        {
            InitializeComponent();
            this.ChatData = data;

            switch(data.Type)
            {
                case DataType.STRING:
                    break;
                case DataType.IMAGE:
                    break;
                case DataType.FILE:
                    break;
            }
        }
    }
}
