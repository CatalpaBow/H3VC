using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using H3VC.View;
namespace H3VC.Test
{
    public class AssetBundleLoadTest{
        public static void Test() {
            AssetBundle assetBundle = AssetBundleLoader.Load();
            GameObject gmObj = assetBundle.LoadAsset<GameObject>("MicSetting");
            if(gmObj == null) {
                H3VC.Mod.Logger.LogError("asset is null");
                return;
            }
            H3VC.Mod.Logger.LogInfo("Load is sucsessed!");
        }
    }
}
