using H3VC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace H3VC.AudioPipelines
{
    public interface IAudioOutput{
        void StreamAudio(int id,PCMSegment sgmnt);
        void Mute(int id,bool isMute);
        void SoundMode(int id, SoundMode mode);
    }
}
