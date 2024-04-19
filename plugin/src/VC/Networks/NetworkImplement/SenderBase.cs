using H3MP.Networking;
using H3VC.Data;
using H3VC.Netwroks;
using HarmonyLib;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static UnityEngine.Rendering.PostProcessing.HableCurve;

namespace H3VC.Networks.NetworkImplement
{
    public abstract class SenderBase<SendT> {
        protected string packetID;
        protected TransportProtocol prtcl;

        protected SenderBase(string pktID,TransportProtocol prtcl) {
            this.packetID = pktID;
            this.prtcl = prtcl;
        }

        private void ClientSend(SendT data) {
            var packet = ToPacket(data);
            H3MP.Networking.ClientSend.SendUDPData(packet);
        }
        public void ServerSend(SendT data) {
            var packet = ToPacket(data);
            ServerRelaySend(0, packet);
        }

        public void ServerRelaySend(int sender, Packet pkt) {
            pkt.WriteLength();
            if(prtcl == TransportProtocol.TCP) {
                foreach (var client in Server.clients.Values) {
                    if (client.ID == sender) {
                        return;
                    }
                    client.udp.SendData(pkt);
                }
            } else {
                foreach (var client in Server.clients.Values) {
                    if (client.ID == sender) {
                        return;
                    }
                    client.tcp.SendData(pkt);
                }
            }
        }
       
        public Packet ToPacket(SendT data) {
            Packet pkt = new Packet(-1);
            pkt.Write(packetID);
            WritePacketData(ref pkt,data);
            return pkt;
        }
        public abstract void WritePacketData(ref Packet pkt,SendT data);

        public void SendFromClient(SendT data) {
            if (ThreadManager.host) {
                ServerSend(data);
            } else {
                ClientSend(data);
            }
        }
    }
}
