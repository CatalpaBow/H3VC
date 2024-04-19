using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;
using UnityEngine;
namespace H3VC.AudioPipelines
{
    /// <summary>
    /// Audio pipeline that connects audio input and audio output.
    /// </summary>
    public class AudioPipeline{
        IAudioInput selfIn;
        IAudioOutput selfOut;

        IAudioInput otherIn;
        IAudioOutput otherOut;

        /// <param name="_inSelf">Player audio input(generally it is microphone)</param>
        /// <param name="_outSelf">Player audio output(generally it is network)</param>
        /// <param name="_inOthers">Another users audio input (generally it is coming from network</param>
        /// <param name="_outOthers">Another users audio output(generally it is output to the player audio speakers)</param>
        public AudioPipeline(IAudioInput _inSelf,IAudioOutput _outSelf,IAudioInput _inOthers,IAudioOutput _outOthers) {
            selfIn = _inSelf;
            selfOut = _outSelf;
            otherIn = _inOthers;
            otherOut = _outOthers;
            H3MP.Mod.OnConnection += StartStream;
        }

        /// <summary>
        /// Start audio stream that input to output.
        /// </summary>
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
