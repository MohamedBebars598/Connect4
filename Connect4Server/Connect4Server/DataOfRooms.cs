using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Connect4Server
{
    public partial class DataOfRooms : Form
    {
        public DataOfRooms()
        {
            InitializeComponent();
        }

        private void DataOfRooms_Load(object sender, EventArgs e)
        {

            refresh();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            refresh();
        }

        private void refresh() {
            lst_roomsData.Items.Clear();          
            for (int i = 0; i < server.getRoomsdata.Length; i++)
            {
                if (server.getRoomsdata[i].isactive)
                {
                    if (server.getRoomsdata[i].isFull)
                    {
                        lst_roomsData.Items.Add($"Room number => {i + 1} | Number of players => 2 ");
                    }
                    else
                    {
                        lst_roomsData.Items.Add($"Room number => {i + 1} | Number of players => 1 ");
                    }

                }
            }
        }
    }
}
