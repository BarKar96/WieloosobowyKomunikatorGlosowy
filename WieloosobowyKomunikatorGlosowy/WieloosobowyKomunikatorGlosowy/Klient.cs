using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Ozeki.Media;
using Ozeki.VoIP;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace WieloosobowyKomunikatorGlosowy
{
    public class Klient
    {
        string destinationIP;
        //OZEKI
        static ISoftPhone softphone;   // softphone object
        static IPhoneLine phoneLine;   // phoneline object
        static IPhoneCall call;
        static string caller;
        static Microphone microphone;
        static Speaker speaker;
        static PhoneCallAudioSender mediaSender;
        static PhoneCallAudioReceiver mediaReceiver;
        static MediaConnector connector;
        private Thread Caller;


        static string local_ip = GetLocalIPAddress();
        static string destination_ip;
        static string destination_user;

        //Server 
        private TcpListener _server;
        private Boolean _isRunning;
        private IPAddress ipAd = IPAddress.Parse(GetLocalIPAddress());
        private int port = 8003;
        private static int port2 = 8003;
        private static Boolean busy { get; set; }


        //Klient 
        private static TcpClient _client;
        private static StreamReader _sReader;
        private static StreamWriter _sWriter;
        private static Boolean _isConnected;
        public static string data;
        public static String sDataIncomming;

        public void Ozeki()
        {
            softphone = SoftPhoneFactory.CreateSoftPhone(6000, 6200);

            microphone = Microphone.GetDefaultDevice();
            speaker = Speaker.GetDefaultDevice();
            mediaSender = new PhoneCallAudioSender();
            mediaReceiver = new PhoneCallAudioReceiver();
            connector = new MediaConnector();

            var config = new DirectIPPhoneLineConfig(local_ip, 5060);
            phoneLine = softphone.CreateDirectIPPhoneLine(config);
            phoneLine.RegistrationStateChanged += line_RegStateChanged;
            softphone.IncomingCall += softphone_IncomingCall;
            softphone.RegisterPhoneLine(phoneLine);
        }
        private static void line_RegStateChanged(object sender, RegistrationStateChangedArgs e)
        {

            if (e.State == RegState.NotRegistered || e.State == RegState.Error)
            {
                //Status.Invoke(new MethodInvoker(delegate { Status.Text = "Blad rejestracji!"; }));
                Console.WriteLine("blad rejestracji");               
            }

            if (e.State == RegState.RegistrationSucceeded)
            {
                //Status.Invoke(new MethodInvoker(delegate { Status.Text = "Zarejestrowano"; }));
                Console.WriteLine("zarejestrowano");
            }
        }
        public static void StartCall(string numberToDial)
        {


            if (call == null)
            {
                Console.WriteLine("starting call");
                call = softphone.CreateDirectIPCallObject(phoneLine, new DirectIPDialParameters("5060"), numberToDial);
                call.CallStateChanged += call_CallStateChanged;


                call.Start();
            }
        }
        static void softphone_IncomingCall(object sender, VoIPEventArgs<IPhoneCall> e)
        {

            call = e.Item;
            caller = call.DialInfo.CallerID;

            call.CallStateChanged += call_CallStateChanged;
            SetupDevices();
            call.Answer();
        }
        static void call_CallStateChanged(object sender, CallStateChangedArgs e)
        {
            //Status.Invoke(new MethodInvoker(delegate { Status.Text = e.State.ToString(); }));

            if (e.State == CallState.Completed)
            {
                //MessageBox.Show("Zakończono rozmowę");
                Console.WriteLine("zakonczono rozmowe");
            }

            if (e.State == CallState.Answered)
                SetupDevices();

            if (e.State.IsCallEnded())
                CloseDevices();
        }
        public static void SetupDevices()
        {
            microphone.Start();
            connector.Connect(microphone, mediaSender);

            speaker.Start();
            connector.Connect(mediaReceiver, speaker);

            mediaSender.AttachToCall(call);
            mediaReceiver.AttachToCall(call);
        }
        static void CloseDevices()
        {
            phoneLine.Dispose();
            microphone.Dispose();
            speaker.Dispose();
            mediaReceiver.Detach();
            mediaSender.Detach();
            connector.Dispose();
        }
        static string GetLocalIPAddress()
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
        public static void HandleCommunication()
        {
            _sReader = new StreamReader(_client.GetStream(), Encoding.ASCII);
            _sWriter = new StreamWriter(_client.GetStream(), Encoding.ASCII);

            _isConnected = true;

        }
        public static Boolean connect()
        {
            IPAddress ip = IPAddress.Parse(destination_ip);
            _client = new TcpClient();
            _client.Connect(ip, port2);
            HandleCommunication();
            return true;
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

