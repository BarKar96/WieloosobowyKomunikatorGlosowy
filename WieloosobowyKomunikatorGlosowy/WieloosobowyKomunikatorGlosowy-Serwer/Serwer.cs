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
    class Serwer
    {
        static ISoftPhone softphone;   // softphone object
        static IPhoneLine phoneLine;   // phoneline object
        public string local_ip;
        static ConferenceRoom conferenceRoom;

        public Serwer()
        {
            //local_ip = GetLocalIPAddress();
            local_ip = "127.0.0.1";
            //OzekiInitialization();
            //InitializeConferenceRoom();
            TCP_Connection tcp = new TCP_Connection(local_ip);


        }

    public void OzekiInitialization()
        {
            softphone = SoftPhoneFactory.CreateSoftPhone(6000, 6200);
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
        }
        

        static void softphone_IncomingCall(object sender, VoIPEventArgs<IPhoneCall> e)
        {
            IPhoneCall call = e.Item;
            call.CallStateChanged += call_CallStateChanged;
            call.Answer();
        }

        static void line_RegStateChanged(object sender, RegistrationStateChangedArgs e)
        {
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
                conferenceRoom.AddToConference(call);
            else if (e.State.IsCallEnded())
                conferenceRoom.RemoveFromConference(call);
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
            Application.Run(new Form1());
        }
    }
}
