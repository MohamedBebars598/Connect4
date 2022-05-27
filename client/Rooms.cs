using ePOSOne.btnProduct;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Connect_4_Project;
using System.Runtime.Serialization.Formatters.Binary;

namespace client
{
    public partial class Rooms : Form
    {

        private int joinBtnCount;
        int watchBtnCount;
        int listBxCount;
        int i, n;
        int joinIndex;
        int watchIndex;
        string colorp1;
        string colorp2;
        int rowBoard;
        int colBoard;
        string username;
        List<Button> joinbtns;
        List<ListBox> listBoxs;
        List<Button> watchBtns;
        StreamReader br;
        StreamWriter bw;
        BoardSizes boardShape;
        public Stream nstream;
        Thread listenThread;
        string noOfRooms;
        actAsTheGame plyrOneWait;
        GameBoard game;
        Boolean opened;
        Connect Myconnect;
        string status;




        public Rooms(Stream nStream, string usrnm)
        {
            InitializeComponent();
            joinBtnCount = 5;
            watchBtnCount = 5;
            listBxCount = 5;
            i = 1;
            n = 1;
            joinbtns = new List<Button>();
            listBoxs = new List<ListBox>();
            watchBtns = new List<Button>();
            username = usrnm;
            joinbtns.Add(join1);
            joinbtns.Add(join2);
            joinbtns.Add(join3);
            joinbtns.Add(join4);
            listBoxs.Add(listBox1);
            listBoxs.Add(listBox2);
            listBoxs.Add(listBox3);
            listBoxs.Add(listBox4);
            watchBtns.Add(watch1);
            watchBtns.Add(watch2);
            watchBtns.Add(watch3);
            watchBtns.Add(watch4);
            this.nstream = nStream;
            bw = new StreamWriter(nStream);
            br = new StreamReader(nStream);

            bw.AutoFlush = true;
            listBox1.Items.Add("Room 1");
            listBox2.Items.Add("Room 2");
            listBox3.Items.Add("Room 3");
            listBox4.Items.Add("Room 4");
            listBox1.Items.Add("No.of players : 0");
            listBox2.Items.Add("No.of players : 0");
            listBox3.Items.Add("No.of players : 0");
            listBox4.Items.Add("No.of players : 0");
            Control.CheckForIllegalCrossThreadCalls = false;
            //   button_WOC1.Enabled = false;
            opened = true;
            status = "opened";
        }

