using Ozeki.VoIP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WieloosobowyKomunikatorGlosowy
{
    class Channel
    {
        public string name;
        public string description;
        public string password;     
        public List<User> userList;
        public Channel(string name, string description, string password)
        {
            this.name = name;
            this.description = description;
            this.password = password;           
            this.userList = new List<User>();

          
        }
    }
}
