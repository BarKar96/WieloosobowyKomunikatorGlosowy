using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WieloosobowyKomunikatorGlosowy_Serwer
{
    public class TCP_Connection
    {
        private TcpListener listener;
        private int port = 5000;
        private Database database;

        public TCP_Connection(string ip)
        {
            listener = new TcpListener(IPAddress.Parse(ip), port);
            listener.Start();
            database = new Database();
            ListenClients();
        }

        private void ListenClients()
        {
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                Thread t = new Thread(new ParameterizedThreadStart(HandleClient));
                t.Start(client);
            }
        }

        private void HandleClient(object obj)
        {
            TcpClient client = (TcpClient)obj;
            StreamWriter sWriter = new StreamWriter(client.GetStream(), Encoding.ASCII);
            StreamReader sReader = new StreamReader(client.GetStream(), Encoding.ASCII);

            Boolean clientConnected = true;
            String data = null;
            Boolean ack;

            while (clientConnected)
            {
                data = sReader.ReadLine();
                string [] split = SplitMessage(data);
                switch (split[0])
                {
                    case "LOGOUT": //wylogowanie
                        {
                            clientConnected = false;
                            break;
                        }
                    case "REG": //rejestracja
                        {
                            sWriter.Flush();
                            ack = database.AddUser(split[1], split[2]);
                            if (ack ==  true)
                            {
                                sWriter.WriteLine("ACK");
                            }
                            else
                            {
                                sWriter.WriteLine("ERR;0");
                            }
                            break;
                        }
                }
            }
        }
        private string [] SplitMessage(string message)
        {
            string[] split = message.Split(';');
            return split;
        }
    }
    
}
