using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Connect_4_Project;


namespace client
{
    public partial class actAsTheGame : Form
    {
        GameBoard game;
        string plyr1Name;
        int roomNumber;
        Rooms room;
        string color;
        int Row;
        int Column;
     
        public actAsTheGame(Rooms r,int nu=0,string na="none",string col="",int R=6,int c=7  )
        {
            InitializeComponent();
            roomNumber = nu;
            plyr1Name = na;
            panel1.Visible = false;
            Control.CheckForIllegalCrossThreadCalls = false;
            room = r;
            color = col;
            Row = R;
            Column = c;
         
        }

        public String labelText
        {
            set
            {
                Waiting.Text = value; 
            }
        }

        public bool showPanel
        {
            set
            {
                panel1.Visible = value;
            }
        }

        private void actAsTheGame_Load(object sender, EventArgs e)
        {
          
        }

        private void Waiting_TextChanged(object sender, EventArgs e) // 
        {
            //if (Waiting.Text.Contains("Waiting"))
            //{
            //    panel1.Visible = false;
            //}
            //else
            //{
            //    panel1.Visible = true;
            //}
        }

        //acceptToPlay:roomnumber:player1Name


        private void button_WOC1_Click(object sender, EventArgs e) // accept
        {
            //MessageBox.Show(plyr1Name+" "+roomNumber);
           
                StreamWriter bw = new StreamWriter(room.nstream);
                bw.AutoFlush = true;
                bw.WriteLine("acceptToPlay:" + roomNumber + ":" + plyr1Name);  
                game = new GameBoard(room.nstream, plyr1Name, roomNumber, "player1", color,Row,Column);
                game.Show();
                this.Hide();                    
        }

        private void button_WOC2_Click(object sender, EventArgs e) // refuse 
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void ActAsTheGame_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}
