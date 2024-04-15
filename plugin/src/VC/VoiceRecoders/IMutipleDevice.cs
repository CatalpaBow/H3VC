﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace H3VC.VoiceRecoders
{
    public interface IMutipleDevice{
        IEnumerable<MicDeviceInfo> ShowDevices();
        void ChangeDevice(int deviceID);
    }
}
