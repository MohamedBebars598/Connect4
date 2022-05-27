using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client
{
    public partial class CoinColors : Form
    {
        string color="";
        
        public CoinColors( )
        {
            InitializeComponent();
            
        }
        
        private void yellow_Click(object sender, EventArgs e)
        {
            color ="yellow";
            yellow.BackColor = Color.YellowGreen;
            red.BackColor = Color.Red;
        }

        private void red_Click(object sender, EventArgs e)
        {
            color = "red";
            yellow.BackColor = Color.Yellow;
            red.BackColor = Color.PaleVioletRed;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (color=="")
            {
                MessageBox.Show("you must choose a color !!");
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        public string col
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
