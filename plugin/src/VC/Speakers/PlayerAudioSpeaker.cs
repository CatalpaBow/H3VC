using H3MP;
using H3MP.Scripts;
using H3VC.AudioPipelines;
using H3VC.Data;
using RUST.Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;
namespace H3VC.Speakers{

    /// <summary>
    /// PlayUserVoice and manage voice state.
    /// </summary>
    public class PlayerAudioSpeaker {
        public readonly int id;
        private SoundMode soundMode = Data.SoundMode.positional;
        private bool isMute = false;

        private UnitySpeaker voicePlayer;

        public PlayerAudioSpeaker(UnitySpeaker pl,int _id) {
            this.id = _id;
            voicePlayer = pl;
            soundMode = Data.SoundMode.positional;
            voicePlayer.SetPositional(false);
        }

        public void Mute(bool isMute) {
            voicePlayer.DontPlaySound(isMute);
            this.isMute = isMute;
        }

        public void SoundMode(SoundMode mode) {
            H3VC.Mod.Logger.LogInfo("UserID:" + id + " is SoundModeChanged");
            this.soundMode = mode;
            if(mode == Data.SoundMode.positional) {
                voicePlayer.SetPositional(true);
            } else {
                voicePlayer.SetPositional(false);
            }
        }

        public void Play(PCMSegment sgmnt) {
            voicePlayer.Play(sgmnt);

            if (isMute) {
                Mute(true);
                return;
            }
            string thisSpeakerUserScene = H3MPWrapper.Players.CurrentScene(id);
            int idSelf = H3MPWrapper.Players.IDSelf();
            string userSelfScene = H3MPWrapper.Players.CurrentScene(idSelf);
            bool isSameScene = thisSpeakerUserScene == userSelfScene;
            if ( (soundMode == Data.SoundMode.localScene) || (soundMode == Data.SoundMode.positional)) {
                if (!isSameScene) {
                    voicePlayer.DontPlaySound(true);
                    return;
                }
            }
            voicePlayer.DontPlaySound(false);
        }
    }
}
