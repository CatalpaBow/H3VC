using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;
using H3MP.Networking;
using static H3MP.Mod;
namespace H3VC.H3MPWrapper
{
    public static class H3MPNetworkEvents{
        public static IObservable<CustomPacketReceivedEventData> OnCustomPacketRecevied = 
            Observable.FromEvent<GenericCustomPacketReceivedDelegate, CustomPacketReceivedEventData>(
                h => (cID, id, pkt) => h(new CustomPacketReceivedEventData(cID, id, pkt)),
                h => GenericCustomPacketReceived += h,
                h => GenericCustomPacketReceived -= h);
    }


    public class CustomPacketReceivedEventData {
        public int clientID;
        public string id;
        public Packet packet;

        public CustomPacketReceivedEventData(int clientID,string id,Packet pkt) {
            this.clientID = clientID;
            this.id = id;
            this.packet = pkt;
        }
    }


}
