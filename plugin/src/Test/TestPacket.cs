using H3MP.Networking;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace H3VC
{
    public static class TestPacket
    {
        public static void Intialize() {
            H3MP.Mod.GenericCustomPacketReceived += PacketReceived;
        }
        public static void PacketReceived(int clientID, string ID, Packet packet) {
            if (ID != "TestPacket") {
                return;
            }
            string sentence = packet.ReadString();
            Mod.Logger.LogMessage(sentence);
        }

        public static void Send() {
            Packet pkt = new Packet(-1);
            pkt.Write("TestPacket");
            pkt.Write("HelloWorldYoYo");
            ClientSend.SendTCPData(pkt);
        }

        public static void Test() {
        }
    }

    public class H3VRRaycaster : BaseRaycaster
    {
        public override Camera eventCamera => throw new System.NotImplementedException();

        public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList) {
            throw new System.NotImplementedException();
        }
    }

}
