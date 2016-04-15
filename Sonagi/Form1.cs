using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Network;

namespace Sonagi
{
    public partial class Form1 : Form
    {
        Network n;
        public Form1()
        {
            InitializeComponent();

            //n = new Network("45.32.50.20", 8085);
            textBox3.Text = "45.32.50.20";
            //n = new Network("10.156.145.164", 8085);
            //n = new Network("10.156.145.152", 8085);
            //n = new Network("127.0.0.1", 8085);
            //n.GetMessage += N_GetMessage;
            //n.Connect();

            string name = "GUEST" + new Random().Next(0, 100);
            Network.myNick = name;
            textBox5.Text = name;
        }

        delegate void ChangeTextCallback(string str);
        private void N_GetMessage(Data d)
        {
            switch(d.Type)
            {
                case DataType.STRING:
                    this.Invoke(new MethodInvoker(delegate () { textBox2.AppendText(d.Info.NickName + " : " + d.InnerData.ToString() + Environment.NewLine); }));
                    break;
                case DataType.INFO:
                    this.Invoke(new MethodInvoker(delegate () { textBox2.AppendText(">> " + d.InnerData.ToString() + Environment.NewLine); }));
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isEnable)
            {
                if (textBox1.Text.Length >= 250) textBox1.Text = textBox1.Text.Substring(0, 250);
                Data d = new Data(DataType.STRING, textBox1.Text, new ClientInfo(Network.myIP, Network.myNick));
                n.Send(d);
                textBox1.Text = "";
                isEnable = false;
            }

            textBox1.Focus();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                button1_Click(null, null);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            n = new Network(this.textBox3.Text, Convert.ToInt32(textBox4.Text));
            Network.myNick = this.textBox5.Text;
            n.GetMessage += N_GetMessage;
            n.Connect();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            this.textBox5.Focus();
        }

        bool isEnable = true;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isEnable == false)
                isEnable = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
