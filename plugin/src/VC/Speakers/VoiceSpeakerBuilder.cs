using H3VC.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using H3MP.Scripts;
using H3MP;
namespace H3VC.Speakers
{
    public static class VoiceSpeakerBuilder{
        public static PlayerAudioSpeaker Build(int id) {
            PlayerManager plMngr;
            if (GameManager.players.TryGetValue(id, out plMngr) == false) {
                return null;
            };
            UnitySpeaker spkr = plMngr.head.gameObject.AddComponent<UnitySpeaker>();

            return new PlayerAudioSpeaker(spkr,id);
        }
    }
}
