using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;


public class MeshUniter : EditorWindow
{
    private string filePath = "";
    private Dictionary<string, Element[]> separatedElementsTypes = new Dictionary<string, Element[]>();
    private List<Element> separatedElements = new List<Element>();
    private string[] separatedTypes = new string[] { "NOZZ", "SUBE", "TMPL", "EQUI" };
    private GameObject[] allObjects;

    private void OnEnable()
    {
        allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
    }

    [MenuItem("Tools/Unite Mesh")]
    public static void ShowWindow()
    {
        GetWindow<MeshUniter>("Unite Mesh");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Select Text File"))
        {
            filePath = EditorUtility.OpenFilePanel("Select Text File", "", "txt");
            if (!string.IsNullOrEmpty(filePath))
            {
                getTypesOfElements(filePath);
            }
        }

        if (GUILayout.Button("Unite Selected Meshes"))
        {
            UniteSelectedMeshes();
        }
    }

    private void getTypesOfElements(string path)
    {
        string[] lines = File.ReadAllLines(path); ;

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            if (line.Contains(" TYPE:=") && i >= 2)
            {
                Element element = new Element();
                element.type = line.Trim().Replace("TYPE:=", "").Trim();
                element.name = lines[i - 2].Replace("\u00A0", "").Trim().Replace("NEW ", "").Trim();
                if (separatedTypes.Contains(element.type))
                {
                    separatedElements.Add(element);
                }
            }
        }

        separatedElementsTypes = separatedElements.GroupBy(key => key.type).ToDictionary(g => g.Key, g => g.ToArray());
    }

    private void UniteSelectedMeshes()
    {
        GameObject[] selectedObjects = Selection.gameObjects;

        if (selectedObjects.Length < 1)
        {
            EditorUtility.DisplayDialog("Error", "Select at least one object with MeshFilter components.", "OK");
            return;
        }

        UniteEqui();

        foreach (GameObject obj in selectedObjects)
        {
            List<MeshFilter> meshFilters = new List<MeshFilter>();
            FindMeshRecursive(obj, ref meshFilters);
            if (meshFilters.Count < 1)
            {
                continue;
            }
            CombineMeshes(meshFilters, $"CombMesh->{obj.name}");
        }

        EditorUtility.DisplayDialog("Success", "Meshes have been united successfully.", "OK");
    }

    private void UniteEqui()
    {
        if (separatedElementsTypes.Keys.Contains("EQUI"))
        {
            List<GameObject> toDelete = new List<GameObject>();

            foreach (Element element in separatedElementsTypes["EQUI"])
            {
                GameObject obj = allObjects.FirstOrDefault(key => key.name == element.name);
                if (obj != null)
                {
                    List<MeshFilter> meshFilters = new List<MeshFilter>();
                    FindMeshRecursiveEqui(obj, ref meshFilters, ref toDelete);
                    if (meshFilters.Count < 1)
                    {
                        continue;
                    }
                    CombineMeshes(meshFilters, $"UniteMesh->{obj.name}", obj.transform.parent);
                }
            }

            foreach (GameObject deletedObj in toDelete)
            {
                DestroyImmediate(deletedObj);
            }
        }
    }

    private void FindMeshRecursiveEqui(GameObject obj, ref List<MeshFilter> meshFilters, ref List<GameObject> toDelete)
    {
        MeshFilter meshFilter = obj.GetComponent<MeshFilter>();
        if (meshFilter != null && !separatedElements.Any(key => key.name == obj.name))
        {
            meshFilters.Add(meshFilter);
        }

        foreach (Transform child in obj.transform)
        {
            FindMeshRecursiveEqui(child.gameObject, ref meshFilters, ref toDelete);
        }

        if (!separatedElements.Any(key => key.name == obj.name))
        {
            toDelete.Add(obj);
        }
    }

    private void FindMeshRecursive(GameObject obj, ref List<MeshFilter> meshFilters)
    {
        MeshFilter meshFilter = obj.GetComponent<MeshFilter>();
        if (meshFilter != null)
        {
            meshFilters.Add(meshFilter);
        }

        foreach (Transform child in obj.transform)
        {
            FindMeshRecursive(child.gameObject, ref meshFilters);
        }
    }

    private void CombineMeshes(List<MeshFilter> meshFilters, string name, Transform parentTransform = null)
    {
        GameObject combinedObject = new GameObject($"{name}");
        MeshFilter combinedMeshFilter = combinedObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = combinedObject.AddComponent<MeshRenderer>();
        if (parentTransform != null)
        {
            combinedObject.transform.parent = parentTransform;
        }

        CombineInstance[] combine = new CombineInstance[meshFilters.Count];
        Material combinedMaterial = null;

        for (int i = 0; i < meshFilters.Count; i++)
        {
            MeshFilter meshFilter = meshFilters[i];
            Mesh mesh = meshFilter.sharedMesh;
            if (combinedMaterial == null)
            {
                combinedMaterial = meshFilter.GetComponent<Renderer>().sharedMaterial;
            }

            CombineInstance combineInstance = new CombineInstance
            {
                mesh = mesh,
                transform = meshFilter.transform.localToWorldMatrix
            };

            combine[i] = combineInstance;
        }

        Mesh combinedMesh = new Mesh
        {
            indexFormat = UnityEngine.Rendering.IndexFormat.UInt32
        };
        combinedMesh.CombineMeshes(combine, true, true);

        combinedMeshFilter.mesh = combinedMesh;
        meshRenderer.sharedMaterial = combinedMaterial;
    }
}