        public void listner()
        {


            actAsTheGame plyrTwoWait = null;


            while (opened)
            {
                string myResult = "";
                try
                {

                    myResult = br.ReadLine();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("By By");
                }
                if (myResult != null)
                {


                    //string myResul = "roomsAvailable:6";
                    if (myResult == "serverIsDisconnected")
                    {
                        MessageBox.Show("NO Connection");
                        Thread.Sleep(5000);
                        this.Close();
                        opened = false;
                    }
                    else if (myResult.Contains("roomsAvailable:"))           //  
                    {
                        Controls.Find("button_WOC1", true)[0].Enabled = true;
                        string[] spliter1 = myResult.Split(':');
                        noOfRooms = spliter1[1];
                        if (int.Parse(noOfRooms) > 4)
                        {
                            int lostRooms = int.Parse(noOfRooms) - 4;
                            for (int k = 0; k < lostRooms; k++)
                            {
                                Button joinBtn = new Button();
                                Button watchBtn = new Button();
                                ListBox lstBx = new ListBox();
                                // formatting of elements....
                                // joint button...
                                joinBtn.Size = new System.Drawing.Size(131, 60);
                                joinBtn.Location = new System.Drawing.Point(459, (i * 66) + join4.Location.Y);
                                joinBtn.Text = "join room" + (5 + k);
                                joinBtn.Name = "join" + (5 + k);
                                joinBtn.TabIndex = (5 + k);
                                joinBtn.Font = new System.Drawing.Font("Segoe Print", 12F, System.Drawing.FontStyle.Bold);
                                joinBtn.Click += new System.EventHandler(this.join_Click);
                                joinBtn.BackColor = Color.Gold;

                                joinbtns.Add(joinBtn);

                                // Watch button......
                                watchBtn.Name = "Watch" + (5 + k);
                                watchBtn.Location = new System.Drawing.Point(377, i * 66 + watch4.Location.Y);
                                watchBtn.Size = new System.Drawing.Size(76, 60);
                                watchBtn.Text = "Watch";
                                watchBtn.Font = new System.Drawing.Font("Segoe Print", 12F, System.Drawing.FontStyle.Bold);
                                watchBtn.TabIndex = (5 + k);
                                watchBtn.Enabled = false;
                                watchBtn.Click += new System.EventHandler(this.watch_Click);
                                watchBtn.BackColor = Color.Gray;
                                watchBtns.Add(watchBtn);

                                //listBx........
                                lstBx.Location = new System.Drawing.Point(141, i * 66 + listBox4.Location.Y);
                                lstBx.Size = new System.Drawing.Size(230, 60);
                                lstBx.Text = "askdnas";
                                lstBx.Name = "listBox" + (5 + k);
                                lstBx.Font = new System.Drawing.Font("Segoe Print", 12F, System.Drawing.FontStyle.Bold);
                                listBoxs.Add(lstBx);
                                lstBx.Items.Add("Room " + (5 + k));
                                lstBx.Items.Add("No.of players : 0");
                                i++;
                                // listBoxs[4+i - 1].Items[1] = "No.of players : 1";
                                if (this.InvokeRequired)
                                {
                                    this.BeginInvoke((MethodInvoker)delegate ()
                                    {
                                        this.Controls.Add(joinBtn);
                                        this.Controls.Add(lstBx);
                                        this.Controls.Add(watchBtn);
                                    });
                                }
                                else
                                {
                                    this.Controls.Add(joinBtn);
                                    this.Controls.Add(lstBx);
                                    this.Controls.Add(watchBtn);
                                }
                            }
                        }
                        for (int u = 1; u <= int.Parse(noOfRooms); u++)
                        {
                            bw.WriteLine("isfull:" + u);  ///// to checked 

                            string stt = br.ReadLine();
                            if (stt == "room_is_full")       //room_is_full
                            {
                                joinbtns[u - 1].Enabled = false;
                                joinbtns[u - 1].BackColor = Color.Gray;

                                watchBtns[u - 1].Enabled = true;
                                watchBtns[u - 1].BackColor = Color.SteelBlue;

                                listBoxs[u - 1].Items[1] = ("No.of players : 2");
                            }
                            else  // this  for checking if there is one player or not....                            
                            {
                                joinbtns[u - 1].Enabled = true;
                                joinbtns[u - 1].BackColor = Color.Gold;

                                watchBtns[u - 1].Enabled = false;
                                watchBtns[u - 1].BackColor = Color.Gray;

                                bw.WriteLine("playeroneExist:" + u);
                                string st = br.ReadLine();
                                if (st.Contains("player1_exist"))
                                {
                                    listBoxs[u - 1].Items[1] = ("No.of players : 1");


                                }
                                else if (st.Contains("player1_donnot_exist"))
                                {
                                    listBoxs[u - 1].Items[1] = ("No.of players : 0");
                                }
                            }
                        }
                    }
                    else if (myResult.Contains("player1_donnot_exist")) // player1_donnot_exist
                    {
                        string[] roomnum = myResult.Split(':');
                        int joinIndex = int.Parse(roomnum[1]);
                        CoinColors ff = new CoinColors();
                        DialogResult dResult;
                        dResult = ff.ShowDialog();
                        if (dResult == DialogResult.OK)
                        {
                            colorp1 = ff.col;
                        }
                        //playeronejoined:roomnumber:player1name:color
                        bw.WriteLine("playeronejoined:" + joinIndex + ":" + username + ":" + colorp1);
                    }// the response player_one_join_accepted:roomnumber
                    else if (myResult.Contains("player_one_join_accepted"))
                    {
                        string[] roomnum = myResult.Split(':');
                        int joinIndex = int.Parse(roomnum[1]);
                        string s = listBoxs[joinIndex - 1].Items[1].ToString();
                        if (s[s.Length - 1] == '0')
                        {
                            listBoxs[joinIndex - 1].Items[1] = "No.of players : 1";
                        }
                        else if (s[s.Length - 1] == '1')
                        {
                            listBoxs[joinIndex - 1].Items[1] = "No.of players : 2";
                            joinbtns[joinIndex - 1].Enabled = false;
                            joinbtns[joinIndex - 1].BackColor = Color.Gray;
                            watchBtns[joinIndex - 1].Enabled = true;
                            watchBtns[joinIndex - 1].BackColor = Color.SteelBlue;
                        }
                        if (this.InvokeRequired)
                        {
                            this.BeginInvoke((MethodInvoker)delegate ()
                            {

                                plyrOneWait = new actAsTheGame(this, joinIndex, username, colorp1);
                                plyrOneWait.Show();
                                this.Hide();
                            });
                        }
                        else
                        {
                            plyrOneWait = new actAsTheGame(this, joinIndex, username, colorp1);
                            plyrOneWait.Show();
                            this.Hide();
                        }

                    }
                    else if (myResult.Contains("player1_exist"))      //player1_exist so player two entered
                    {//getcolor:roomnumber:playernumber
                        string[] roomnum = myResult.Split(':');
                        int joinIndex = int.Parse(roomnum[1]);
                        bw.WriteLine("isfull:" + joinIndex);
                        string response = br.ReadLine();
                        if (response == "room_is_full")
                        {
                            joinbtns[joinIndex - 1].Enabled = false;
                            joinbtns[joinIndex - 1].BackColor = Color.Gray;
                            watchBtns[joinIndex - 1].Enabled = true;
                            watchBtns[joinIndex - 1].BackColor = Color.SteelBlue;
                            listBoxs[joinIndex - 1].Items[1] = ("No.of players : 2");
                        }
                        else
                        {
                            joinbtns[joinIndex - 1].Enabled = true;
                            listBoxs[joinIndex - 1].Items[1] = ("No.of players : 1");
                            bw.WriteLine("getcolor:" + joinIndex + ":player1");
                            string colorp1 = br.ReadLine();
                            if (colorp1.Contains("red"))
                            {
                                colorp2 = "yellow";
                            }
                            else
                            {
                                colorp2 = "red";
                            }
                            bw.WriteLine("playertwojoined:" + joinIndex + ":" + username + ":" + colorp2);  // 3awzen rkm el room mn 3bdalaa
                            plyrTwoWait = new actAsTheGame(this, joinIndex, username, colorp2);
                            if (this.InvokeRequired)
                            {
                                this.BeginInvoke((MethodInvoker)delegate ()
                                {
                                    plyrTwoWait.labelText = " waiting for the other player to accept your request...";
                                    plyrTwoWait.Show();
                                    this.Hide();
                                });
                            }
                            else
                            {
                                plyrTwoWait.labelText = "waiting for the other player to accept your request...";
                                plyrTwoWait.Show();
                                this.Hide();
                            }
                            this.Hide();
                        }
                    }
                    else if (myResult.Contains("playwith?:"))
                    {

                        string[] infoPlayer2 = myResult.Split(':');
                        string player2Name = infoPlayer2[1];
                        plyrOneWait.labelText = player2Name + " wnats to play with you.......";

                        if (this.InvokeRequired)
                        {
                            this.BeginInvoke((MethodInvoker)delegate ()
                            {
                                plyrOneWait.showPanel = true;
                            });
                        }
                        else
                        {
                            plyrOneWait.showPanel = true;
                        }
                    }


                    else if (myResult.Contains("acceptededToPlay"))
                    {

                        string[] info = myResult.Split(':');
                        int roomNum = int.Parse(info[1]);

                        bw.WriteLine("getsize:" + roomNum);
                        string ms = br.ReadLine();
                        if (ms.Contains("room" + roomNum + ":row:"))
                        {

                            rowBoard = Convert.ToInt32(ms.Split(':')[2]);
                            colBoard = Convert.ToInt32(ms.Split(':')[4]);

                        }
                        if (this.InvokeRequired)
                        {
                            this.BeginInvoke((MethodInvoker)delegate ()
                            {
                                game = new GameBoard(nstream, username, roomNum, "player2", colorp2, rowBoard, colBoard);
                                game.Show();

                                plyrTwoWait.Hide();
                            });
                        }
                        else
                        {
                            game = new GameBoard(nstream, username, roomNum, "player2", colorp2, rowBoard, colBoard);
                            game.Show();
                            plyrTwoWait.Hide();

                        }

                        this.Invoke(new MethodInvoker(plyrTwoWait.Hide));

                        listenThread.Abort();
                    }

                    // else if (myResult.Contains("player_one_join_accepted") || myResult.Contains("acceptededToPlay"))
                    //هنشيل accepted to play 

                    else if (myResult.Contains("yes"))  //Yes:4  //No:0
                    {
                        string[] roomnum = myResult.Split(':');
                        int roomNom = int.Parse(roomnum[1]);

                        CoinColors rr = new CoinColors();
                        DialogResult dResult;
                        dResult = rr.ShowDialog();
                        if (dResult == DialogResult.OK)
                        {
                            colorp1 = rr.col;
                            if (colorp1 == "red")
                            {
                                colorp2 = "yellow";
                            }
                            else
                            {
                                colorp2 = "red";
                            }
                        }
                        boardShape = new BoardSizes();
                        DialogResult ddres;
                        ddres = boardShape.ShowDialog();
                        if (ddres == DialogResult.OK)
                        {
                            colBoard = boardShape.colret;
                            rowBoard = boardShape.rowRet;
                        }

                        //button_WOC1.Enabled = true;
                        // newroomstarted:roomnumber:player1name:color:row:col
                        bw.WriteLine("newroomstarted:" + roomNom + ":" + username + ":" + colorp1 + ":" + rowBoard + ":" + colBoard);

                        Button joinBtn = new Button();
                        Button watchBtn = new Button();
                        ListBox lstBx = new ListBox();
                        // formatting of elements....
                        // joint button...
                        joinBtn.Size = new System.Drawing.Size(131, 60);
                        joinBtn.Location = new System.Drawing.Point(459, (i * 66) + join4.Location.Y);
                        joinBtn.Text = "join room" + roomNom;
                        joinBtn.Name = "join" + roomNom;
                        joinBtn.TabIndex = roomNom;
                        joinBtn.Font = new System.Drawing.Font("Segoe Print", 12F, System.Drawing.FontStyle.Bold);
                        joinBtn.Click += new System.EventHandler(this.join_Click);
                        joinBtn.BackColor = Color.Gold;

                        joinbtns.Add(joinBtn);

                        // Watch button......
                        watchBtn.Name = "Watch" + roomNom;
                        watchBtn.Location = new System.Drawing.Point(377, i * 66 + watch4.Location.Y);
                        watchBtn.Size = new System.Drawing.Size(76, 60);
                        watchBtn.Text = "Watch";
                        watchBtn.Font = new System.Drawing.Font("Segoe Print", 12F, System.Drawing.FontStyle.Bold);
                        watchBtn.TabIndex = roomNom;
                        watchBtn.Click += new System.EventHandler(this.watch_Click);

                        watchBtn.Enabled = false;
                        watchBtn.BackColor = Color.Gray;
                        watchBtns.Add(watchBtn);

                        //listBx........
                        lstBx.Location = new System.Drawing.Point(141, i * 66 + listBox4.Location.Y);
                        lstBx.Size = new System.Drawing.Size(230, 60);
                        lstBx.Text = "askdnas";
                        lstBx.Name = "listBox" + roomNom;
                        lstBx.Font = new System.Drawing.Font("Segoe Print", 12F, System.Drawing.FontStyle.Bold);
                        listBoxs.Add(lstBx);
                        lstBx.Items.Add("Room " + roomNom);
                        lstBx.Items.Add("No.of players : 0");
                        i++;
                        listBoxs[roomNom - 1].Items[1] = "No.of players : 1";
                        if (this.InvokeRequired)
                        {
                            this.BeginInvoke((MethodInvoker)delegate ()
                            {
                                this.Controls.Add(joinBtn);
                                this.Controls.Add(lstBx);
                                this.Controls.Add(watchBtn);
                            });
                        }
                        else
                        {
                            this.Controls.Add(joinBtn);
                            this.Controls.Add(lstBx);
                            this.Controls.Add(watchBtn);
                        }


                        //
                        if (this.InvokeRequired)
                        {
                            this.BeginInvoke((MethodInvoker)delegate ()
                            {

                                plyrOneWait = new actAsTheGame(this, roomNom, username, colorp1, rowBoard, colBoard);
                                plyrOneWait.Show();
                                this.Hide();

                            });
                        }
                        else
                        {
                            plyrOneWait = new actAsTheGame(this, roomNom, username, colorp1, rowBoard, colBoard);
                            plyrOneWait.Show();
                            this.Hide();
                        }


                    }

                    else if (myResult.Contains("no"))
                    {
                        MessageBox.Show("YOU CANNOT CREATE A ROOM !!");
                        //  Button_WOC1
                        Controls.Find("button_WOC1", true)[0].Enabled = false;
                        Controls.Find("button_WOC1", true)[0].BackColor = Color.Gray;
                    }

                    else if (myResult.Contains("can_Watch"))
                    {
                        string[] roomnum = myResult.Split(':');
                        int roomNom = int.Parse(roomnum[1]);
                        bw.WriteLine("getsize:" + roomNom);
                        string ms = br.ReadLine();
                        if (ms.Contains("room" + roomNom + ":row:"))
                        {

                            rowBoard = Convert.ToInt32(ms.Split(':')[2]);
                            colBoard = Convert.ToInt32(ms.Split(':')[4]);

                        }
                        bw.WriteLine($"watch:{roomNom}");
                        bw.WriteLine($"sendlocations:{roomNom}");
                        BinaryFormatter formatter;
                        formatter = new BinaryFormatter();
                        var rm = (List<int>[])formatter.Deserialize(nstream);
                        bw.WriteLine($"getcolor:{roomNom}:player1");
                        string Player1Color = br.ReadLine();
                        string c1;
                        if (Player1Color.Contains("red"))
                        {
                            c1 = "red";
                        }
                        else
                        {
                            c1 = "yellow";
                        }


                        if (this.InvokeRequired)
                        {
                            this.BeginInvoke((MethodInvoker)delegate ()
                            {

                                game = new GameBoard(nstream, username, roomNom, "watcher", c1, rowBoard, colBoard, rm);
                                game.Show();
                            });
                        }
                        else
                        {
                            game = new GameBoard(nstream, username, roomNom, "watcher", Player1Color, rowBoard, colBoard, rm);
                            game.Show();
                        }
                        this.Hide();
                    }//cannot_Watch:roomnumber
                    else if (myResult.Contains("cannot_Watch"))
                    {
                        MessageBox.Show("there is another watcher in the room !! ");
                    }
                }
            }
        }
        private void join_Click(object sender, EventArgs e)
        {
            joinIndex = ((sender as Button).TabIndex);   //1/2/3/4
            bw.WriteLine("playeroneExist:" + joinIndex);
        }
        private void watch_Click(object sender, EventArgs e)
        {//canwatch?:roomnumber
            watchIndex = ((sender as Button).TabIndex);
            bw.WriteLine("canwatch?:" + watchIndex);
        }
        private void btn_createroom_Click(object sender, EventArgs e)
        {
            bw.WriteLine("canImakeaRoom");
            //newroomstarted:roomnumber:player1name:color:row:col
            //      MessageBox.Show(button_WOC1.ToString());

        }

