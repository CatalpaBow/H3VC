using H3VC.AudioPipelines;
using H3VC.Converter;
using H3VC.Data;
using H3VC.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;

namespace H3VC.Networks{
    public class NetworkAudioListener : IAudioInput{
        private VoiceDecoder decoder;

        public IObservable<KeyValuePair<int, PCMSegment>> OnAudioReady => 
            VoiceReceiver.Instance
                         .OnVoiceReceived
                         .Select(audioData => {
                             PCMSegment pcmSgmnt = decoder.Decode(audioData.Item2);
                             return new KeyValuePair<int, PCMSegment>(audioData.Item1, pcmSgmnt);
                         });

        private Subject<KeyValuePair<int, bool>> muteDummy;
        public IObservable<KeyValuePair<int, bool>> OnMuteChanged => muteDummy;

         private Subject<KeyValuePair<int, SoundMode>> modeDummy;
        public IObservable<KeyValuePair<int, SoundMode>> OnSoundModeChanged => modeDummy;


        public NetworkAudioListener() {
            muteDummy = new Subject<KeyValuePair<int, bool>>();
            modeDummy = new Subject<KeyValuePair<int, SoundMode>>();
            decoder = new VoiceDecoder();
        }


    }
}
