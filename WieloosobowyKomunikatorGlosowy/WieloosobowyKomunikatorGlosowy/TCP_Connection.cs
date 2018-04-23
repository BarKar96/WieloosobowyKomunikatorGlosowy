using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace WieloosobowyKomunikatorGlosowy
{
    public class TCP_Connection
    {
        private static TcpClient client;
        private static StreamReader sReader = new StreamReader(client.GetStream(), Encoding.ASCII);
        private static StreamWriter sWriter = new StreamWriter(client.GetStream(), Encoding.ASCII);
        public static string data;
        public static string ip_server = "127.0.0.1";
        public static int port = 5000;

        public TCP_Connection()
        {
            IPAddress ip = IPAddress.Parse(ip_server);
            client = new TcpClient();
            client.Connect(ip, port);
        }
        

        public string send(string message)
        {
            Console.WriteLine(message);
            sWriter.WriteLine(message);
            sWriter.Flush();
            data = sReader.ReadLine();
            Console.WriteLine(data);
            return data;
        }

        public void close()
        {
            sReader.Close();
            sWriter.Close();
            client.Client.Close();
            client.Close();
        }
    }
}