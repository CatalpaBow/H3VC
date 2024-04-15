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

namespace H3VC.View
{
    public class H3VCViewPatch{
        [HarmonyPatch(typeof(FVRWristMenu2), nameof(FVRWristMenu2.Update)), HarmonyPrefix]
        private static void PrefixUpdate() {

        }
        [HarmonyPatch(typeof(FVRWristMenu2), nameof(FVRWristMenu2.Awake)),HarmonyPrefix]
        private static void PrefixAwake(FVRWristMenu2 __instance) {
            H3VC.Mod.Logger.LogInfo("HarmonyPatch worked!");
            GameObject section = new GameObject("Section_H3VC", typeof(RectTransform));
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
