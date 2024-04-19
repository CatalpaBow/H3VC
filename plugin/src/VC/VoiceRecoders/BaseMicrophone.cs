using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;

namespace H3VC.VoiceRecoders{
    public abstract class BaseMicrophone : IMutipleDevice{
        public BaseMicrophone() {
        }

        public abstract IObservable<float[]> OnAudioReady { get; }

        public abstract void StartRecoding();
        public abstract IEnumerable<MicDeviceInfo> ShowDevices();
        public abstract MicDeviceInfo ChangeDevice(int deviceNumber);

        public abstract int DeviceCount();
    }
}