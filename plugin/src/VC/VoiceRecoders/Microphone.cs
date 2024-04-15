using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;

namespace H3VC.VoiceRecoders{
    public abstract class Microphone :IMutipleDevice {
        public Microphone() {
        }

        public abstract IObservable<float[]> OnAudioReady { get; }

        public abstract void StartRecoding();
        public abstract IEnumerable<MicDeviceInfo> ShowDevices();
        public abstract void ChangeDevice(int deviceNumber);
    }
}