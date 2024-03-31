using H3MP;
using H3MP.Scripts;
using H3VC.Data;
using H3VC.IO;
using System.Collections.Generic;
using System.Linq;
namespace H3VC
{
    public class VoicePlayerFacade
    {
        private Dictionary<int, VoicePlayer> voicePlayerDic;

        public VoicePlayerFacade() {
            voicePlayerDic = GameManager.players
                                        .ToDictionary(pair => pair.Key, pair => AttachVoicePlayer(pair.Value));
            GameManager.OnPlayerAdded += PlayerAddedHandler;
            H3MP.Mod.OnPlayerRemoved += OnPlayerRemovedHandler;
        }

        private void OnPlayerRemovedHandler(PlayerManager player) {
            voicePlayerDic.Remove(player.ID);
        }

        public VoicePlayer AttachVoicePlayer(PlayerManager plMngr) {
            return plMngr.head.gameObject.AddComponent<VoicePlayer>();
        }
        public void DetachVoicePlayer(int id) {

        }

        public void PlayerAddedHandler(PlayerManager plMngr) {

            VoicePlayer voicePlayer = plMngr.head.gameObject.AddComponent<VoicePlayer>();
            if (voicePlayerDic.ContainsKey(plMngr.ID)) {
                voicePlayerDic.Remove(plMngr.ID);
            }
            voicePlayerDic.Add(plMngr.ID, voicePlayer);
        }

        public void Play(int id, PCMSegment sgmnt) {
            voicePlayerDic.TryGetValue(id, out VoicePlayer player);
            player?.Play(sgmnt);
        }


    }
}
