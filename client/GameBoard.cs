using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;
using client;
namespace Connect_4_Project
{
    public partial class GameBoard : Form
    {
        Brush BackGroundBrush = Brushes.DarkBlue;
        Brush EmptyEllipseBrush = Brushes.White;
        int Rows;
        int columns;
        int Boardwidth;
        int Boardheight;
        int SmallRectHeight;
        int SmallRectWidth;
        int EllipseWidth;
        int EllipseHeight;
        int X_pointEllipse;
        int Y_pointEllipse;
        int[,] Board;
        Brush Player1Brush;
        Brush Player2Brush;
        int winner;
        bool isDraw = false;
        int turn;
        Boolean Myturn;
        int RoomNumber;
        int MyFormPlayer;
        StreamReader SRead;
        StreamWriter SWrite;
        static int quit;
        Stream NetStream;
        string userTp;
        Thread myThread;
        string Mycolor;
        List<int>[] roomLocation;
        string MyuserName;

        public GameBoard(Stream nStream, string nm, int rn, string userType, string Mycol, int r, int c, List<int>[] rm = null)
        {
            InitializeComponent();
            this.Text = (userType + ":" + nm);
            Control.CheckForIllegalCrossThreadCalls = false;
            panel1.Visible = false;
            myThread = new Thread(new ThreadStart(listen));
            quit = 1;
            NetStream = nStream;
            SRead = new StreamReader(NetStream);
            SWrite = new StreamWriter(NetStream);
            SWrite.AutoFlush = true;
            userTp = userType;
            RoomNumber = rn;
            MyuserName = nm;
            myThread.IsBackground = true;
            myThread.Start();
            Mycolor = Mycol;
            Rows = r;
            columns = c;
            Board = new int[Rows, columns]; // creation of the board
            roomLocation = rm;
            DetectInitialData();  //Function To Process The Data Came from the constructor

        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawBoard(e);
            DrawWatcherBoard();

        }
        private void DrawWatcherBoard()
        {
            if (MyFormPlayer == 0)
            {

                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        Graphics gra = this.CreateGraphics();
                        if (Board[i, j] == 1)
                        {

                            gra.FillEllipse(Player1Brush, (X_pointEllipse + SmallRectWidth * j), (Y_pointEllipse + SmallRectHeight * i), EllipseWidth, EllipseHeight);
                        }
                        else if (Board[i, j] == 2)
                        {
                            gra.FillEllipse(Player2Brush, (X_pointEllipse + SmallRectWidth * j), (Y_pointEllipse + SmallRectHeight * i), EllipseWidth, EllipseHeight);

                        }
                    }
                }
            }
        }
        public void listen()
        {
            while (quit != 0)
            {
                string msg = SRead.ReadLine();
                if (msg != null)
                {
                    if (msg.Contains("player1At:")) //player1 played At specific Location
                    {
                        int R = Convert.ToInt32(msg.Split(':')[2]);
                        int C = Convert.ToInt32(msg.Split(':')[4]);
                        Board[R, C] = 1;
                        turn = 1;
                        DrawEllipse(R, C);
                        winner = WinnerPlayer(1);
                        if (winner == 1) //Check If he Won
                        {
                            if (MyFormPlayer != 0) //I'm player
                            {
                                if (this.InvokeRequired)
                                {
                                    this.BeginInvoke((MethodInvoker)delegate ()
                                    {
                                        label1.Text = "Sorry!! " + MyuserName + " You " + "Lost ";
                                        panel1.Visible = true;
                                    });
                                }
                                else
                                {
                                    label1.Text = "Sorry!! " + MyuserName + " You " + "Lost ";
                                    //MessageBox.Show();
                                    panel1.Visible = true;
                                }
                            }
                            else if (MyFormPlayer == 0) //watcher
                            {
                                if (this.InvokeRequired)
                                {
                                    this.BeginInvoke((MethodInvoker)delegate ()
                                    {
                                        label1.Text = "Player 1 Won";
                                        panel1.Visible = true;
                                    });
                                }
                            }
                        }
                        isDraw = PlayerDraw();
                        if (isDraw == true)
                        {
                            if (this.InvokeRequired)
                            {
                                this.BeginInvoke((MethodInvoker)delegate ()
                                {
                                    label1.Text = "Draw!";
                                    panel1.Visible = true;
                                });
                            }
                        }  
                        

                    }
                    else if (msg.Contains("player2At:"))
                    {
                        int R = Convert.ToInt32(msg.Split(':')[2]);
                        int C = Convert.ToInt32(msg.Split(':')[4]);
                        Board[R, C] = 2;
                        turn = 2;
                        DrawEllipse(R, C);
                        winner = WinnerPlayer(2);
                        if (winner == 2)
                        {
                            if (MyFormPlayer != 0) //I'm player
                            {
                                if (this.InvokeRequired)
                                {
                                    this.BeginInvoke((MethodInvoker)delegate ()
                                    {
                                        label1.Text = "Sorry!! " + MyuserName + " You " + "Lost ";
                                        panel1.Visible = true;
                                    });
                                }
                                else
                                {
                                    label1.Text = "Sorry!! " + MyuserName + " You " + "Lost ";
                                    panel1.Visible = true;
                                }
                            }
                            else if (MyFormPlayer == 0) //watcher
                            {
                                this.BeginInvoke((MethodInvoker)delegate ()
                                {
                                    label1.Text = "Player 2 Won";
                                    panel1.Visible = true;
                                });
                            }
                        }
                        isDraw = PlayerDraw();
                        if (isDraw == true)
                        {
                            if (this.InvokeRequired)
                            {
                                this.BeginInvoke((MethodInvoker)delegate ()
                                {
                                    label1.Text = "Draw!";
                                    panel1.Visible = true;
                                });
                            }
                        }
                    }
                    else if (msg.Contains("player1wins:"))
                    {
                        MessageBox.Show("sorry you lost");
                    }
                    else if (msg.Contains("player2wins:"))
                    {
                        MessageBox.Show("sorry you lost");
                    }
                    else if (msg.Contains("playerOneRefusedToRePlay") && MyFormPlayer == 2)
                    {
                        panel1.Visible = false;
                        MessageBox.Show("player 1 refused to play with you ");
                        // Thread.Sleep(3000);
                      
                        if (this.InvokeRequired)
                        {
                            this.BeginInvoke((MethodInvoker)delegate ()
                            {
                                SWrite.WriteLine("leave:" + RoomNumber + ":" + "player" + MyFormPlayer);

                                this.Hide();
                                Rooms rm = new Rooms(NetStream, MyuserName);
                                rm.Show();
                                quit = 0;
                            });
                        }
                       
                    }

                    else if (msg.Contains("playerTwoRefusedToRePlay") && MyFormPlayer == 1)
                    {
                        msg = " ";
                        panel1.Visible = false;
                        MessageBox.Show("player 2 refused to play with you ");
                        // Thread.Sleep(3000);
                       
                        if (this.InvokeRequired)
                        {
                            this.BeginInvoke((MethodInvoker)delegate ()
                            {
                                SWrite.WriteLine("leave:" + RoomNumber + ":" + "player" + MyFormPlayer);

                                this.Hide();
                                Rooms rm = new Rooms(NetStream, MyuserName);
                                rm.Show();
                                quit = 0;
                            });
                        }
                    }
                    else if (msg.Contains("player1requestReplay") && MyFormPlayer == 2)
                    {

                        DialogResult dlgres = MessageBox.Show("Player 1 request to play again, do you want to play again?", "Re-play request", MessageBoxButtons.YesNo);
                        if (dlgres == DialogResult.Yes)
                        {
                            panel1.Visible = false;
                            Array.Clear(Board, 0, Board.Length);



                            this.Invalidate();
                        }
                        else if (dlgres == DialogResult.No)
                        {

                            leave_refuse();
                        }
                        else
                        {
                            leave_refuse();

                        }

                    }
                    else if (msg.Contains("player2requestReplay") && MyFormPlayer == 1)
                    {
                        DialogResult dlgres = MessageBox.Show("Player 2 request to play again, do you want to play again?", "Re-play request", MessageBoxButtons.YesNo);
                        if (dlgres == DialogResult.Yes)
                        {
                            panel1.Visible = false;
                            Array.Clear(Board, 0, Board.Length);
                            this.Invalidate();
                        }
                        else if (dlgres == DialogResult.No)
                        {

                            leave_refuse();
                        }
                        else
                        {
                            leave_refuse();

                        }
                    }
                    else if (msg.Contains("player1AgreedReplay") && MyFormPlayer == 2)
                    {
                        panel1.Visible = false;

                        Array.Clear(Board, 0, Board.Length);
                        this.Invalidate();
                    }
                    else if (msg.Contains("player2AgreedReplay") && MyFormPlayer == 1)
                    {
                        panel1.Visible = false;
                        Array.Clear(Board, 0, Board.Length);
                        this.Invalidate();

                    }
                    if (MyFormPlayer == 0) //I'm watcher
                    {
                        if (msg.Contains("player1wins"))
                        {
                            label1.Text = "Player 1 Won";
                            panel1.Visible = true;
                            //MessageBox.Show("Player 1 Won");
                        }
                        else if (msg.Contains("player2wins"))
                        {
                            label1.Text = "Player 2 Won";
                            panel1.Visible = true;
                            //MessageBox.Show("Player 2 Won");
                        }
                    }
                }
            }
        }
        private void DrawBoard(PaintEventArgs e)
        {
            //Drawing the Base board 
            Boardwidth = Convert.ToInt32(this.Width * 0.8);
            Boardheight = Convert.ToInt32(this.Height * 0.8);
            SmallRectWidth = Boardwidth / columns;
            SmallRectHeight = Boardheight / Rows;
            EllipseWidth = Convert.ToInt32(SmallRectWidth * 0.8);
            EllipseHeight = Convert.ToInt32(SmallRectHeight * 0.8);
            X_pointEllipse = Convert.ToInt32(SmallRectWidth * 0.1);
            Y_pointEllipse = Convert.ToInt32(SmallRectHeight * 0.1);
            e.Graphics.FillRectangle(BackGroundBrush, 0, 0, Boardwidth, Boardheight);
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    e.Graphics.FillEllipse(EmptyEllipseBrush, (X_pointEllipse + SmallRectWidth * j), (Y_pointEllipse + SmallRectHeight * i), EllipseWidth, EllipseHeight);
                }
            }
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            //this.Invalidate();
        }
        private void DetectInitialData()
        {//Handling the Initial Data came through the constructor
            if (userTp == "player1")
            {
                Myturn = true; //To Start playing
                MyFormPlayer = 1;
                //--------------Initialize Color---------------------
                if (Mycolor == "red")
                {
                    Player1Brush = Brushes.Red;
                    Player2Brush = Brushes.Yellow;
                }
                else if (Mycolor == "yellow")
                {
                    Player1Brush = Brushes.Yellow;
                    Player2Brush = Brushes.Red;
                }
            }
            else if (userTp == "player2")
            {
                MyFormPlayer = 2;
                Myturn = false;
                if (Mycolor == "red")
                {
                    Player1Brush = Brushes.Yellow;
                    Player2Brush = Brushes.Red;
                }
                else if (Mycolor == "yellow")
                {
                    Player1Brush = Brushes.Red;
                    Player2Brush = Brushes.Yellow;
                }
            }
            else if (userTp == "watcher")
            {
                button1.Visible = true;
                MyFormPlayer = 0;
                if (Mycolor == "red")
                {
                    Player1Brush = Brushes.Red;
                    Player2Brush = Brushes.Yellow;
                }
                else if (Mycolor == "yellow")
                {
                    Player1Brush = Brushes.Yellow;
                    Player2Brush = Brushes.Red;
                }

                //Save The old Already played array to Draw The Board To The watcher
                for (int i = 0; i < roomLocation[0].Count; i += 2)
                {   //player1 
                    int row = roomLocation[0][i];
                    int col = roomLocation[0][i + 1];
                    Board[row, col] = 1;
                }
                for (int i = 0; i < roomLocation[1].Count; i += 2)
                {   //player 2
                    int row = roomLocation[1][i];
                    int col = roomLocation[1][i + 1];
                    Board[row, col] = 2;
                }
            }
        }
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            int ColumnIndex, RowIndex;
            if (MyFormPlayer != 0)
            {
                ColumnIndex = GetColumnIndex(e);
                if (Myturn)
                {
                    if (ColumnIndex != -1)
                    {
                        RowIndex = GetEmptyRow(ColumnIndex);
                        if (RowIndex != -1) // there is at least one ellipse empty in the column
                        {
                            if (MyFormPlayer == 1)
                            {
                                Board[RowIndex, ColumnIndex] = 1;
                            }
                            else if (MyFormPlayer == 2)
                            {
                                Board[RowIndex, ColumnIndex] = 2;
                            }
                            DrawEllipse(RowIndex, ColumnIndex);
                            SWrite.WriteLine("locationchanged:" + RoomNumber + ":player" + MyFormPlayer + ":" + RowIndex + ":" + ColumnIndex);
                            Myturn = false;
                        }
                        winner = WinnerPlayer(MyFormPlayer);
                        if (winner != -1)
                        {
                            if (winner == MyFormPlayer)
                            {
                                SWrite.WriteLine("result:" + RoomNumber + ":" + "player" + MyFormPlayer);
                                //MessageBox.Show("Congratulations!! " + MyuserName + " You " + "won ♥ ");
                                label1.Text = "Congratulations!! " + MyuserName + " You " + "won ♥ ";
                                panel1.Visible = true;
                                //Application.Restart();
                            }
                        }
                        isDraw = PlayerDraw();
                        if (isDraw==true)
                        {
                            label1.Text = "Draw!";
                            panel1.Visible = true;
                        }
                         if (RowIndex == -1)
                        {
                            MessageBox.Show("This column is Full !");
                        }
                    }
                    if (ColumnIndex == -1)
                    {
                        MessageBox.Show("Please Click on the right Column !");
                    }
                }
                else if (!Myturn)
                {
                    MessageBox.Show("Not your Turn");
                }

            }
            if (MyFormPlayer == 0)
            {
                MessageBox.Show("You Can't Play, Just Watch The Game");
            }
        }
        private int WinnerPlayer(int playerToCheck)
        {
            //Check Virtical
            for (int i = 0; i < Rows - 3; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (AllNumberEqual(playerToCheck, Board[i, j], Board[i + 1, j], Board[i + 2, j], Board[i + 2, j], Board[i + 3, j]))
                    {
                        return playerToCheck; //player won
                    }
                }
            }
            //Check horizontal
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < columns - 3; j++)
                {
                    if (AllNumberEqual(playerToCheck, Board[i, j], Board[i, j + 1], Board[i, j + 2], Board[i, j + 3]))
                    {
                        return playerToCheck;//player won
                    }
                }
            }
            //top left diagonal
            for (int i = 0; i < Rows - 3; i++)
            {
                for (int j = 0; j < columns - 3; j++)
                {
                    if (AllNumberEqual(playerToCheck, Board[i, j], Board[i + 1, j + 1], Board[i + 2, j + 2], Board[i + 3, j + 3]))
                    {
                        return playerToCheck;//player won
                    }
                }
            }
            //top right diagonal
            for (int i = 0; i < Rows - 3; i++)
            {
                for (int j = 3; j < columns; j++)
                {
                    if (AllNumberEqual(playerToCheck, Board[i, j], Board[i + 1, j - 1], Board[i + 2, j - 2], Board[i + 3, j - 3]))
                    {
                        return playerToCheck;//player won
                    }
                }
            }
            return -1;
        }
        private bool AllNumberEqual(int num, params int[] numbers)
        {
            //Check the equality of numbers 
            foreach (int i in numbers)
            {
                if (i != num)
                {
                    return false;
                }
            }
            return true;
        }
        private int GetColumnIndex(MouseEventArgs e)
        {
            //Get the Index of column Clicked on
            for (int i = 0; i < columns; i++)
            {
                if ((e.X >= Boardwidth * i / columns) && (e.X < Boardwidth * (i + 1) / columns))
                {
                    return i;
                }
            }
            return -1;
        }
        private int GetEmptyRow(int col)
        {
            //Get the Index of an empty row to define the empty ellipse 

            for (int i = Rows - 1; i >= 0; i--)
            {
                if (Board[i, col] == 0)
                {
                    return i;
                }
            }
            return -1;
        }
        private void DrawEllipse(int row, int column)
        {
            Graphics g = this.CreateGraphics();
            //Drawing Changes For players
            if (MyFormPlayer != 0) //I'm player
            {
                if (Myturn)
                {
                    if (MyFormPlayer == 1)
                    {
                        g.FillEllipse(Player1Brush, (X_pointEllipse + SmallRectWidth * column), (Y_pointEllipse + SmallRectHeight * row), EllipseWidth, EllipseHeight);
                    }
                    else if (MyFormPlayer == 2)
                    {
                        g.FillEllipse(Player2Brush, (X_pointEllipse + SmallRectWidth * column), (Y_pointEllipse + SmallRectHeight * row), EllipseWidth, EllipseHeight);
                    }
                }
                else if (!Myturn)
                {
                    if (MyFormPlayer == 1)
                    {
                        g.FillEllipse(Player2Brush, (X_pointEllipse + SmallRectWidth * column), (Y_pointEllipse + SmallRectHeight * row), EllipseWidth, EllipseHeight);
                    }
                    else if (MyFormPlayer == 2)
                    {
                        g.FillEllipse(Player1Brush, (X_pointEllipse + SmallRectWidth * column), (Y_pointEllipse + SmallRectHeight * row), EllipseWidth, EllipseHeight);
                    }
                    Myturn = true;
                }
            }
            //Drawing Changes For Watcher
            else if (MyFormPlayer == 0) //I'm Watcher
            {
                if (turn == 1)
                {
                    g.FillEllipse(Player1Brush, (X_pointEllipse + SmallRectWidth * column), (Y_pointEllipse + SmallRectHeight * row), EllipseWidth, EllipseHeight);
                }
                else if (turn == 2)
                {
                    g.FillEllipse(Player2Brush, (X_pointEllipse + SmallRectWidth * column), (Y_pointEllipse + SmallRectHeight * row), EllipseWidth, EllipseHeight);
                }

                if (turn == 1)
                {
                    turn = 2;
                }
                else if (turn == 2)
                {
                    turn = 1;
                }

            }
        }
        private void GameBoard_FormClosing(object sender, FormClosingEventArgs e)
        {
            quit = 0;
            SWrite.WriteLine("leave:" + RoomNumber + ":" + "player" + MyFormPlayer);

        }
        private void Button1_Click(object sender, EventArgs e)
        {

        }
        private void Button_WOC1_Click(object sender, EventArgs e)  //accept
        {
            playeragain();
        }
        private void playeragain()
        {
            if (MyFormPlayer == 1)
            {

                SWrite.WriteLine($"playagain:{RoomNumber}:player1");
            }
            else if (MyFormPlayer == 2)
            {
                SWrite.WriteLine($"playagain:{RoomNumber}:player2");
            }
            else if (MyFormPlayer == 0)
            {
                if (this.InvokeRequired)
                {
                    this.BeginInvoke((MethodInvoker)delegate ()
                    {
                        label1.Text = "Sorry!! You Can't Play";
                        panel1.Visible = true;
                    });
                }
            }
            panel1.Visible = false;
            Array.Clear(Board, 0, Board.Length);
            this.Invalidate();
        }
        private void Button_WOC2_Click(object sender, EventArgs e)  // leave: 
        {

            if (MyFormPlayer != 0) //player
            {
                //leave:roomnumber:playernumber
                //refusedToReplay:roomnumber: playernumber
                //SWrite.WriteLine($"refusedToReplay:{RoomNumber}:player{MyFormPlayer}");
                //SWrite.WriteLine("leave:" + RoomNumber + ":" + "player" + MyFormPlayer);
                //this.Close();
                //Rooms rm = new Rooms(NetStream, MyuserName);
                //rm.Show();
                leave_refuse();

            }
            else if (MyFormPlayer == 0)//watcher
            {
                //leave:roomnumber:watcher
                SWrite.WriteLine("leave:" + RoomNumber + ":watcher");
                this.Close();
                Rooms rm = new Rooms(NetStream, MyuserName);
                rm.Show();
            }
            quit = 0;
        }
        private void Button1_Click_1(object sender, EventArgs e)
        {

            //    //leave::roomnumber:watcher

            SWrite.WriteLine("leave:" + RoomNumber + ":watcher");

            this.Close();
            Rooms rm = new Rooms(NetStream, MyuserName);
            rm.Show();

            quit = 0;
        }

        private void leave_refuse()
        {
            SWrite.WriteLine($"refusedToReplay:{RoomNumber}:player{MyFormPlayer}");
            SWrite.WriteLine("leave:" + RoomNumber + ":" + "player" + MyFormPlayer);
            this.Hide();
            Rooms rm = new Rooms(NetStream, MyuserName);
            rm.Show();

        }
        private bool PlayerDraw()
        {
            for (int i=0;i<Rows ;i++)
            {
                for (int j=0; j < columns; j++)
                {
                    if (Board[i,j]==0)
                    {
                        return false;
                    }

                }
            }
                        return true;
        }
    }
}
