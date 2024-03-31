using H3MP.Networking;
using H3VC.Data;
namespace H3VC.Network
{
    static class VoiceSender
    {
        public const string packetID = "H3VC_VoiceData";

        public static void ClientSend(OpusSegment sgmnt) {
            var packet = sgmnt.ToPacket(-1, packetID);
            H3MP.Networking.ClientSend.SendUDPData(packet);
        }
        public static void ServerRelaySend(int sender, OpusSegment sgmnt) {
            var packet = sgmnt.ToPacket(-1, packetID);

            packet.WriteLength();
            foreach (var client in Server.clients.Values) {
                if (client.ID == sender) {
                    return;
                }
                client.udp.SendData(packet);
            }

            //H3MP.Networking.ServerSend.SendUDPData(Server.connectedClients,packet);
        }
    }
}
