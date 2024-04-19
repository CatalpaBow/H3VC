using H3VC.AudioPipelines;
using H3VC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;
using H3MP;
namespace H3VC.VoiceRecoders{
    public class VoiceRecoder : IAudioInput{
        /// <summary>
        /// The user ID is -1, but the ID must be passed as a rule of the interface, so I temporarily set the ID to -1.
        /// </summary>
        // オーディオパイプラインでのマイク入力の出力先がオーディオストリーマーである限りは、問題なく動作する。
        public int id = -1;
        private BaseMicrophone mic;
        private VoiceLevelMeter levelMeter;

        public ReactiveProperty<float> volume;
        public ReactiveProperty<bool> isMute;
        public ReactiveProperty<SoundMode> soundMode;
        public ReactiveProperty<MicDeviceInfo> crntDevice;
        public IReadOnlyReactiveProperty<float> VoiceLevel => levelMeter.VoiceLevel;

        public IObservable<KeyValuePair<int, PCMSegment>> OnAudioReady => mic.OnAudioReady
                                                                             .Select(samples => samples.Select(sample => sample * volume.Value * 2).ToArray())
                                                                             .Select(sgmnt => new PCMSegment(sgmnt,sgmnt.Length))
                                                                             .Select(pcmData => new KeyValuePair<int, PCMSegment>(id,pcmData));
        public IObservable<KeyValuePair<int, bool>> OnMuteChanged => isMute.Select(isMute => new KeyValuePair<int, bool>(id,isMute));

        public IObservable<KeyValuePair<int, SoundMode>> OnSoundModeChanged => soundMode.Select(mode => new KeyValuePair<int,SoundMode>(id, mode));

        public VoiceRecoder(BaseMicrophone _mic) {
            this.mic = _mic;
            volume = new ReactiveProperty<float>();
            volume.Value = 0.5f;
            crntDevice = new ReactiveProperty<MicDeviceInfo>();
            crntDevice.Value = mic.ShowDevices().First(); 
            isMute = new ReactiveProperty<bool>();
            soundMode = new ReactiveProperty<SoundMode>();
            StartRecoding();
            levelMeter = new VoiceLevelMeter(this);
        }

        public void DecreaseVolume() {
            ChangeVolume(volume.Value - 0.1f);
        }
        public void IncreaseVolume() {
            ChangeVolume(volume.Value + 0.1f);
        }
        private void ChangeVolume(float val) {
            if(val < 0) {
                val = 0;
            }
            if(val > 1) {
                val = 1;
            }
            volume.Value = val;
        }
        private void StartRecoding() {
            mic.StartRecoding();
        }

        public IEnumerable<MicDeviceInfo> ShowDevices() {
            return mic.ShowDevices();
        }

        public void ChangeDevice(int deviceID) {
            mic.ChangeDevice(deviceID);
        }

        public void NextDevice(){
            int nextNum = crntDevice.Value.deviceID + 1;
            //if over DeviceCount, return to 0(Loop)
            if (nextNum  >= mic.DeviceCount()) {
                nextNum = 0;
            }
            crntDevice.Value = mic.ChangeDevice(nextNum);
            
        }
        public void PreviousDevice(){
            int previousNum = crntDevice.Value.deviceID - 1;

            //if under 0 ,Do Loop
            if (previousNum < 0) {
                previousNum = mic.DeviceCount() - 1;
            }
            crntDevice.Value = mic.ChangeDevice(previousNum);
        }

        public void NextSoundMode(){
            int enumLenth = Enum.GetValues(typeof(SoundMode)).Length;

            //if over enumLength, return to 0(Loop)
            if ((int)soundMode.Value + 1 >= enumLenth) {
                soundMode.Value = 0;
                return;
            }

            //NextValue
            soundMode.Value += 1;

        }
        public void PreviousSoundMode(){
            int enumLenth = Enum.GetValues(typeof(SoundMode)).Length;

            //if under 0 , Do loop.
            if ((int)soundMode.Value - 1 < 0) {
                soundMode.Value = (SoundMode)(enumLenth - 1);
                return;
            }

            //Previous Value
            soundMode.Value -= 1;
        }

    }
}
