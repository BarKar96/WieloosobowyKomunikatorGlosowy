using Ozeki.Media;
using Ozeki.VoIP;
using SimpleTCP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace WieloosobowyKomunikatorGlosowy_Serwer
{
    public partial class Server : Form
    {

        //Ozeki      
        public IPhoneCall call;
        public ISoftPhone softphone;   // softphone object
        public IPhoneLine phoneLine;
        public string local_ip;

        private List<Channel> channelList;


        string temp_name = null;
        public int whichChannel = -1;


        //TCP
        SimpleTcpServer server;

        //Database
        Database database;
       
        public Server()
        {    
            InitializeComponent();
            //Ozeki
            local_ip = GetLocalIPAddress();
            OzekiInitialization();
            //InitializeConferenceRoom();
            channelList = new List<Channel>();

            channelList.Add(new Channel("a","a", ChangeToSHA2_256("a")));
            channelList.Add(new Channel("b", "b", ChangeToSHA2_256("b")));
            channelList.Add(new Channel("c", "c", ChangeToSHA2_256("c")));
            channelList.Add(new Channel("d", "d", ChangeToSHA2_256("d")));

            //TCP
            setupTCPServer();
            this.database = new Database();
            Console.WriteLine("ip:" + local_ip);

        }
        private void setupTCPServer()
        {
            server = new SimpleTcpServer();
            server.Delimiter = 0x13; // enter
            server.StringEncoder = Encoding.UTF8;
            server.DataReceived += Server_DataReceived;
        }
        public string sendChannelInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("CHB;|");
            foreach(Channel ch in channelList)
            {
                sb.Append(ch.name);
                sb.Append(";");
                sb.Append(ch.userList.Count);
                sb.Append(";");
                sb.Append(ch.description);
                sb.Append(";");
                if (ch.password == ChangeToSHA2_256(""))
                    sb.Append("F");
                else
                    sb.Append("T");
                sb.Append("|");
            }
            return sb.ToString();

        }
        public void buildChannelMessageInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("CHI");
            sb.Append(";");
            sb.Append(whichChannel);
            sb.Append(";");
            sb.Append(channelList[whichChannel].name);
            sb.Append(";");
            sb.Append(channelList[whichChannel].userList.Count);
            sb.Append(";");
            sb.Append(channelList[whichChannel].description);
            foreach (User u in channelList[whichChannel].userList)
            {
                sb.Append(";");
                sb.Append(u.name);
            }
            
            server.Broadcast(sb.ToString());
        }
        private void Server_DataReceived(object sender, SimpleTCP.Message e)
        {
            string message = e.MessageString.Remove(e.MessageString.Length - 1);
            Char delimiter = ';';
            String[] substrings = message.Split(delimiter);
            Console.WriteLine(message);
            if (substrings[0] == "HI")
            {
                e.Reply(sendChannelInfo());

            }
            else if(substrings[1] == "CH")
            {
                whichChannel = Int32.Parse(substrings[2]);
                temp_name = substrings[0];
                if (channelList[whichChannel].password == substrings[3])
                    e.Reply("PASSOK");
                else
                    e.Reply("PASSNOK");

            }               
            else if (substrings[1] == "BYE")
            {
                whichChannel = Int32.Parse(substrings[2]);
                temp_name = substrings[0];
                
                buildChannelMessageInfo();
                e.Reply("BYE");
            }
            else if (substrings[0] == "REG")
            {
                bool answer = database.AddUser(substrings[1], substrings[2]);
                if (answer == true)
                    e.Reply("REGOK");
                else
                    e.Reply("REGNOK");
            }
            else if (substrings[0] == "LOG")
            {
                bool answer = database.CheckPassword(substrings[1], substrings[2]);
                if (answer == true)
                    e.Reply("LOGOK");
                else
                    e.Reply("LOGNOK");
            }
            else
            {
                Console.WriteLine("nieznany komunikat");
            }
        }

        public void OzekiInitialization()
        {
            softphone = SoftPhoneFactory.CreateSoftPhone(5000, 10000);
            var config = new DirectIPPhoneLineConfig(local_ip, 5060);
            phoneLine = softphone.CreateDirectIPPhoneLine(config);
            phoneLine.RegistrationStateChanged += line_RegStateChanged;
            softphone.IncomingCall += softphone_IncomingCall;
            softphone.RegisterPhoneLine(phoneLine);
        }


        void softphone_IncomingCall(object sender, VoIPEventArgs<IPhoneCall> e)
        {
            call = e.Item;            
            call.CallStateChanged += call_CallStateChanged;
            call.Answer();
        }

        void line_RegStateChanged(object sender, RegistrationStateChangedArgs e)
        {

            IPhoneCall call = sender as IPhoneCall;
            if (e.State == RegState.NotRegistered || e.State == RegState.Error)
                Console.WriteLine("Registration failed!");

            if (e.State == RegState.RegistrationSucceeded)
            {
                Console.WriteLine("Registration succeeded - Online!");                
            }
        }

        void call_CallStateChanged(object sender, CallStateChangedArgs e)
        {
            call = sender as IPhoneCall;
            if (e.State == CallState.Answered)
            {
                channelList[whichChannel].conferenceRoom.AddToConference(call);
                channelList[whichChannel].addUser(new User(temp_name, call.CallID));
                Console.WriteLine(temp_name + " added to channel: " + whichChannel);
                buildChannelMessageInfo();
                
            }
            else if (e.State.IsCallEnded())
            {
                channelList[whichChannel].conferenceRoom.RemoveFromConference(call);
                channelList[whichChannel].remUser(call.CallID);
                Console.WriteLine(temp_name + " removed from channel: " + whichChannel);
            }

        }
        
        public string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }
        

        private void button2_Click(object sender, EventArgs e)
        {
            //string z = GetLocalIPAddress();
            //System.Net.IPAddress ip = System.Net.IPAddress.Parse("192.168.1.15");
            System.Net.IPAddress ip = System.Net.IPAddress.Parse(local_ip);
            server.Start(ip, 8910);
            Console.WriteLine("server started");
        }


        private string ChangeToSHA2_256(string input)
        {
            using (SHA256Managed sha1 = new SHA256Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("X2"));
                }
                string result = sb.ToString();
                return result.ToLower();
            }
        }


        private void add_channel_Click(object sender, EventArgs e)
        {
            if (channelList.Exists(x => x.name == name_box.Text))
                MessageBox.Show("Istnieje kanał o tej samej nazwie");
            else
            {
                channelList.Add(new Channel(name_box.Text, description_box.Text, ChangeToSHA2_256(password_box.Text)));
                MessageBox.Show("Dodano kanał");
                name_box.Clear();
                description_box.Clear();
                password_box.Clear();
            }
        }


        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Server());
        }
    }
}



