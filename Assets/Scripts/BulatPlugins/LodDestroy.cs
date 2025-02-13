using UnityEngine;
using UnityEditor;

public class DeleteLOD1ObjectsEditor : EditorWindow
{
    [MenuItem("Tools/Delete LOD1 Objects")]
    public static void ShowWindow()
    {
        GetWindow<DeleteLOD1ObjectsEditor>("Delete LOD1 Objects");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Delete All LOD1 Objects"))
        {
            DeleteLOD1Objects();
        }
    }

    private void DeleteLOD1Objects()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>(true); // true для поиска в неактивных объектах

        foreach (GameObject obj in allObjects)
        {
            if (obj.name.EndsWith("LOD3"))
            {
                DestroyImmediate(obj);
            }
        }

        Debug.Log("Deleted all objects ending with 'LOD1' in their name.");
    }
}


