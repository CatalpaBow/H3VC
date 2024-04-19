using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace H3VC.View{
    public static class AssetBundleLoader{
        private static AssetBundle bundle;
        public static AssetBundle Load() {
            if(bundle != null) {
                return bundle;
            }
            //dllPath
            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string assetPath = Directory.GetParent(path).FullName + "\\miccfgview";
            bundle = UnityEngine.AssetBundle.LoadFromFile(assetPath);
            return bundle;
        }
    }
}
