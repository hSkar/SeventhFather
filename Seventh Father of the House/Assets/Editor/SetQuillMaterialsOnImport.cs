using UnityEngine;
using UnityEditor;

public class SetQuillMaterialsOnImport : AssetPostprocessor
{
    public void OnAssignMaterialModel(Material mat, Renderer renderer)
    {
        string materialPath = "Assets/Materials";
        string singleSidedMatName = "quill_singleSided_Mat.mat";
        string doubleSidedMatName = "quill_doubleSided_Mat.mat";

        if (assetPath.Contains("_quill"))
        {
            if(mat.name.IndexOf("double", 0, mat.name.Length, System.StringComparison.CurrentCultureIgnoreCase) != -1)
            {
                var doubleSidedMat = AssetDatabase.LoadAssetAtPath<Material>(materialPath + doubleSidedMatName);
                mat = doubleSidedMat;

            }else if(mat.name.IndexOf("single", 0, mat.name.Length, System.StringComparison.CurrentCultureIgnoreCase) != -1)
            {
                var singleSidedMat = AssetDatabase.LoadAssetAtPath<Material>(materialPath + singleSidedMatName);
                mat = singleSidedMat;
            }
        }
    }

    void OnPreprocessModel()
    {
        if (assetPath.Contains("_quill"))
        {
            ModelImporter modelImporter = assetImporter as ModelImporter;
            modelImporter.useFileScale = false;
            
        }
    }
}
