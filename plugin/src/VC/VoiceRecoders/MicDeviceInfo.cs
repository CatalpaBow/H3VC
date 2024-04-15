using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace H3VC.VoiceRecoders
{
    public class MicDeviceInfo{
        public readonly int deviceID;
        public string name { get; private set; }
        
        public MicDeviceInfo(int _deviceID,string _name) {
            deviceID = _deviceID;
            this.name = _name;
        }
    }
}
