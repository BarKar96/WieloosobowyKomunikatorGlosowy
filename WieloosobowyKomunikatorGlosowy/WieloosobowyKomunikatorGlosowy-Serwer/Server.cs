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

        static ISoftPhone softphone;   // softphone object
        static ISoftPhone softphone1;

        static IPhoneLine phoneLine;
        static IPhoneLine phoneLine1; // phoneline object

        public string local_ip;
        public static ConferenceRoom conferenceRoom;
        public static ConferenceRoom conferenceRoom1;


        public Server()
        {
            InitializeComponent();
            local_ip = GetLocalIPAddress();
            //local_ip = "127.0.0.1";
            OzekiInitialization();
            InitializeConferenceRoom();
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




        static void InitializeConferenceRoom()
        {
            conferenceRoom = new ConferenceRoom();
            conferenceRoom.StartConferencing();

            conferenceRoom1 = new ConferenceRoom();
            conferenceRoom1.StartConferencing();

        }


        static void softphone_IncomingCall(object sender, VoIPEventArgs<IPhoneCall> e)
        {
            IPhoneCall call = e.Item;
            call.CallStateChanged += call_CallStateChanged;
            call.Answer();

        }

        static void line_RegStateChanged(object sender, RegistrationStateChangedArgs e)
        {

            IPhoneCall call = sender as IPhoneCall;
           

            if (e.State == RegState.NotRegistered || e.State == RegState.Error)
                Console.WriteLine("Registration failed!");

            if (e.State == RegState.RegistrationSucceeded)
            {
                Console.WriteLine("Registration succeeded - Online!");
                InitializeConferenceRoom();
            }
        }

        static void call_CallStateChanged(object sender, CallStateChangedArgs e)
        {

            IPhoneCall call = sender as IPhoneCall;

            if (e.State == CallState.Answered)
            {
                if (xyz < 1)
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
                    Console.WriteLine("rem to conf 1");
                }
                else
                {
                    conferenceRoom1.RemoveFromConference(call);
                    Console.WriteLine("rem to conf 2");
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

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Server());
        }
    }
}



