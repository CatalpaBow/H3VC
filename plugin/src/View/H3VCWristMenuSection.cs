using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FistVR;
using UnityEngine;

using UnityEngine.UI;
namespace H3VC.View
{
    public class H3VCWristMenuSection : FVRWristMenuSection{

        public override void Enable() {
            
        }

        private void Awake() {
            Image background = gameObject.AddComponent<Image>();
            background.rectTransform.sizeDelta = new Vector2(500, 350);
            background.color = new Color(0.1f, 0.1f, 0.1f, 1);
        }
    }
}
