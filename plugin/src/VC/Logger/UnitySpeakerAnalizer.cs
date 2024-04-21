using H3VC.Speakers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace H3VC.src.VC.Logger
{
    public class UnitySpeakerAnalizer{
        private UnitySpeaker spkr;
        private int speakrCrntPosDelay;
        private int headPos { get { return spkr.head; } }
        private int crntPos { get { return spkr.source.timeSamples; } }
        
    }
}
