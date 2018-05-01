using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WieloosobowyKomunikatorGlosowy
{
    class Channel
    {
        public string description;
        public string name;
        public string password = null;
        public int port;

        public Channel(string name, string description, string password, int port)
        {
            this.name = name;
            this.description = description;
            this.password = password;
            this.port = port;

        }
    }
}
