using H3MP.Networking;
using H3VC.Data;
using H3VC.H3MPWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;

namespace H3VC.Networks.NetworkImplement
{
    public class MuteReceiver : ReceiverBase<bool> {
        public MuteReceiver(string id): base(id) { 

        }
        public override bool Unpacket(CustomPacketReceivedEventData data) {
            return data.packet.ReadBool();
        }
    }
}
