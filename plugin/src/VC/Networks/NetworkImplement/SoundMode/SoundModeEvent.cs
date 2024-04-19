using H3VC.Networks.NetworkImplement;
using H3VC.Netwroks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace H3VC.NetworkImplement.Mute
{
    public static class SoundModeEvent{
        public static SoundModeSender sender;
        public static SoundModeReceiver receiver;

        static SoundModeEvent() {
            string id = "H3VC_SoundMode";
            TransportProtocol prtcl = TransportProtocol.TCP;
            sender = new SoundModeSender(id,prtcl);
            receiver = new SoundModeReceiver(id);
        }
    }
}
