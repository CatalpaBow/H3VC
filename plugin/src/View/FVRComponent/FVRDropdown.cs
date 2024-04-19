using FistVR;
using System.Collections;
using System.Collections.Generic;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;
namespace H3VC.View.FVRComponent{
    public class FVRDropdown : FVRPointable{
        private Dropdown dropdown;

        public void Awake() {
            var rect = GetComponent<RectTransform>();
            dropdown = GetComponent<Dropdown>();
            var cldr = this.gameObject.AddComponent<BoxCollider>();
            cldr.size = new Vector3(rect.sizeDelta.x, rect.sizeDelta.y, 0);
        }
        public override void OnPoint(FVRViveHand hand) {
            base.OnPoint(hand);
            if (hand.Input.TriggerDown && dropdown != null) {
                dropdown.Show();
            }
        }
        
    }
}