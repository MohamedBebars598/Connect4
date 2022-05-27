using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client
{
    public partial class BoardSizes : Form
    {
        int row;
        int col;
       
        public BoardSizes()
        {
            InitializeComponent();
            row = 6;
            col = 7;
         
        }
        public int  colret{

            get
            {
                return col;
            }
            }
        public int rowRet
        {
            get
            {
                return row;
            }
        }
        private void Form4_Load(object sender, EventArgs e) // 7*6
        {
               
        }

        private void button1_Click(object sender, EventArgs e)
        {
            row = 6;
            col = 7;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            row = 7;
            col = 8;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            row = 8;
            col = 8;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            row = 7;
            col = 9;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

    }
}
