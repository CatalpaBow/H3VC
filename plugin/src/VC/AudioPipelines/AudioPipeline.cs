using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;
namespace H3VC.AudioPipelines
{
    public class AudioPipeline{
        IAudioInput selfIn;
        IAudioOutput selfOut;

        IAudioInput otherIn;
        IAudioOutput otherOut;

        public AudioPipeline(IAudioInput _inSelf,IAudioOutput _outSelf,IAudioInput _inOthers,IAudioOutput _outOthers) {
            selfIn = _inSelf;
            selfOut = _outSelf;
            otherIn = _inOthers;
            otherOut = _outOthers;
            H3MP.Mod.OnConnection += StartStream;
        }

        public void StartStream() {
            selfIn.OnAudioReady
                         .Subscribe(audioData => selfOut.StreamAudio(audioData.Key, audioData.Value));
            selfIn.OnMuteChanged
                         .Subscribe(isMute => selfOut.Mute(isMute.Key, isMute.Value));
            selfIn.OnSoundModeChanged
                         .Subscribe(mode => selfOut.SoundMode(mode.Key, mode.Value));


            otherIn.OnAudioReady
                         .Subscribe(audioData => otherOut.StreamAudio(audioData.Key, audioData.Value));
            otherIn.OnMuteChanged
                         .Subscribe(isMute => otherOut.Mute(isMute.Key, isMute.Value));
            otherIn.OnSoundModeChanged
                         .Subscribe(mode => otherOut.SoundMode(mode.Key, mode.Value));

        }

    }
}
