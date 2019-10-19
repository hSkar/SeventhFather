using UnityEngine;
using UnityEditor;

public class SetQuillMaterialsOnImport : AssetPostprocessor
{
    public Material OnAssignMaterialModel(Material mat, Renderer renderer)
    {
        string materialPath = "Assets/Materials/";
        string singleSidedMatName = "quill_singleSided_Mat.mat";
        string doubleSidedMatName = "quill_doubleSided_Mat.mat";
        Material newMaterial = new Material(mat);

        if (assetPath.Contains("_quill"))
        {
            if(mat.name.IndexOf("double", 0, mat.name.Length, System.StringComparison.CurrentCultureIgnoreCase) != -1)
            {
                Debug.Log("Setting double sided material on imported quill object");
               return AssetDatabase.LoadAssetAtPath(materialPath + doubleSidedMatName, typeof(Material)) as Material;
            }
            else if(mat.name.IndexOf("single", 0, mat.name.Length, System.StringComparison.CurrentCultureIgnoreCase) != -1)
            {
                Debug.Log("Setting single sided material on imported quill object");
                return AssetDatabase.LoadAssetAtPath(materialPath + singleSidedMatName, typeof(Material)) as Material;
            }
        }

        return mat;
    }

    void OnPreprocessModel()
    {
        if (assetPath.Contains("_quill"))
        {
            ModelImporter modelImporter = assetImporter as ModelImporter;
            modelImporter.useFileScale = false;
            modelImporter.importMaterials = false;
            
        }
    }
}
