using System;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.Windows.Forms;

namespace WieloosobowyKomunikatorGlosowy
{
    public class Connection
    {
        private static TcpClient _client;
        private static StreamReader _sReader;
        private static StreamWriter _sWriter;
        private static Boolean _isConnected;
        public static string data;
        public static string ip_server;
        public static int port = 8001;
        public static String sDataIncomming;

        public Connection()
        {

        }

        public static Boolean runserver()
        {
            try
            {
                IPAddress ip = IPAddress.Parse(ip_server);
                _client = new TcpClient();
                _client.Connect(ip, port);
                HandleCommunication();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void HandleCommunication()
        {
            _sReader = new StreamReader(_client.GetStream(), Encoding.ASCII);
            _sWriter = new StreamWriter(_client.GetStream(), Encoding.ASCII);

            _isConnected = true;

        }

        public static string send(string text)
        {
            _sReader = new StreamReader(_client.GetStream(), Encoding.ASCII);
            _sWriter = new StreamWriter(_client.GetStream(), Encoding.ASCII);

            if (_isConnected)
            {
                // write data and make sure to flush, or the buffer will continue to 
                // grow, and your data might not be sent when you want it, and will
                // only be sent once the buffer is filled.
                _sWriter.WriteLine(text);
                _sWriter.Flush();

                sDataIncomming = _sReader.ReadLine();
                return sDataIncomming;
            }

            return "false";
        }

        public static void close()
        {
            send("bye");
            _sReader.Close();
            _sWriter.Close();
            _client.Client.Close();
            _client.Close();
        }
    }
}