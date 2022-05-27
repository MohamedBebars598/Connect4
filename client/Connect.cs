using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace client
{
    public partial class Connect : Form
    {
       
        public Stream nStream;
        TcpClient client;
        string username;
        Rooms ff;
        Thread connection;


        public Connect()
        {
            InitializeComponent();

            connection= new Thread(new ThreadStart(listner));
        }

        private void button_WOC1_Click(object sender, EventArgs e)
        {
            try
            {
                IPAddress addr = IPAddress.Parse(txt_ipaddress.Text);
                client = new TcpClient();
                username = txt_username.Text;
                client.Connect(addr, int.Parse(txt_port.Text));
                nStream = client.GetStream();

                this.Hide();
                if (client.Connected)
                {
                     ff = new Rooms(nStream, username);
                    ff.Show();
                    connection.Start();

                }
            }
            catch
            {
                MessageBox.Show("There is no connection ");
            }
        }



        public void listner()
        {
            while (true)
            {

                if (ff.Check == "closed")
                {
                    nStream.Close();
                    this.Close();
                    connection.Abort();


                }

            }

        }
       
    }
}
