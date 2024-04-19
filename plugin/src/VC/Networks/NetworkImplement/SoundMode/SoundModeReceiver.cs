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
    public class SoundModeReceiver : ReceiverBase<SoundMode> {
        public SoundModeReceiver(string id): base(id) { 

        }
        public override SoundMode Unpacket(CustomPacketReceivedEventData data) {
            return (SoundMode)data.packet.ReadInt();
        }
    }
}
