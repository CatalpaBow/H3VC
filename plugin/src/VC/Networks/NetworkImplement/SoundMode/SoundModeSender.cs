using H3MP.Networking;
using H3VC.Data;
using H3VC.Netwroks;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace H3VC.Networks.NetworkImplement
{
    public class SoundModeSender : SenderBase<SoundMode>
    {

        public SoundModeSender(string id, TransportProtocol prtcl) :base(id,prtcl) { 
        }
        public override void WritePacketData(ref Packet pkt, SoundMode data) {
            pkt.Write((int)data);
        }
    }
}
