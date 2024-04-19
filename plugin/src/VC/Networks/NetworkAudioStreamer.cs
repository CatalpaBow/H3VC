using H3VC.AudioPipelines;
using H3VC.Data;
using H3VC.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using H3VC.Converter;
using System.IO;
using H3VC.Netwroks;
using H3VC.NetworkImplement.Mute;
namespace H3VC.Networks{

    /// <summary>
    /// Encode audio data and send it to network.
    /// </summary>
    // This class may need to be refactored to be split into an audio streamer and a VC EventStreamer.
    public class NetworkAudioStreamer : IAudioOutput{
        private VoiceEncoder encoder;

        public NetworkAudioStreamer() {
            encoder = new VoiceEncoder();
        }
        public void Mute(int id, bool isMute) {
            MuteEvent.sender.SendFromClient(isMute);
        }

        public void SoundMode(int id, SoundMode mode) {
            SoundModeEvent.sender.SendFromClient(mode);
        }

        public void StreamAudio(int id, PCMSegment sgmnt) {
            foreach (var opusSgmnt in encoder.Encode(sgmnt.pcmBuffer)) {
                VoiceSender.SendFromClient(opusSgmnt);
            }
        }
    }
}
