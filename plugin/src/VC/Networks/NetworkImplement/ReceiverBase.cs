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
    public abstract class ReceiverBase<ReceiveT> {
        public string packetID;
        public IObservable<ReceiveT> OnReceived;
        public IObservable<KeyValuePair<int, ReceiveT>> OnReceivedWithSenderID;
        public IObservable<CustomPacketReceivedEventData> OnReceivedRaw;
        public ReceiverBase(string packetID) {
            this.packetID = packetID;
            OnReceivedRaw = H3MPWrapper.H3MPNetworkEvents.OnCustomPacketRecevied
                                       .Where(evntData => evntData.id == packetID);
            OnReceived = OnReceivedRaw.Select(Unpacket);
            OnReceivedWithSenderID = OnReceivedRaw.Select(data => new KeyValuePair<int,ReceiveT>(data.clientID,Unpacket(data)));
        }
        public abstract ReceiveT Unpacket(CustomPacketReceivedEventData data);
    }
}
