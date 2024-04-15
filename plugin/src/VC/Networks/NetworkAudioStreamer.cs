using H3VC.AudioPipelines;
using H3VC.Data;
using H3VC.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using H3VC.Converter;
namespace H3VC.Networks
{
    public class NetworkAudioStreamer : IAudioOutput{
        private VoiceEncoder encoder;

        public NetworkAudioStreamer() {
            encoder = new VoiceEncoder();
        }
        public void Mute(int id, bool isMute) {
        }

        public void SoundMode(int id, SoundMode mode) {
        }

        public void StreamAudio(int id, PCMSegment sgmnt) {
            foreach (var opusSgmnt in encoder.Encode(sgmnt.pcmBuffer)) {
                VoiceSender.SendFromClient(opusSgmnt);
            }
        }
    }
}
