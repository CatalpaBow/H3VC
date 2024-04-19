using H3VC.Networks.NetworkImplement;
using H3VC.Netwroks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace H3VC.NetworkImplement.Mute
{
    public static class MuteEvent{
        public static MuteSender sender;
        public static MuteReceiver receiver;

        static MuteEvent() {
            string id = "H3VC_Mute";
            TransportProtocol prtcl = TransportProtocol.TCP;
            sender = new MuteSender(id,prtcl);
            receiver = new MuteReceiver(id);
        }
    }
}
