using H3VC.AudioPipelines;
using H3VC.Converter;
using H3VC.Data;
using H3VC.Network;
using H3VC.NetworkImplement.Mute;
using H3VC.Netwroks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;

namespace H3VC.Networks{
    /// <summary>
    /// Receive audiodata from network and decode it and publish audio event.
    /// </summary>
    public class NetworkAudioListener : IAudioInput{
        private VoiceDecoder decoder;

        public IObservable<KeyValuePair<int, PCMSegment>> OnAudioReady => 
            VoiceReceiver.Instance
                         .OnVoiceReceived
                         .Select(audioData => {
                             PCMSegment pcmSgmnt = decoder.Decode(audioData.Item2);
                             return new KeyValuePair<int, PCMSegment>(audioData.Item1, pcmSgmnt);
                         });

        public IObservable<KeyValuePair<int, bool>> OnMuteChanged => MuteEvent.receiver
                                                                              .OnReceivedWithSenderID;
        public IObservable<KeyValuePair<int, SoundMode>> OnSoundModeChanged => SoundModeEvent.receiver
                                                                                             .OnReceivedWithSenderID;
        public NetworkAudioListener() {
            decoder = new VoiceDecoder();
        }
    }
}
