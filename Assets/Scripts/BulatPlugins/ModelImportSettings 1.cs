using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ModelImportSettings : AssetPostprocessor
{
    void OnPreprocessModel()
    {
        ModelImporter modelImporter = assetImporter as ModelImporter;

        if (modelImporter != null)
        {
            modelImporter.materialLocation = ModelImporterMaterialLocation.External;
        }
    }
}
