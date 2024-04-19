using H3MP.Networking;
using H3VC.Data;
using System;
using UniRx;
namespace H3VC.Network
{
    /// <summary>
    /// Receive audiodata and vc event.
    /// 
    /// </summary>
    public class VoiceReceiver : IDisposable{
        public static VoiceReceiver Instance => _instance ?? (_instance = new VoiceReceiver());
        private static VoiceReceiver _instance;
        public static string packetName = "H3VC_VoiceData";
        public IObservable<Tuple<int, OpusSegment>> OnVoiceReceived => voiceReceivedStream;
        private Subject<Tuple<int, OpusSegment>> voiceReceivedStream;

        public VoiceReceiver() {
            voiceReceivedStream = new Subject<Tuple<int, OpusSegment>>();
            H3MP.Mod.GenericCustomPacketReceived += PacketHandler;
        }
        /// <summary>
        /// Handle packet and Publish received event.
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="name"></param>
        /// <param name="pkt"></param>
        private void PacketHandler(int clientID, string name, Packet pkt) {
            if (name != packetName) {
                return;
            }
            int dataLength = pkt.ReadInt();
            byte[] data = pkt.ReadBytes(dataLength);
            int length = pkt.ReadInt();

            voiceReceivedStream.OnNext(Tuple.Create(clientID, new OpusSegment(data, length)));


        }

        public void Dispose() {
            H3MP.Mod.GenericCustomPacketReceived -= PacketHandler;
        }
    }


}
