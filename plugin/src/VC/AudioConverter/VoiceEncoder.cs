using H3VC.Data;
using System.Collections.Generic;
using UnityOpus;
namespace H3VC.Converter{
    /// <summary>
    /// encode pcm to opus
    /// </summary>
    public class VoiceEncoder
    {
        const int bitrate = 96000;
        const int frameSize = 120;
        const int outputBufferSize = frameSize * 4; // at least frameSize * sizeof(float)

        Encoder encoder;
        Queue<float> pcmQueue = new Queue<float>();
        readonly float[] frameBuffer = new float[frameSize];
        readonly byte[] outputBuffer = new byte[outputBufferSize];

        public VoiceEncoder() {
            encoder = new Encoder(
                SamplingFrequency.Frequency_48000,
                NumChannels.Mono,
                OpusApplication.Audio) {
                Bitrate = bitrate,
                Complexity = 10,
                Signal = OpusSignal.Music
            };
        }

        void Disable() {
            encoder.Dispose();
            encoder = null;
            pcmQueue.Clear();
        }
        /// <param name="data">pcmData</param>
        /// <returns>Encoded opus datas</returns>
        public IEnumerable<OpusSegment> Encode(float[] data) {
            foreach (var sample in data) {
                pcmQueue.Enqueue(sample);
            }
            while (pcmQueue.Count > frameSize) {
                for (int i = 0; i < frameSize; i++) {
                    frameBuffer[i] = pcmQueue.Dequeue();
                }
                var encodedLength = encoder.Encode(frameBuffer, outputBuffer);
                yield return new OpusSegment(outputBuffer, encodedLength);
            }
        }
    }

}
