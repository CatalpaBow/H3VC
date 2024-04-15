using H3VC.AudioPipelines;
using H3VC.Data;
using H3VC.VCUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;
namespace H3VC.Speakers
{
    public class PlayerAudioSpeakers : IAudioOutput{
        public List<AudioSpeaker> speakerList;
        private readonly VCUserList userList;
        public PlayerAudioSpeakers(VCUserList _userList) {
            this.userList = _userList;
            /* ユーザーリストを初期化 */
            speakerList = userList.crntUserIDs
                                  .Select(VoiceSpeakerBuilder.Build)
                                  .ToList();

            /* 参加時ボイススピーカーを作成しリストに追加 */
            userList.OnJoined
                    .Select(VoiceSpeakerBuilder.Build)
                    .Where(spkr => spkr is not null)
                    .Subscribe(speakerList.Add);

            /* 退出時ボイススピーカーリストから削除 */
            userList.OnLeaved
                    .Subscribe(id => speakerList.RemoveAll(spkr => spkr.id == id));

        }

        public void StreamAudio(int id, PCMSegment sgmnt) {
            //H3VC.Mod.Logger.LogDebug("SpeakerList PlayVoice");
            speakerList.Find(spkr => spkr.id == id)
                      ?.Play(sgmnt);
        }

        public void Mute(int id, bool isMute) {
            speakerList.Find(spkr => spkr.id == id)
                      ?.Mute(isMute);
        }

        public void SoundMode(int id, SoundMode mode) {
            speakerList.Find(spkr => spkr.id == id)
                      ?.SoundMode(mode);
        }
    }
}
