using System;
using System.Net;
using System.Net.Sockets;
using Ozeki.Media;
using Ozeki.VoIP;
using System.Windows.Forms;
using System.Collections.Generic;

namespace WieloosobowyKomunikatorGlosowy
{
    public class Klient
    {

        //OZEKI
        public ISoftPhone softphone;   // softphone object
        public IPhoneLine phoneLine;   // phoneline object

        public List<IPhoneLine> phoneLineList;

        
        public IPhoneCall call = null;
        public string caller;
        public Microphone microphone;
        public Speaker speaker;
        public PhoneCallAudioSender mediaSender;
        public PhoneCallAudioReceiver mediaReceiver;
        public MediaConnector connector;
        public string local_ip;
        public string server_ip;

        //TCP
        public static TCP_Connection tcp;


        public Klient()
        {
           local_ip = GetLocalIPAddress();
           phoneLineList = new List<IPhoneLine>();
            OzekiInitialization();

        }
        public int counter = 0;
        public void OzekiInitialization()
        {
            softphone = SoftPhoneFactory.CreateSoftPhone(5000, 10000);
            microphone = Microphone.GetDefaultDevice();
            speaker = Speaker.GetDefaultDevice();
            mediaSender = new PhoneCallAudioSender();
            mediaReceiver = new PhoneCallAudioReceiver();
            connector = new MediaConnector();

            

          
        }
        public void sss()
        {
            var config = new DirectIPPhoneLineConfig(local_ip, 5060 + counter);
            phoneLine = softphone.CreateDirectIPPhoneLine(config);
            phoneLine.RegistrationStateChanged += line_RegStateChanged;
            softphone.IncomingCall += softphone_IncomingCall;
            softphone.RegisterPhoneLine(phoneLine);
            counter++;
        }
       
        public void line_RegStateChanged(object sender, RegistrationStateChangedArgs e)
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
        public void StartCall(string numberToDial)
        {

            sss();
            if (call == null)
            {
               
                Console.WriteLine("starting call");
                call = softphone.CreateDirectIPCallObject(phoneLine, new DirectIPDialParameters("5060"), numberToDial);
      
                call.CallStateChanged += call_CallStateChanged;
                call.Start();
                Console.WriteLine(call.CallID);
                
            }
        }
        public void softphone_IncomingCall(object sender, VoIPEventArgs<IPhoneCall> e)
        {
           
            call = e.Item;
            caller = call.DialInfo.CallerID;
            call.CallStateChanged += call_CallStateChanged;
           
            call.Answer();
            
        }
        public void call_CallStateChanged(object sender, CallStateChangedArgs e)
        { 

            if (e.State == CallState.Completed)
            {
                Console.WriteLine("zakonczono rozmowe");
              
            }
            if (e.State == CallState.Answered)
                SetupDevices();
            if (e.State.IsCallEnded())
                CloseDevices();
        }
        int x = 0;
        public void SetupDevices()
        {
            
            if (x > 0)
            {
                OzekiInitialization();
            }
            Console.WriteLine("setupDevices");

            x++;
            microphone.Start();
            connector.Connect(microphone, mediaSender);

            speaker.Start();
            connector.Connect(mediaReceiver, speaker);

            mediaSender.AttachToCall(call);
            mediaReceiver.AttachToCall(call);
        }
        public void CloseDevices()
        {
            Console.WriteLine("closeDevices");
            phoneLine.Dispose();
            microphone.Dispose();
            speaker.Dispose();
            mediaReceiver.Detach();
            mediaSender.Detach();
            connector.Dispose();

            
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
        public void HangUp()
        {         
            if (call != null)
            {
                
                call.HangUp();
                call.PhoneLine.Dispose();
                call.CallStateChanged += call_CallStateChanged;
                call = null; 
            }
            else
            {
                Console.WriteLine("call = null");
            }
        }
      
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ChannelsView());
        }
    }
}

