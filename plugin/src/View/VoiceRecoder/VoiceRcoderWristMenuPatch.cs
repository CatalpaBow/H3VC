using FistVR;
using H3MP.Patches;
using H3MP.Scripts;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using static System.Collections.Specialized.BitVector32;

namespace H3VC.View
{
    /// <summary>
    /// Add H3VC button in Wrist menu.
    /// </summary>
    public class VoiceRcoderWristMenuPatch : MonoBehaviour{
        [HarmonyPatch(typeof(FVRWristMenu2), nameof(FVRWristMenu2.Update)), HarmonyPrefix]
        private static void PrefixUpdate() {

        }
        [HarmonyPatch(typeof(FVRWristMenu2), nameof(FVRWristMenu2.Awake)),HarmonyPrefix]
        private static void PrefixAwake(FVRWristMenu2 __instance) {
            GameObject section = new GameObject("Section_H3VC", typeof(RectTransform));
            /* 
             * The current AssetBundle loader implementation loads the file when executing the Load method.
             * An implementation that preloads the AssetBundle in advance and passes the preloaded AssetBundle when executing the load does not require file IO and seems to be more efficient.
             * Scheduled to be refactored after Ver1.0 release
             */
            AssetBundle assetBundle = AssetBundleLoader.Load();
            GameObject prefab = assetBundle.LoadAsset<GameObject>("MicSettings");
            var guiGmObj = Instantiate(prefab);
            guiGmObj.transform.parent = section.transform;
            VoiceRecoderView.SingleInstance.Value = guiGmObj.GetComponent<VoiceRecoderView>();

            section.transform.SetParent(__instance.MenuGO.transform);
            section.transform.localPosition = new Vector3(0, 300, 0);
            section.transform.localRotation = Quaternion.identity;
            section.transform.localScale = Vector3.one;
            section.GetComponent<RectTransform>().sizeDelta = new Vector2(350, 350);
            FVRWristMenuSection sectionScript = section.AddComponent<H3VCWristMenuSection>();
            sectionScript.ButtonText = "H3VC";
            __instance.Sections.Add(sectionScript);
            section.SetActive(false); 
            
        }
    }
}