        private void join_MouseHover(object sender, EventArgs e)
        {
            join1.BackColor = Color.Yellow;
        }

        private void join1_MouseLeave(object sender, EventArgs e)
        {
            join1.BackColor = Color.Gold;
        }
        private void join2_MouseLeave(object sender, EventArgs e)
        {
            join2.BackColor = Color.Gold;
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            listenThread = new Thread(new ThreadStart(listner));
            listenThread.IsBackground = true;
            listenThread.Start();
            Control.CheckForIllegalCrossThreadCalls = false;
            bw.WriteLine("getAvailableRooms");
        }
        private void watch3_MouseHover(object sender, EventArgs e)
        {
            watch3.BackColor = Color.LightSteelBlue;
        }
        private void watch2_MouseLeave(object sender, EventArgs e)
        {
            watch2.BackColor = Color.SteelBlue;
        }
        private void watch4_MouseHover(object sender, EventArgs e)
        {
            watch4.BackColor = Color.LightSteelBlue;
        }

        private void watch4_MouseLeave(object sender, EventArgs e)
        {
            watch4.BackColor = Color.SteelBlue;
        }

        private void watch3_MouseLeave(object sender, EventArgs e)
        {
            watch3.BackColor = Color.SteelBlue;
        }
        private void watch1_MouseHover(object sender, EventArgs e)
        {

            watch1.BackColor = Color.LightSteelBlue;
        }

        private void watch1_MouseLeave(object sender, EventArgs e)
        {
            watch1.BackColor = Color.SteelBlue;
        }

        private void watch2_MouseHover(object sender, EventArgs e)
        {
            watch2.BackColor = Color.LightSteelBlue;
        }

        private void join3_MouseHover(object sender, EventArgs e)
        {
            join3.BackColor = Color.Yellow;
        }

        private void join3_MouseLeave(object sender, EventArgs e)
        {
            join3.BackColor = Color.Gold;
        }

        private void join4_MouseHover(object sender, EventArgs e)
        {
            join4.BackColor = Color.Yellow;
        }

        private void join4_MouseLeave(object sender, EventArgs e)
        {
            join4.BackColor = Color.Gold;
        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            bw.WriteLine("disconnect");
            status = "closed";
            opened = false;
      

        }
        private void Rooms_MouseClick(object sender, MouseEventArgs e)
        {
            bw.WriteLine("getAvailableRooms");

        }


        private void join2_MouseHover(object sender, EventArgs e)
        {
            join2.BackColor = Color.Yellow;
        }



        public string Check{
           
            get
            {

                return status; 


            }

          }
    }
}
