using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class AssetBundleEditor
{
    static string root_path = "AssetBundles";
    static string variant = "assetbundle";

    [MenuItem("アセットバンドル / ビルド(Windows64 向け)", false)]
    static private void BuildAssetBundlesForWindows64() {
        // 他プラットフォームを対象にする場合はここを変更する(今回は Windows64 向け)
        UnityEditor.BuildTarget target_platform = UnityEditor.BuildTarget.StandaloneWindows64;

        var output_path = System.IO.Path.Combine(root_path, target_platform.ToString());

        if (System.IO.Directory.Exists(output_path) == false) {
            System.IO.Directory.CreateDirectory(output_path);
        }

        var asset_bundle_build_list = new List<UnityEditor.AssetBundleBuild>();
        foreach (string asset_bundle_name in UnityEditor.AssetDatabase.GetAllAssetBundleNames()) {
            var builder = new AssetBundleBuild();
            builder.assetBundleName = asset_bundle_name;
            builder.assetNames = UnityEditor.AssetDatabase.GetAssetPathsFromAssetBundle(builder.assetBundleName);
            builder.assetBundleVariant = variant;
            asset_bundle_build_list.Add(builder);
        }
        if (asset_bundle_build_list.Count > 0) {
            UnityEditor.BuildPipeline.BuildAssetBundles(
                output_path,
                asset_bundle_build_list.ToArray(),
                UnityEditor.BuildAssetBundleOptions.ChunkBasedCompression,
                target_platform
            );
        }
    }
}