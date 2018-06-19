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
        public string number_user;
        public string description;
        public bool password;
        public List<User> userList;
        public Channel(string name, string number_user, string description, bool password)
        {
            this.name = name;
            this.number_user = number_user;
            this.description = description;
            this.password = password;
            this.userList = new List<User>();


        }

    }
}
