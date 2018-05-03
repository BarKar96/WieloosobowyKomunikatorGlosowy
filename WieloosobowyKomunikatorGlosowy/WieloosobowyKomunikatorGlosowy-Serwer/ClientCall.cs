using Ozeki.VoIP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WieloosobowyKomunikatorGlosowy_Serwer
{
    class ClientCall
    {
        public string id;
        public IPhoneCall call;
        public int channelIndex = 0;

        public ClientCall(string id, IPhoneCall call)
        {
            this.id = id;
            this.call = call;
        }
    }
}
