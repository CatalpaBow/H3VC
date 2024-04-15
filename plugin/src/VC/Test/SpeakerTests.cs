using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using H3VC.Speakers;
using H3MP;
using UnityEngine.Assertions;
namespace H3VC.Test
{
    public class SpeakerTests{
        private PlayerAudioSpeakers spkrs;

        public SpeakerTests(PlayerAudioSpeakers _spkrs) {
            this.spkrs = _spkrs;
        }

        
        public bool UserCountTests() {
            return spkrs.speakerList.Count == H3MP.GameManager.players.Count;
        }
        

    }
}
