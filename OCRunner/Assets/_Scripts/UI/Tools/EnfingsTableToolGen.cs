using UnityEditor;
using UnityEditor.Localization;
using UnityEngine;

public class EnfingsTableToolGen : MonoBehaviour
{
    // Se crea la herramienta
    [MenuItem("Tools/Generate Ending Entries")]

    // Método para generar las entradas de la tabla de generación
    public static void GenerateEndings()
    {
        const int total = 243;
        const string tableName = "Endings";

        var collection = LocalizationEditorSettings.GetStringTableCollection(tableName);

        foreach (var table in collection.StringTables)
        {
            for (int i = 0; i < total; i++)
            {
                string key = $"ENDING_{i+1}";

                if (table.GetEntry(key) == null)
                {
                    table.AddEntry(key, $"[Texto de entrada_{i+1}]");
                }
            }
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
