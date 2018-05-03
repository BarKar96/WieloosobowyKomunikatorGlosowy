using Ozeki.Media;
using Ozeki.VoIP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WieloosobowyKomunikatorGlosowy_Serwer
{
    public partial class Server : Form
    {
        static int xyz = 0;
        public IPhoneCall call;
        public ISoftPhone softphone;   // softphone object
        public IPhoneLine phoneLine;

        public string local_ip;
        public ConferenceRoom conferenceRoom;
        public ConferenceRoom conferenceRoom1;

        List<ClientCall> callList;
        public Server()
        {    
            InitializeComponent();
            callList = new List<ClientCall>();
            local_ip = GetLocalIPAddress();
            OzekiInitialization();
            InitializeConferenceRoom();

            //local_ip = "127.0.0.1";
            //TCP_Connection tcp = new TCP_Connection(local_ip);
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
            conferenceRoom = new ConferenceRoom();
            conferenceRoom.StartConferencing();

            conferenceRoom1 = new ConferenceRoom();
            conferenceRoom1.StartConferencing();

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
                if (xyz < 2)
                {
                    conferenceRoom.AddToConference(call);
                    Console.WriteLine("added to conf 1");
                    xyz++;
                }
                else
                {
                    conferenceRoom1.AddToConference(call);
                    Console.WriteLine("added to conf 2");
                }

            }
            else if (e.State.IsCallEnded())
            {
                if (xyz <= 2)
                {
                    conferenceRoom.RemoveFromConference(call);
                    Console.WriteLine("rem from conf 1");
                }
                else
                {
                    conferenceRoom1.RemoveFromConference(call);
                    Console.WriteLine("rem from conf 2");
                }

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
            conferenceRoom.RemoveFromConference(callList[Int32.Parse(textBox1.Text)].call);
            Console.WriteLine("usunieto " + callList[Int32.Parse(textBox1.Text)].call.CallID);
            callList.RemoveAt(Int32.Parse(textBox1.Text));
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



