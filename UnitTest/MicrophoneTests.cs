using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using H3VC;
using H3VC.VoiceRecoders;
using Xunit.Abstractions;
using UniRx;
using FFmpeg.AutoGen;
using H3VC.Data;
using FistVR;
namespace UnitTest{
    public class MicrophoneTests{
        private VoiceRecoder recoder;
        private readonly ITestOutputHelper _output;
        public MicrophoneTests(ITestOutputHelper output) {
            _output = output;
            H3VC.VoiceRecoders.MicImpement.NAudioMic mic = new H3VC.VoiceRecoders.MicImpement.NAudioMic();
            recoder = new VoiceRecoder(mic);
        }
        [Fact]
        public void InstanceTest() {
            Assert.NotNull(recoder);
        }
        [Fact]
        public void DeviceInfoTest() {
            IEnumerable<MicDeviceInfo> deviceInfos =  recoder.ShowDevices();
            foreach(var deviceInfo in deviceInfos) {
                _output.WriteLine(deviceInfo.deviceID + ":" + deviceInfo.name);
            }
        }
        [Fact]
        public void OnAudioReadyTest() {
            bool isAudioComed = false;
            var disposer = recoder.OnAudioReady
                                  .Subscribe(_ => isAudioComed = true);
            Thread.Sleep(1000);
            Assert.True(isAudioComed);
            disposer.Dispose();
        }
        [Fact]
        public void NextDeviceTest() {
            _output.WriteLine(recoder.crntDevice.Value.deviceID + ":" + recoder.crntDevice.Value.name);
            for (int i =0; i < 10; i++) {
                recoder.NextDevice();
                var deviceInfo = recoder.crntDevice.Value;
                _output.WriteLine(deviceInfo.deviceID + ":" + deviceInfo.name);
            }
        }
        [Fact]
        public void PreviousDeviceTest() {
            _output.WriteLine(recoder.crntDevice.Value.deviceID + ":" + recoder.crntDevice.Value.name);
            for (int i = 0; i < 10; i++) {
                recoder.PreviousDevice();
                var deviceInfo = recoder.crntDevice.Value;
                _output.WriteLine(deviceInfo.deviceID + ":" + deviceInfo.name);
            }
        }
        [Fact]
        public void NextSoundModeTest() {
            int initalValue = (int)recoder.soundMode.Value;
            int soundModeLength = Enum.GetValues(typeof(SoundMode)).Length;
            var numberLoop = Enumerable.Range(initalValue, soundModeLength * 3)
                                       .Select(num => num % soundModeLength);

            foreach (var num in numberLoop) {
                Assert.Equal(num, (int)recoder.soundMode.Value);
                _output.WriteLine(num + ":" + recoder.soundMode.Value.ToString());
                recoder.NextSoundMode();
            }

        }
        [Fact]
        public void PreviousSoundModeTest() {
            int initalValue = (int)recoder.soundMode.Value;
            int soundModeLength = Enum.GetValues(typeof(SoundMode)).Length;
            var numberLoop = Enumerable.Range(initalValue, soundModeLength * 3)
                                       .Select(num => -(num % soundModeLength) + soundModeLength - 1)
                                       .Prepend(0);
            foreach(var num in numberLoop) {
                Assert.Equal(num, (int)recoder.soundMode.Value);
                _output.WriteLine(num + ":" + recoder.soundMode.Value.ToString());
                recoder.PreviousSoundMode();
            }
        }
        [Fact]
        public async void SamplesAverageTst() {
            int samplesCount = 0;
            recoder.OnAudioReady.Subscribe(data => samplesCount += data.Value.pcmBuffer.Length);
            await Task.Delay(100000);
            _output.WriteLine(samplesCount.ToString());
        }
    }

}
