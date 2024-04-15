using H3VC.Speakers;
using H3VC.VoiceRecoders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UniRx;
namespace H3VC.Test
{
    internal class AudioTest{
        private VoiceRecoder recoder;
        private UnitySpeaker spkr;
        public AudioTest() {
            H3VC.VoiceRecoders.MicImpement.NAudioMic mic = new H3VC.VoiceRecoders.MicImpement.NAudioMic();
            recoder = new VoiceRecoder(mic);

            spkr = new GameObject().AddComponent<UnitySpeaker>();
            spkr.SetPositional(false);
            recoder.OnAudioReady.Subscribe(data => spkr.Play(data.Value));
        }


    }
}
