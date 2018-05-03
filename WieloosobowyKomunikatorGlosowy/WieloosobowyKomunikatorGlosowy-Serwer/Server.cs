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

namespace WieloosobowyKomunikatorGlosowy_Serwer
{
    public partial class Server : Form
    {

        //Ozeki
        static int xyz = 0;
        public IPhoneCall call;
        public ISoftPhone softphone;   // softphone object
        public IPhoneLine phoneLine;
        public string local_ip;

        public List<ConferenceRoom> conferenceRoomlist;

        List<ClientCall> callList;

        public int whichChannel = 0;

        //TCP
        SimpleTcpServer server;
       
        public Server()
        {    
            InitializeComponent();
            
            //Ozeki
            callList = new List<ClientCall>();
            local_ip = GetLocalIPAddress();
            OzekiInitialization();
            //InitializeConferenceRoom();
            conferenceRoomlist = new List<ConferenceRoom>();
            conferenceRoomlist.Add(new ConferenceRoom());
            conferenceRoomlist.Add(new ConferenceRoom());

            //TCP
            setupTCPServer();

        }

        private void setupTCPServer()
        {
            server = new SimpleTcpServer();
            server.Delimiter = 0x13; // enter
            server.StringEncoder = Encoding.UTF8;
            server.DataReceived += Server_DataReceived;
        }

        private void Server_DataReceived(object sender, SimpleTCP.Message e)
        {
            string message = e.MessageString.Remove(e.MessageString.Length - 1);
            Console.WriteLine(e.MessageString);
            if(message == "ch1")
            {
                whichChannel = 0;
                e.Reply("OK");
                
            }
            else if (message == "ch2")
            {
                whichChannel = 1;
                e.Reply("OK");
                
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


        void InitializeConferenceRoom()
        {
            for (int i =0; i<conferenceRoomlist.Count; i++)
            {
                conferenceRoomlist[i] = new ConferenceRoom();
            }
    
        }


        void softphone_IncomingCall(object sender, VoIPEventArgs<IPhoneCall> e)
        {
            call = e.Item;            
            callList.Add(new ClientCall(call.CallID, call));
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
               
                    conferenceRoomlist[whichChannel].AddToConference(call);
                    Console.WriteLine("added to conf " + whichChannel);                 
            }
            else if (e.State.IsCallEnded())
            {
               
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

        private void button1_Click(object sender, EventArgs e)
        {
            //conferenceRoom.RemoveFromConference(callList[Int32.Parse(textBox1.Text)].call);
            //Console.WriteLine("usunieto " + callList[Int32.Parse(textBox1.Text)].call.CallID);
            //callList.RemoveAt(Int32.Parse(textBox1.Text));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            System.Net.IPAddress ip = System.Net.IPAddress.Parse("192.168.1.14");
            server.Start(ip, 8910);
            Console.WriteLine("server started");
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



