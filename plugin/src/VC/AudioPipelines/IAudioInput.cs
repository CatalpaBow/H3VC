using H3VC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;
namespace H3VC.AudioPipelines
{
    public interface IAudioInput{
        IObservable<KeyValuePair<int, PCMSegment>> OnAudioReady { get; }
        IObservable<KeyValuePair<int, bool>> OnMuteChanged { get; }
        IObservable<KeyValuePair<int,SoundMode>> OnSoundModeChanged { get; }

    }
}
