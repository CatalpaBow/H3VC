using H3MP;
using H3MP.Scripts;
using H3VC.AudioPipelines;
using H3VC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;
namespace H3VC.Speakers
{
    public class AudioSpeaker {
        public readonly int id;
        private SoundMode soundMode;

        private UnitySpeaker voicePlayer;

        public AudioSpeaker(UnitySpeaker pl,int _id) {
            this.id = _id;
            voicePlayer = pl;
            soundMode = Data.SoundMode.positional;
            voicePlayer.SetPositional(false);
        }

        public void Mute(bool isMute) {
            
        }

        public void SoundMode(SoundMode mode) {
            this.soundMode = mode;
            switch(mode){
                case Data.SoundMode.positional:
                    voicePlayer.SetPositional(true);
                    break;
                case Data.SoundMode.globalScene:
                    voicePlayer.SetPositional(false);
                    break;
            }
        }

        public void Play(PCMSegment sgmnt) {
            H3VC.Mod.Logger.LogDebug("PBSpeaker Play");
            voicePlayer.Play(sgmnt);
        }
    }
}
