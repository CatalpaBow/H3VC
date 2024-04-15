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
            recoder.StartRecoding();
            Thread.Sleep(1000);
            Assert.True(isAudioComed);
        }


    }

}
