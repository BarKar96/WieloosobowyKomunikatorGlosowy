using Ozeki.VoIP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WieloosobowyKomunikatorGlosowy_Serwer
{
    class User
    {
        public string name;
        public string callID;
        public User(string name, string callID)
        {
            this.name = name;
            this.callID = callID;
        }
    }
}
