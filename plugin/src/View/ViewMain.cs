using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace H3VC.src.View
{
    public static class ViewMain{

        public static void Intialize() {
            Harmony.CreateAndPatchAll(typeof(H3VC.View.VoiceRcoderWristMenuPatch));
        }
    }
}
