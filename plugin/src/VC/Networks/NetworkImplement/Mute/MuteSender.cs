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
    public class MuteSender : SenderBase<bool>{

        public MuteSender(string id, TransportProtocol prtcl) :base(id,prtcl) { 
        }
        public override void WritePacketData(ref Packet pkt,bool data) {
            pkt.Write(data);
        }
    }
}
