using H3VC.AudioPipelines;
using H3VC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;
using H3MP;
namespace H3VC.VoiceRecoders
{
    public class VoiceRecoder : IAudioInput, IMutipleDevice{
        public int id = -1;
        private H3VC.VoiceRecoders.Microphone mic;
        public ReactiveProperty<bool> isMute;
        public ReactiveProperty<SoundMode> soundMode;

        public IObservable<KeyValuePair<int, PCMSegment>> OnAudioReady => mic.OnAudioReady
                                                                             .Select(sgmnt => new PCMSegment(sgmnt,sgmnt.Length))
                                                                             .Select(pcmData => new KeyValuePair<int, PCMSegment>(id,pcmData));

        public IObservable<KeyValuePair<int, bool>> OnMuteChanged => isMute.Select(isMute => new KeyValuePair<int, bool>(id,isMute));

        public IObservable<KeyValuePair<int, SoundMode>> OnSoundModeChanged => soundMode.Select(mode => new KeyValuePair<int,SoundMode>(id, mode));

        public VoiceRecoder(VoiceRecoders.Microphone _mic) {
            this.mic = _mic;
            isMute = new ReactiveProperty<bool>();
            soundMode = new ReactiveProperty<SoundMode>();
            StartRecoding();
        }

        public void StartRecoding() {
            mic.StartRecoding();
        }

        public IEnumerable<MicDeviceInfo> ShowDevices() {
            return mic.ShowDevices();
        }

        public void ChangeDevice(int deviceID) {
            mic.ChangeDevice(deviceID);
        }
    }
}
