using UnityEngine;
using System.IO;
using UnityEditor;

public static class EndingGenerator
{
    // Crea la herramienta para generar los finales
    [MenuItem("Tools/Generate Ending (243)")]

    public static void GenerateEndings()
    {
        // Se maneja la totalidad de finales
        const int totalEndings = 243;

        // Se guarda el path donde se deben crear
        string folderPath = "Assets/_Scripts/UI/Endings/ScriptableObjects";

        // Por cada uno de los finales se genera un SO y se almacena en el path
        for (int i = 0; i < totalEndings; i++)
        {
            EndingsSO ending = ScriptableObject.CreateInstance<EndingsSO>();
            ending.endingIndex = i+1;

            string assetPath = $"{folderPath}/Ending_{i+1:D3}.asset";
            AssetDatabase.CreateAsset(ending, assetPath);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("✔ 243 finales creados correctamente");

    }
}
