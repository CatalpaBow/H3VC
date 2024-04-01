using H3MP.Networking;
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
    }
}
