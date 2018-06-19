using Ozeki.VoIP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WieloosobowyKomunikatorGlosowy_Serwer
{
    class Channel
    {
        public string name;
        public string description;
        public string password;
        public ConferenceRoom conferenceRoom;
        public List<User> userList;
        public Channel(string name, string description, string password)
        {
            this.name = name;
            this.description = description;
            this.password = password;
            this.conferenceRoom = new ConferenceRoom();            
            this.userList = new List<User>();

            conferenceRoom.StartConferencing();
        }
        public void addUser(User user)
        {
            userList.Add(user);
        }
        public void remUser(string callID)
        {
          
            foreach (User u in userList)
            {
                if (u.callID.Equals(callID))
                {
                    Console.WriteLine("tutaj4");
                    userList.Remove(u);
                    break;
                }
            }
            
        }

        public void remUser1(string name)
        {

            foreach (User u in userList)
            {
                if (u.name.Equals(name))
                {
                    userList.Remove(u);
                    break;
                }
            }
        }

        public string GetName()
        {
            return name;

        }
    }
}
