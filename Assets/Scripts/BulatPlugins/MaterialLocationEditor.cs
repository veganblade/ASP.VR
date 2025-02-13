using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class MaterialLocationEditor : MonoBehaviour
{
    [MenuItem("Tools/Set Materials Location to Use External Materials")]
    static void SetMaterialsLocationToExternal()
    {
        string assetFolderPath = "Assets";
        string[] modelPaths = Directory.GetFiles(assetFolderPath, "*.fbx", SearchOption.AllDirectories);

        foreach (string modelPath in modelPaths)
        {
            ModelImporter modelImporter = AssetImporter.GetAtPath(modelPath) as ModelImporter;

            if (modelImporter != null)
            {
                modelImporter.materialLocation = ModelImporterMaterialLocation.External;

                AssetDatabase.WriteImportSettingsIfDirty(modelPath);
                AssetDatabase.ImportAsset(modelPath, ImportAssetOptions.ForceUpdate);
            }
        }
        Debug.Log("Materials Location has been set to Use External Materials for all models in Assets folder.");
    }
}
