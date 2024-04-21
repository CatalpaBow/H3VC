using FistVR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace H3VC.View.FVRComponent{
    public class FVRToggle : FVRPointable{
        private Toggle toggle;

        public void Awake() {
            var rect = GetComponent<RectTransform>();
            toggle = GetComponent<Toggle>();
            var cldr = this.gameObject.AddComponent<BoxCollider>();
            cldr.size = new Vector3(rect.sizeDelta.x, rect.sizeDelta.y, 0);
        }
        public override void OnPoint(FVRViveHand hand) {
            base.OnPoint(hand);
            if (hand.Input.TriggerDown && toggle != null) {
                toggle.isOn = !toggle.isOn;
            }
        }
    }
}
