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
            n = new Network("10.156.145.164", 8085);
            //n = new Network("127.0.0.1", 8085);
            n.Connect();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Data d = new Data(DataType.STRING, textBox1.Text);
            n.Send(d);

            textBox1.Text = "";
            textBox1.Focus();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                button1_Click(null, null);
            }
        }
    }
}
