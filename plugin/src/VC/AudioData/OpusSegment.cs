using H3MP.Networking;

namespace H3VC.Data
{
    /// <summary>
    /// Opus audio codec data
    /// </summary>
    public class OpusSegment
    {
        public readonly byte[] data;
        public readonly int dataLength;
        public readonly int length;
        public OpusSegment(byte[] _data, int _length) {
            data = _data;
            dataLength = _data.GetLength(0);
            length = _length;
        }

        public Packet ToPacket(int id, string name) {
            Packet pkt = new Packet(id);
            pkt.Write(name);
            pkt.Write(dataLength);
            pkt.Write(data);
            pkt.Write(length);
            return pkt;
        }

    }
}
