using H3VC.Data;
using UnityOpus;
namespace H3VC.Converter
{
    public class VoiceDecoder
    {
        const NumChannels channels = NumChannels.Mono;
        Decoder decoder;
        readonly float[] pcmBuffer = new float[Decoder.maximumPacketDuration * (int)channels];

        public VoiceDecoder() {
            this.decoder = new Decoder(
                SamplingFrequency.Frequency_48000,
                NumChannels.Mono);
        }
        void OnDisable() {
            decoder.Dispose();
            decoder = null;
        }

        public PCMSegment Decode(OpusSegment segment) {
            var pcmLength = decoder.Decode(segment.data, segment.length, pcmBuffer);
            return new PCMSegment(pcmBuffer, pcmLength);
        }
    }

}
