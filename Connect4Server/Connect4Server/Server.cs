using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Windows.Forms;



namespace Connect4Server
{


    public partial class server : Form
    {
        #region fields used to store data
        static TcpListener serverListner;
        IPAddress localaddr;
        const int LIMIT = 20; //20 concurrent clients
        StreamWriter fileWrite;
        StreamWriter fileWriteResult;
        List<Stream> listOfAll;
        string last_one;
        string resultpath;
        string logspath;
        static int counter;


        public struct roomsData
        {
            public string player1Name;
            public string player2Name;
            public string player1CoinColor;
            public string player2CoinColor;
            public int[] player1LastPoint;
            public int[] player2LastPoint;
            public string[] usersDataIP_Port;
            public string result;
            public int boardRowNum;
            public int boardColNum;
            public bool isFull;
            public bool isactive;
            public string whoseturn;
            public Stream player1stream;
            public Stream player2stream;
            public Stream watcherStream;
        }

        public static roomsData[] getRoomsdata
        {
            get { return roomData; }
        }

        public List<List<int>[]> roomsLocations;
        public List<int> player1Locations;
        public List<int> player2Locations;
        private const int CP_NOCLOSE_BUTTON = 0x200;
        public struct clientsDataIpPort
        {
            public string clientIP;
            public string clientport;
        }
        public static int roomcounter = 4;//number of ready rooms to join to(increments by creating another rooms).
        static int quit;
        public static roomsData[] roomData = new roomsData[LIMIT / 2];//store each room data         
        public static ArrayList clientsData = new ArrayList();//store each client data ip,port
        Thread[] myThread = new Thread[LIMIT];
        public static readonly object LockObj = new object();
        [SecurityPermissionAttribute(SecurityAction.Demand, ControlThread = true)]//for killing the threads

