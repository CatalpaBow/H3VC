using FistVR;
using H3MP.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace H3VC.View
{
    class WristMenuPatch
    {
        static void UpdatePrefix(FVRWristMenu2 __instance) {

        }

        static void AwakePrefix(FVRWristMenu2 __instance) {
            AddSections(__instance);
        }

        private static void AddSections(FVRWristMenu2 __instance) {
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