        #endregion
        public server()
        {
            InitializeComponent();
            btn_DisConnect.Enabled = false;
            Control.CheckForIllegalCrossThreadCalls = false;
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            fileWrite = File.AppendText(path + @"\connect4logs.txt");
            fileWriteResult = File.AppendText(path + @"\connect4Result.txt");
            resultpath = path + @"\connect4Result.txt";
            logspath = path + @"\connect4logs.txt";
            fileWrite.AutoFlush = true;
            fileWriteResult.AutoFlush = true;
            listOfAll = new List<Stream>();

            roomsLocations = new List<List<int>[]>();

            for (int i = 0; i < LIMIT / 2; i++)
            {
                roomsLocations.Add(new List<int>[2]);
            }
            for (int i = 0; i < LIMIT / 2; i++)
            {
                player1Locations = new List<int>();
                player2Locations = new List<int>();
                roomsLocations[i][0] = player1Locations;
                roomsLocations[i][1] = player2Locations;
            }
            for (int i = 0; i < LIMIT / 2; i++)
            {
                roomData[i].usersDataIP_Port = new string[4];
            }
            #region test to get ip of machine (wifi)
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    last_one = ip.ToString();
                }
            }
            #endregion
            quit = 1;
            counter = 0;
        }

        private void recieveData()
        {
            Stream nStream = null;
            Socket soc = null;
            while (quit != 0)
            {
                try
                {
                    soc = serverListner.AcceptSocket();//throw exception to close waiting socket and current thread
                }
                catch (Exception)
                {
                    //nothing to do,just catch the exception

                }

                if (soc != null)
                {

                    if (quit != 0)
                    {
                        logmsg.Items.Add("Connected: " + soc.RemoteEndPoint + " at " + DateTime.Now.ToString());
                        logmsg.SelectedIndex = logmsg.Items.Count - 1;
                        counter++;
                        lbl_counter.Text = counter.ToString();
                        //fileWrite.WriteLineAsync("\nConnected: " + soc.RemoteEndPoint + " at " + DateTime.Now.ToString());
                        #region store clients data
                        clientsDataIpPort client = new clientsDataIpPort();
                        client.clientIP = soc.RemoteEndPoint.ToString().Split(':')[0];
                        client.clientport = soc.RemoteEndPoint.ToString().Split(':')[1];
                        clientsData.Add(client);
                        #endregion
                    }

                    try
                    {
                        #region Handling clients area (main code)
                        if (soc != null && soc.Connected)
                        {
                            nStream = new NetworkStream(soc);
                            listOfAll.Add(nStream);//record all streams to use them in server closing
                            StreamReader SRead = new StreamReader(nStream, Encoding.UTF8);
                            StreamWriter SWrite = new StreamWriter(nStream);
                            SWrite.AutoFlush = true; // enable automatic flushing
                            SWrite.WriteLine("connected");
                            while (quit != 0)
                            {
                                int Introomnumber = 0;
                                if (SRead != null)
                                {
                                    if (nStream != null)//in case of recieving more than one time "disconnect" from the same client(bug fix)
                                    {
                                        #region test
                                        /*int chars= SRead.Read();
                                         string message="";                                       
                                         while (chars != Convert.ToInt32('\r')) {
                                             message += Convert.ToChar(chars);
                                             chars= SRead.Read();
                                         }*/
                                        #endregion
                                        string message = SRead.ReadLine();
                                        if (message != null)
                                        {
                                            if (message.Contains("disconnect"))
                                            {
                                                counter--;
                                                lbl_counter.Text = counter.ToString();
                                                break;
                                            }
                                            else if (message.Contains("newroomstarted:"))//for room creation
                                            {//"newroomstarted:1:mrx:red:6:7"
                                                if (roomcounter <= (LIMIT / 2))//check if there's available rooms 
                                                {
                                                    string roomnumber = message.Split(':')[1];
                                                    Introomnumber = Convert.ToInt32(roomnumber);
                                                    Introomnumber--;
                                                    //store players data
                                                    if (roomData[Introomnumber].player1Name == "" || roomData[Introomnumber].player1Name == null)
                                                    {
                                                        //store ip and port number                                                   
                                                        //ip player1
                                                        roomData[Introomnumber].usersDataIP_Port[0] = soc.RemoteEndPoint.ToString().Split(':')[0];
                                                        //port player1
                                                        roomData[Introomnumber].usersDataIP_Port[1] = soc.RemoteEndPoint.ToString().Split(':')[1];
                                                        //name
                                                        roomData[Introomnumber].player1Name = message.Split(':')[2];//name
                                                                                                                    //MessageBox.Show(soc.RemoteEndPoint.ToString());
                                                        roomData[Introomnumber].player1CoinColor = message.Split(':')[3];
                                                        roomData[Introomnumber].boardRowNum = Convert.ToInt32(message.Split(':')[4]);//row
                                                        roomData[Introomnumber].boardColNum = Convert.ToInt32(message.Split(':')[5]);//col
                                                        roomData[Introomnumber].isactive = true;

                                                        player1Locations = new List<int>();
                                                        roomsLocations[Introomnumber][0] = player1Locations;

                                                        roomData[Introomnumber].player1stream = new NetworkStream(soc);
                                                        roomData[Introomnumber].player1LastPoint = new int[2];

                                                    }

                                                    logmsg.Items.Add($"Room number {roomnumber} started");
                                                    logmsg.Items.Add($"Player 1 (creator) : {roomData[Introomnumber].player1Name}");
                                                    //fileWrite.WriteLineAsync("\n" + DateTime.Now.ToString() + $"\nnew room started : player1{roomData[Introomnumber].player1Name}");
                                                    roomcounter++;
                                                    SWrite.WriteLine("room_created");//if available
                                                }
                                                else if (roomcounter > (LIMIT / 2))
                                                {

                                                }
                                            }
                                            else if (message.Contains("playeronejoined:")) //player one joining defined room
                                            { //"playeronejoined:1:mrx:red"

                                                //"playeronejoined:1:mrx:red"
                                                string roomnumber = message.Split(':')[1];
                                                Introomnumber = Convert.ToInt32(roomnumber);
                                                Introomnumber--;

                                                //ip player1
                                                roomData[Introomnumber].usersDataIP_Port[0] = soc.RemoteEndPoint.ToString().Split(':')[0];
                                                //port player1
                                                roomData[Introomnumber].usersDataIP_Port[1] = soc.RemoteEndPoint.ToString().Split(':')[1];
                                                //name
                                                roomData[Introomnumber].player1Name = message.Split(':')[2];
                                                roomData[Introomnumber].player1CoinColor = message.Split(':')[3];
                                                roomData[Introomnumber].isactive = true;
                                                roomData[Introomnumber].player1LastPoint = new int[2];
                                                logmsg.Items.Add($"player 1 {roomData[Introomnumber].player1Name} joined");
                                                logmsg.Items.Add($"at room {roomnumber} ");

                                                player1Locations = new List<int>();
                                                roomsLocations[Introomnumber][0] = player1Locations;

                                                roomData[Introomnumber].player1stream = new NetworkStream(soc);
                                                //fileWrite.WriteLineAsync("\n" + DateTime.Now.ToString() + $"\nplayer 1 {roomData[Introomnumber].player1Name} joined");
                                                SWrite.WriteLine($"player_one_join_accepted:{roomnumber}");

                                            }
                                            else if (message.Contains("playertwojoined:"))//player two joining defined room
                                            {
                                                //"playertwojoined:1:ahmed:red"
                                                string roomnumber = message.Split(':')[1];
                                                Introomnumber = Convert.ToInt32(roomnumber);
                                                Introomnumber--;
                                                //ip player2
                                                roomData[Introomnumber].usersDataIP_Port[2] = soc.RemoteEndPoint.ToString().Split(':')[0];
                                                //port player2
                                                roomData[Introomnumber].usersDataIP_Port[3] = soc.RemoteEndPoint.ToString().Split(':')[1];
                                                //name
                                                roomData[Introomnumber].player2Name = message.Split(':')[2];
                                                roomData[Introomnumber].player2CoinColor = message.Split(':')[3];
                                                roomData[Introomnumber].isFull = true;

                                                roomData[Introomnumber].player2LastPoint = new int[2];

                                                player2Locations = new List<int>();
                                                roomsLocations[Introomnumber][1] = player2Locations;

                                                //for test
                                                if (roomData[Introomnumber].player1stream.Equals(roomData[Introomnumber].player2stream))
                                                {
                                                    MessageBox.Show("Stream conflict");
                                                }
                                                //end test

                                                logmsg.Items.Add($"player 2 {roomData[Introomnumber].player2Name} joined");
                                                logmsg.Items.Add($"at room {roomnumber} ");
                                                roomData[Introomnumber].player2stream = new NetworkStream(soc);
                                                //test
                                                //send request to player 1
                                                StreamWriter sww;
                                                sww = new StreamWriter(roomData[Introomnumber].player1stream);
                                                sww.AutoFlush = true;
                                                sww.WriteLine($"playwith?:{roomData[Introomnumber].player2Name}");
                                                //test
                                                // fileWrite.WriteLineAsync("\n" + DateTime.Now.ToString() + $"\nplayer 1 {roomData[Introomnumber].player2Name} joined");

                                            }
                                            else if (message.Contains("isfull:")) //check if is full
                                            {
                                                //"isfull:1"
                                                string roomnumber = message.Split(':')[1];
                                                Introomnumber = Convert.ToInt32(roomnumber);
                                                Introomnumber--;
                                                if (roomData[Introomnumber].isFull)
                                                {
                                                    SWrite.WriteLine("room_is_full");
                                                }
                                                else
                                                {
                                                    SWrite.WriteLine("room_is_not_full");
                                                }
                                            }
                                            else if (message.Contains("playeroneExist:"))
                                            {
                                                //"playeroneExist:roomnumber"
                                                string roomnumber = message.Split(':')[1];
                                                Introomnumber = Convert.ToInt32(roomnumber);
                                                Introomnumber--;
                                                if (roomData[Introomnumber].player1Name != null)
                                                {
                                                    SWrite.WriteLine($"player1_exist:{roomnumber}");
                                                }
                                                else
                                                {
                                                    SWrite.WriteLine($"player1_donnot_exist:{roomnumber}");
                                                }

                                            }
                                            else if (message.Contains("playertwoExist:"))
                                            {
                                                //"playertwoExist:roomnumber"
                                                string roomnumber = message.Split(':')[1];
                                                Introomnumber = Convert.ToInt32(roomnumber);
                                                Introomnumber--;
                                                if (roomData[Introomnumber].player2Name != null)
                                                {
                                                    SWrite.WriteLine($"player2_exist:{roomnumber}");
                                                }
                                                else
                                                {
                                                    SWrite.WriteLine($"player2_donnot_exist:{roomnumber}");
                                                }
                                            }
                                            else if (message.Contains("locationchanged:"))
                                            {
                                                //"locationchanged:1:player1:2:3"
                                                string roomnumber = message.Split(':')[1];
                                                Introomnumber = Convert.ToInt32(roomnumber);
                                                Introomnumber--;
                                                string player = message.Split(':')[2];//player number
                                                if (player == "player1")
                                                {
                                                    //player 1 row
                                                    roomData[Introomnumber].player1LastPoint[0] = Convert.ToInt32(message.Split(':')[3]);
                                                    //player 1 col
                                                    roomData[Introomnumber].player1LastPoint[1] = Convert.ToInt32(message.Split(':')[4]);
                                                    //player 2 turn
                                                    roomData[Introomnumber].whoseturn = "player2";
                                                    #region send data about new location
                                                    StreamWriter sww;
                                                    sww = new StreamWriter(roomData[Introomnumber].player2stream);
                                                    sww.AutoFlush = true;
                                                    int row = roomData[Introomnumber].player1LastPoint[0];
                                                    int col = roomData[Introomnumber].player1LastPoint[1];

                                                    roomsLocations[Introomnumber][0].Add(row);
                                                    roomsLocations[Introomnumber][0].Add(col);



                                                    sww.WriteLine("player1");
                                                    sww.WriteLine($"player1At:row:{row}:col:{col}");

                                                    #region test
                                                    //string loc = $"player1At:row:{row}:col:{col}";
                                                    //foreach (var letter in loc)
                                                    //{
                                                    //    if (letter != '\n') 
                                                    //    {
                                                    //        sww.Write(letter);
                                                    //    }

                                                    //}
                                                    //sww.Write('\r');
                                                    //sww.Flush();
                                                    #endregion

                                                    //fileWrite.WriteLineAsync("\n" + DateTime.Now.ToString() + $"\nplayer 1 played at row:{row}:col:{col}");
                                                    //##watcher

                                                    if (roomData[Introomnumber].watcherStream != null)
                                                    {
                                                        StreamWriter swatcher;
                                                        swatcher = new StreamWriter(roomData[Introomnumber].watcherStream);
                                                        swatcher.AutoFlush = true;
                                                        //swatcher.WriteLine("watcher");
                                                        swatcher.WriteLine($"player1At:row:{row}:col:{col}");

                                                        swatcher.WriteLine($"player1At:row:{row}:col:{col}");
                                                    }
                                                    //test area
                                                    if (roomData[Introomnumber].player1stream.Equals(roomData[Introomnumber].player2stream))
                                                    {
                                                        MessageBox.Show("Stream conflict");
                                                    }
                                                    //end test
                                                    #endregion


                                                }
                                                else if (player == "player2")
                                                {
                                                    //player 2 row
                                                    roomData[Introomnumber].player2LastPoint[0] = Convert.ToInt32(message.Split(':')[3]);
                                                    //player 2 col
                                                    roomData[Introomnumber].player2LastPoint[1] = Convert.ToInt32(message.Split(':')[4]);
                                                    //player 1 turn
                                                    #region send data about new location
                                                    roomData[Introomnumber].whoseturn = "player1";

                                                    StreamWriter sww;
                                                    sww = new StreamWriter(roomData[Introomnumber].player1stream);
                                                    sww.AutoFlush = true;
                                                    int row = roomData[Introomnumber].player2LastPoint[0];
                                                    int col = roomData[Introomnumber].player2LastPoint[1];

                                                    roomsLocations[Introomnumber][1].Add(row);
                                                    roomsLocations[Introomnumber][1].Add(col);
                                                    #region test
                                                    //string loc = $"player2At:row:{row}:col:{col}";
                                                    //foreach (var letter in loc)
                                                    //{
                                                    //    if (letter != '\n') 
                                                    //    {
                                                    //        sww.Write(letter);
                                                    //    }                                                                                                       
                                                    //}
                                                    //sww.Write('\r');
                                                    //sww.Flush();
                                                    #endregion
                                                    //sww.WriteLine("player2");

                                                    sww.WriteLine($"player2At:row:{row}:col:{col}");
                                                    sww.WriteLine($"player2At:row:{row}:col:{col}");

                                                    //fileWrite.WriteLineAsync("\n" + DateTime.Now.ToString() + $"\nplayer 2 played at row:{row}:col:{col}");

                                                    if (roomData[Introomnumber].watcherStream != null)
                                                    {
                                                        StreamWriter swatcher;
                                                        swatcher = new StreamWriter(roomData[Introomnumber].watcherStream);
                                                        swatcher.AutoFlush = true;
                                                        //swatcher.WriteLine("watcher");

                                                        swatcher.WriteLine($"player2At:row:{row}:col:{col}");
                                                        swatcher.WriteLine($"player2At:row:{row}:col:{col}");
                                                    }
                                                    //test area
                                                    if (roomData[Introomnumber].player1stream.Equals(roomData[Introomnumber].player2stream))
                                                    {
                                                        MessageBox.Show("Stream conflict");
                                                    }
                                                    //end test

                                                    #endregion

                                                    // sendlocationsUpdates(Introomnumber);
                                                }
                                            }
                                            else if (message.Contains("sendlocations:"))
                                            {
                                                //"sendlocations:roomnumber"
                                                string roomnumber = message.Split(':')[1];
                                                Introomnumber = Convert.ToInt32(roomnumber);
                                                Introomnumber--;
                                                BinaryFormatter serializer = new BinaryFormatter();
                                                //send list of all location to the watcher
                                                if (roomData[Introomnumber].watcherStream != null)
                                                {
                                                    serializer.Serialize(roomData[Introomnumber].watcherStream, roomsLocations[Introomnumber]);

                                                }

                                            }
                                            else if (message.Contains("isactive:"))
                                            {
                                                //"isactive:roomnumber"
                                                string roomnumber = message.Split(':')[1];
                                                Introomnumber = Convert.ToInt32(roomnumber);
                                                Introomnumber--;
                                                if (roomData[Introomnumber].isactive)
                                                {
                                                    SWrite.WriteLine("room_is_active");
                                                }
                                                else
                                                {
                                                    SWrite.WriteLine("room_is_not_active");
                                                }

                                            }
                                            else if (message.Contains("leave:"))
                                            {

                                                //"leave:roomnumber:playernumber"
                                                //"leave:roomnumber:watcher"
                                                string roomnumber = message.Split(':')[1];
                                                Introomnumber = Convert.ToInt32(roomnumber);
                                                Introomnumber--;
                                                string playernumber = message.Split(':')[2];
                                                if (playernumber == "player1")
                                                {
                                                    roomData[Introomnumber].player1Name = null;
                                                    roomData[Introomnumber].player1CoinColor = null;
                                                    roomData[Introomnumber].player1LastPoint = null;
                                                    roomData[Introomnumber].usersDataIP_Port[0] = null;
                                                    roomData[Introomnumber].usersDataIP_Port[1] = null;
                                                    roomData[Introomnumber].isFull = false;
                                                    if (roomData[Introomnumber].player2Name == null)
                                                    {
                                                        roomData[Introomnumber].isactive = false;
                                                    }
                                                    roomsLocations[Introomnumber][0].Clear();
                                                    //fileWrite.WriteLineAsync("\n" + DateTime.Now.ToString() + $"\nplayer 1 left room {Introomnumber}");

                                                    if (roomData[Introomnumber].player2Name != null)
                                                    {
                                                        StreamWriter sww;
                                                        sww = new StreamWriter(roomData[Introomnumber].player2stream);
                                                        sww.AutoFlush = true;
                                                        sww.WriteLine("playerOneRefusedToRePlay");
                                                    }

                                                }
                                                else if (playernumber == "player2")
                                                {
                                                    roomData[Introomnumber].player2Name = null;
                                                    roomData[Introomnumber].player2CoinColor = null;
                                                    roomData[Introomnumber].player2LastPoint = null;
                                                    roomData[Introomnumber].usersDataIP_Port[2] = null;
                                                    roomData[Introomnumber].usersDataIP_Port[3] = null;
                                                    roomData[Introomnumber].isFull = false;
                                                    if (roomData[Introomnumber].player1Name == null)
                                                    {
                                                        roomData[Introomnumber].isactive = false;
                                                    }
                                                    roomsLocations[Introomnumber][1].Clear();

                                                    //fileWrite.WriteLineAsync("\n" + DateTime.Now.ToString() + $"\nplayer 2 left room {Introomnumber}");

                                                    if (roomData[Introomnumber].player1Name != null)
                                                    {
                                                        StreamWriter sww;
                                                        sww = new StreamWriter(roomData[Introomnumber].player1stream);
                                                        sww.AutoFlush = true;
                                                        sww.WriteLine("playerTwoRefusedToRePlay");
                                                    }
                                                }
                                                else if (playernumber == "watcher")
                                                {
                                                    roomData[Introomnumber].watcherStream = null;
                                                    //fileWrite.WriteLineAsync("\n" + DateTime.Now.ToString() + $"\nwatcher left room {Introomnumber}");

                                                }

                                            }
                                            else if (message.Contains("getcolor:"))
                                            {
                                                //"getcolor:roomnumber:playernumber"
                                                string roomnumber = message.Split(':')[1];
                                                Introomnumber = Convert.ToInt32(roomnumber);
                                                Introomnumber--;
                                                string playernumber = message.Split(':')[2];
                                                if (playernumber == "player1")
                                                {
                                                    if (roomData[Introomnumber].player1CoinColor != null)
                                                    {
                                                        SWrite.WriteLine("player1Col:" + roomData[Introomnumber].player1CoinColor);
                                                    }
                                                }
                                                else if (playernumber == "player2")
                                                {
                                                    if (roomData[Introomnumber].player2CoinColor != null)
                                                    {
                                                        SWrite.WriteLine("player2Col:" + roomData[Introomnumber].player2CoinColor);
                                                    }
                                                }
                                            }
                                            else if (message.Contains("getTurn:"))
                                            {
                                                //"getTurn:roomnumber"
                                                string roomnumber = message.Split(':')[1];
                                                Introomnumber = Convert.ToInt32(roomnumber);
                                                Introomnumber--;
                                                if (roomData[Introomnumber].whoseturn == "player1")
                                                {
                                                    SWrite.WriteLine("player1Turn");
                                                }
                                                else if (roomData[Introomnumber].whoseturn == "player2")
                                                {
                                                    SWrite.WriteLine("player2Turn");
                                                }
                                            }
                                            else if (message.Contains("getAvailableRooms"))
                                            {
                                                SWrite.WriteLine("roomsAvailable:" + roomcounter.ToString());
                                            }
                                            else if (message.Contains("getname:"))
                                            {
                                                //"getname:roomnumber:playernumber"
                                                string roomnumber = message.Split(':')[1];
                                                Introomnumber = Convert.ToInt32(roomnumber);
                                                Introomnumber--;
                                                string playernumber = message.Split(':')[2];
                                                if (playernumber == "player1")
                                                {
                                                    SWrite.WriteLine(roomData[Introomnumber].player1Name);
                                                }
                                                else if (playernumber == "player2")
                                                {
                                                    SWrite.WriteLine(roomData[Introomnumber].player2Name);
                                                }

                                            }
                                            else if (message.Contains("result:"))
                                            {
                                                //result:roomnumbner:winnerplayernumber
                                                string roomnumber = message.Split(':')[1];
                                                Introomnumber = Convert.ToInt32(roomnumber);
                                                Introomnumber--;
                                                string playernumber = message.Split(':')[2];
                                                if (playernumber == "player1")
                                                {
                                                    roomData[Introomnumber].result = "player1wins";
                                                    #region send data about winner
                                                    StreamWriter sww;
                                                    sww = new StreamWriter(roomData[Introomnumber].player2stream);
                                                    sww.AutoFlush = true;
                                                    sww.WriteLine("player1wins");
                                                    //fileWriteResult.WriteLineAsync($"player 1 {roomData[Introomnumber].player1Name} wins in room {roomnumber} at {DateTime.Now.ToString()} on player 2 {roomData[Introomnumber].player2Name}");
                                                    writetexttofile($"player 1 {roomData[Introomnumber].player1Name} wins in room {roomnumber} at {DateTime.Now.ToString()} on player 2 {roomData[Introomnumber].player2Name}");

                                                    //send to watcher
                                                    if (roomData[Introomnumber].watcherStream != null)
                                                    {
                                                        sww = new StreamWriter(roomData[Introomnumber].watcherStream);
                                                        sww.AutoFlush = true;
                                                        sww.WriteLine("player1wins");
                                                    }

                                                    //fileWrite.WriteLineAsync("\n" + DateTime.Now.ToString() + $"\nplayer 1 winns at room {Introomnumber}");

                                                    #endregion
                                                }
                                                else if (playernumber == "player2")
                                                {
                                                    roomData[Introomnumber].result = "player2wins";
                                                    #region send data about winner
                                                    StreamWriter sww;
                                                    sww = new StreamWriter(roomData[Introomnumber].player1stream);
                                                    sww.AutoFlush = true;
                                                    sww.WriteLine("player2wins");
                                                    //fileWriteResult.WriteLineAsync($"player 2 {roomData[Introomnumber].player2Name} wins in room {roomnumber} at {DateTime.Now.ToString()} on player 1 {roomData[Introomnumber].player1Name}");
                                                    writetexttofile($"player 2 {roomData[Introomnumber].player2Name} wins in room {roomnumber} at {DateTime.Now.ToString()} on player 1 {roomData[Introomnumber].player1Name}");
                                                    //send to watcher
                                                    if (roomData[Introomnumber].watcherStream != null)
                                                    {
                                                        sww = new StreamWriter(roomData[Introomnumber].watcherStream);
                                                        sww.AutoFlush = true;
                                                        sww.WriteLine("player2wins");
                                                    }

                                                    // fileWrite.WriteLineAsync("\n" + DateTime.Now.ToString() + $"\nplayer 2 winns at room {Introomnumber}");

                                                    #endregion

                                                }
                                            }
                                            else if (message.Contains("playagain:"))
                                            {
                                                //"playagain:roomnumber:PlayerWhoSendWheRequest"
                                                string roomnumber = message.Split(':')[1];
                                                Introomnumber = Convert.ToInt32(roomnumber);
                                                Introomnumber--;
                                                string playernumber = message.Split(':')[2];
                                                if (playernumber == "player1")
                                                {
                                                    if (roomData[Introomnumber].player2Name != null)
                                                    {
                                                        #region send data about winner
                                                        StreamWriter sww;
                                                        sww = new StreamWriter(roomData[Introomnumber].player2stream);
                                                        sww.AutoFlush = true;
                                                        sww.WriteLine("player1requestReplay");
                                                        #endregion
                                                    }

                                                }
                                                else if (playernumber == "player2")
                                                {
                                                    if (roomData[Introomnumber].player1Name != null)
                                                    {
                                                        #region send data about winner
                                                        StreamWriter sww;
                                                        sww = new StreamWriter(roomData[Introomnumber].player1stream);
                                                        sww.AutoFlush = true;
                                                        sww.WriteLine("player2requestReplay");
                                                        #endregion
                                                    }

                                                }
                                            }
                                            else if (message.Contains("agreeToReplay:"))
                                            {
                                                //"agreeToReplay:roomnumber:playernumberwhoAgrred"
                                                string roomnumber = message.Split(':')[1];
                                                Introomnumber = Convert.ToInt32(roomnumber);
                                                Introomnumber--;
                                                string playernumber = message.Split(':')[2];
                                                if (playernumber == "player1")
                                                {
                                                    #region send data about winner
                                                    StreamWriter sww;
                                                    sww = new StreamWriter(roomData[Introomnumber].player2stream);
                                                    sww.AutoFlush = true;
                                                    sww.WriteLine("\0");
                                                    sww.WriteLine("player1AgreedReplay");
                                                    #endregion
                                                }
                                                else if (playernumber == "player2")
                                                {
                                                    #region send data about winner
                                                    StreamWriter sww;
                                                    sww = new StreamWriter(roomData[Introomnumber].player1stream);
                                                    sww.AutoFlush = true;
                                                    sww.WriteLine("\0");
                                                    sww.WriteLine("player2AgreedReplay");
                                                    #endregion
                                                }
                                            }
                                            else if (message.Contains("refusedToReplay:"))
                                            {
                                                //refusedToReplay:roomnumber:playernumber
                                                string roomnumber = message.Split(':')[1];
                                                Introomnumber = Convert.ToInt32(roomnumber);
                                                Introomnumber--;
                                                string playernumber = message.Split(':')[2];
                                                if (playernumber == "player1")
                                                {
                                                    #region send data about
                                                    if (roomData[Introomnumber].player2Name != null)
                                                    {
                                                        StreamWriter sww;
                                                        sww = new StreamWriter(roomData[Introomnumber].player2stream);
                                                        sww.AutoFlush = true;
                                                        //sendmessage(sww, "playerOneRefusedToRePlay");
                                                        sww.WriteLine("playerOneRefusedToRePlay");
                                                    }
                                                    #endregion test
                                                }
                                                else if (playernumber == "player2")
                                                {
                                                    #region send data about
                                                    if (roomData[Introomnumber].player1Name != null)
                                                    {
                                                        StreamWriter sww;
                                                        sww = new StreamWriter(roomData[Introomnumber].player1stream);
                                                        sww.AutoFlush = true;
                                                        //sendmessage(sww, "playerTwoRefusedToRePlay");                                              
                                                        sww.WriteLine("playerTwoRefusedToRePlay");

                                                    }
                                                    #endregion test
                                                }

                                            }
                                            else if (message.Contains("watch:"))
                                            {
                                                //watch:roomnumber
                                                string roomnumber = message.Split(':')[1];
                                                Introomnumber = Convert.ToInt32(roomnumber);
                                                Introomnumber--;
                                                roomData[Introomnumber].watcherStream = new NetworkStream(soc);
                                                //fileWrite.WriteLineAsync("\n" + DateTime.Now.ToString() + $"\nWatcher at room {Introomnumber}");
                                            }
                                            else if (message.Contains("getsize:"))
                                            {
                                                //"getsize:roomnumber
                                                string roomnumber = message.Split(':')[1];
                                                Introomnumber = Convert.ToInt32(roomnumber);
                                                Introomnumber--;
                                                //SWrite.WriteLine("player");

                                                SWrite.WriteLine($"room{roomnumber}:row:{roomData[Introomnumber].boardRowNum}:col:{roomData[Introomnumber].boardColNum}");

                                            }
                                            else if (message.Contains("canImakeaRoom"))
                                            {
                                                if (roomcounter < (LIMIT / 2))
                                                {
                                                    SWrite.WriteLine($"yes:{roomcounter + 1}");//room that will be created
                                                                                               //no increment in case of no creation
                                                }
                                                else
                                                {
                                                    SWrite.WriteLine("no:0");
                                                }

                                            }
                                            else if (message.Contains("acceptToPlay:"))
                                            {
                                                //acceptToPlay:roomnumber:player1name
                                                string roomnumber = message.Split(':')[1];
                                                Introomnumber = Convert.ToInt32(roomnumber);
                                                Introomnumber--;
                                                string playerName = message.Split(':')[2];
                                                //send acceptance to player 2
                                                StreamWriter sww;
                                                sww = new StreamWriter(roomData[Introomnumber].player2stream);
                                                sww.AutoFlush = true;
                                                sww.WriteLine($"acceptededToPlay:{roomnumber}:{roomData[Introomnumber].player1Name}");
                                                //fileWrite.WriteLineAsync("\n" + DateTime.Now.ToString() + $"\nplayer 1 accepted player 2 at room {Introomnumber}");


                                            }
                                            else if (message.Contains("refusedToPlay:"))
                                            {
                                                //refusedToPlay:roomnumber:player1name
                                                string roomnumber = message.Split(':')[1];
                                                Introomnumber = Convert.ToInt32(roomnumber);
                                                Introomnumber--;
                                                string playerName = message.Split(':')[2];
                                                //send refuseing to player 2
                                                StreamWriter sww;
                                                sww = new StreamWriter(roomData[Introomnumber].player2stream);
                                                sww.AutoFlush = true;
                                                sww.WriteLine($"refusedToPlay:{roomnumber}:{roomData[Introomnumber].player1Name}");
                                                //delete player 2 data froom the room
                                                roomData[Introomnumber].player2Name = null;
                                                roomData[Introomnumber].player2CoinColor = null;
                                                roomData[Introomnumber].player2LastPoint = null;
                                                roomData[Introomnumber].usersDataIP_Port[2] = null;
                                                roomData[Introomnumber].usersDataIP_Port[3] = null;
                                                roomData[Introomnumber].isFull = false;
                                                if (roomData[Introomnumber].player1Name == null)
                                                {
                                                    roomData[Introomnumber].isactive = false;
                                                }
                                                //fileWrite.WriteLineAsync("\n" + DateTime.Now.ToString() + $"\nplayer 1 refused player 2 at room {Introomnumber}");
                                                //delete player 2 data
                                            }
                                            else if (message.Contains("canwatch?:"))
                                            {
                                                //canwatch?:roomnumber
                                                string roomnumber = message.Split(':')[1];
                                                Introomnumber = Convert.ToInt32(roomnumber);
                                                Introomnumber--;
                                                if (roomData[Introomnumber].watcherStream != null)
                                                {
                                                    SWrite.WriteLine($"cannot_Watch:{roomnumber}");
                                                }
                                                else if (roomData[Introomnumber].watcherStream == null)
                                                {
                                                    SWrite.WriteLine($"can_Watch:{roomnumber}");
                                                }
                                            }
                                            logmsg.SelectedIndex = logmsg.Items.Count - 1;
                                        }

                                    }
                                }


                            }
                        }
                    }
                    #endregion
                    #region catch and record the exception's area
                    catch (Exception err)
                    {
                        #region Store exceptions data
                        string ErrorlineNo, Errormsg;
                        ////MessageBox.Show(err.Message);
                        ErrorlineNo = err.StackTrace.Substring(err.StackTrace.Length - 7, 7);
                        Errormsg = err.ToString();
                        try
                        {
                            fileWrite.WriteAsync("\nexception at (" + (ErrorlineNo) + ") msg => " + Errormsg);
                        }
                        catch (Exception)
                        {

                            //do nothig
                        }

                        #endregion
                    }
                    finally
                    {
                        if (soc != null)
                        {

                            try
                            {
                                logmsg.Items.Add("Disconnected: " + soc.RemoteEndPoint + " at " + DateTime.Now.ToString());
                                logmsg.SelectedIndex = logmsg.Items.Count - 1;
                                //fileWrite.WriteLineAsync("\n" + DateTime.Now.ToString() + $" Disconnected:  {soc.RemoteEndPoint}");
                            }
                            catch (Exception)
                            {
                                //do nothig
                            }

                            nStream.Close();
                            soc.Close();
                        }

                    }
                    #endregion area

                }

            }

        }

        #region functions
        private void writetexttofile(string text)
        {
            try
            {
                Thread.Sleep(500);
                fileWriteResult.WriteLineAsync(text);
            }
            catch (Exception)
            {
                //do nothing
            }


        }
        private void sendmessage(StreamWriter sww, string message)
        {
            sww.WriteLine(message);
        }
        private void sendlocationsUpdates(int roomnumber)
        {
            //used to test
            /*StreamWriter sww;
            sww = new StreamWriter(roomData[roomnumber].player1stream);
            sww.AutoFlush = true;
            int row = roomData[roomnumber].player2LastPoint[0];
            int col = roomData[roomnumber].player2LastPoint[1];

            roomsLocations[roomnumber][1].Add(row);
            roomsLocations[roomnumber][1].Add(col);
           
            //sww.WriteLine($"player2At:row:{row}:col:{col}");
            sww.WriteLine($"player2At:row:{row}:col:{col}");*/

        }
        private void btn_Connect_Click(object sender, EventArgs e)
        {
            btn_close.Enabled = false;
            btn_DisConnect.Enabled = true;
            string[] last_one2 = last_one.Split('.');
            #region Ips to test
            //byte[] bt = new byte[] { Convert.ToByte(last_one2[0]), Convert.ToByte(last_one2[1]), Convert.ToByte(last_one2[2]), Convert.ToByte(last_one2[3]) };
            //byte[] bt = new byte[] { 127, 0, 0, 1 };
            //byte[] bt = new byte[] { 192,168,56,1 };
            #endregion
            //localaddr = new IPAddress(bt);
            //int port = 13000;
            int port = Convert.ToInt32(GetOpenPort());
            //serverListner = new TcpListener(localaddr, port);
            serverListner = new TcpListener(IPAddress.Parse(last_one), port);
            #region showing used IP,Port on logs msg
            //string IpShowen ="";
            //foreach (var item in bt)
            //{
            //    IpShowen += item+ " ";
            //}
            //IpShowen.TrimEnd('.');
            //logmsg.Items.Add($"Server IP => { last_one} , Port => {port}");
            lbl_SrvIpPrt.Text = $"Server IP => { last_one} , Port => {port}";
            #endregion
            try
            {
                serverListner.Start();
                logmsg.Items.Add($"Server started and ready to handle {LIMIT} players: ");
                fileWrite.WriteLineAsync("\n" + DateTime.Now.ToString() + "\nServer started");
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            for (int i = 0; i < LIMIT; i++)
            {
                myThread[i] = new Thread(new ThreadStart(recieveData));
                myThread[i].IsBackground = true;
                myThread[i].Start();
            }
            //initialize rooms size
            for (int i = 0; i < roomcounter; i++)
            {
                roomData[i].boardRowNum = 6;
                roomData[i].boardColNum = 7;
            }
            btn_Connect.Enabled = false;

        }
        private void btn_DisConnect_Click(object sender, EventArgs e)
        {
            try
            {
                //to handle the disconnect from btn of form closing 
                quit = 0;
                serverListner.Stop();//for killing listning threads by throwing an excpetion
                fileWrite.WriteLine(DateTime.Now.ToString() + "\nServer Disconnected ");
                StreamWriter disWrite = null;
                for (int i = 0; i < listOfAll.Count; i++)
                {
                    if (listOfAll[i] != null)
                    {
                        if (listOfAll[i].CanWrite)
                        {
                            disWrite = new StreamWriter(listOfAll[i]);
                            disWrite.AutoFlush = true;
                            disWrite.WriteLine("serverIsDisconnected");
                            disWrite.Close();
                        }
                    }
                }


                for (int i = 0; i < (LIMIT / 2); i++)
                {
                    if (roomData[i].player1stream != null)
                    {
                        roomData[i].player1stream.Close();
                    }
                    if (roomData[i].player2stream != null)
                    {
                        roomData[i].player2stream.Close();
                    }
                    if (roomData[i].watcherStream != null)
                    {
                        roomData[i].watcherStream.Close();
                    }

                }

                fileWrite.Flush();
                if (fileWrite != null)
                {
                    fileWrite.Close();
                }
                this.Close();
            }
            catch (Exception err)
            {
                #region Store exceptions data
                string ErrorlineNo, Errormsg;
                ErrorlineNo = err.StackTrace.Substring(err.StackTrace.Length - 7, 7);
                Errormsg = err.ToString();
                if (fileWrite != null)
                {
                    fileWrite.WriteAsync("\nexception at (" + (ErrorlineNo) + ") msg => " + Errormsg);
                }
                #endregion

            }
        }
        private void btn_close_Click(object sender, EventArgs e)
        {
            fileWrite.Close();
            fileWriteResult.Close();
            this.Close();
        }
        //Hide close button
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }
        private void btn_restart_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.Show();
        }
        private string GetOpenPort()
        {
            int PortStartIndex = 1000;
            int PortEndIndex = 65000;
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] tcpEndPoints = properties.GetActiveTcpListeners();

            List<int> usedPorts = tcpEndPoints.Select(p => p.Port).ToList<int>();
            int unusedPort = 0;

            for (int port = PortStartIndex; port < PortEndIndex; port++)
            {
                if (!usedPorts.Contains(port))
                {
                    unusedPort = port;
                    break;
                }
            }
            return unusedPort.ToString();
        }
        private void showDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataOfRooms dataOfRooms = new DataOfRooms();
            dataOfRooms.Show();
        }
        #endregion
    }

}

