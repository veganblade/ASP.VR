using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UniColliderInterpolator;
using UnityEditor;
using UnityEngine;

public class CreateCollders : EditorWindow
{
    //private float _divisionUnitLength = 0.05f;
    GameObject[] allObjects;
    List<GameObject> inactiveObjects = new List<GameObject>();

    private void OnEnable()
    {
        allObjects = GameObject.FindObjectsOfType<GameObject>();
    }

    [MenuItem("Tools/Collider Maker tool")]
    public static void ShowWindow()
    {
        GetWindow<CreateCollders>("Collider Maker tool");
    }

    private void OnGUI()
    {
        GUILayout.Label("Collider Maker tool", EditorStyles.boldLabel);
        if (GUILayout.Button("Add Colliders"))
        {
            GameObject[] selectedObjects = Selection.gameObjects;
            foreach (GameObject obj in selectedObjects)
            {
                AddBoxCollidersRecursive(obj);
            }
        }
    }

    private void AddBoxCollidersRecursive(GameObject obj)
    {
        MeshFilter meshFilter = obj.GetComponent<MeshFilter>();

        if (meshFilter != null)
        {
            if (obj.GetComponent<MeshCollider>() == null)
            {
                if (meshFilter.sharedMesh.triangles.Length > 100000)
                {
                    GenerateMeshForLargePolyObject(obj);
                }
                else
                {
                    MeshCollider meshCollider = obj.AddComponent<MeshCollider>();
                    meshCollider.sharedMesh = meshFilter.sharedMesh;
                }
            }
        }

        foreach (Transform child in obj.transform)
        {
            AddBoxCollidersRecursive(child.gameObject);
        }
    }

    private void GenerateMeshForLargePolyObject(GameObject gameObject)
    {
        DiactivateAllExceptCurrent(gameObject);

        Selection.activeGameObject = gameObject;

        ColliderInterpolator interpolator = gameObject.AddComponent<ColliderInterpolator>();
        if (interpolator != null && interpolator.isActiveAndEnabled)
        {
            interpolator.Generate();
        }

        ActivateDiactivated();
    }

    private void DiactivateAllExceptCurrent(GameObject currentObj)
    {
        HashSet<GameObject> ancestors = new HashSet<GameObject>();

        // Находим всех предков (родителей и вышестоящие объекты)
        Transform currentTransform = currentObj.transform;
        while (currentTransform != null)
        {
            ancestors.Add(currentTransform.gameObject);
            currentTransform = currentTransform.parent;
        }

        foreach (GameObject go in allObjects)
        {
            // Деактивируем объект, если он не текущий и не является предком текущего
            if (!ancestors.Contains(go) && go.activeSelf)
            {
                go.SetActive(false);
                inactiveObjects.Add(go);
            }
        }
    }

    private void ActivateDiactivated()
    {
        foreach (GameObject go in inactiveObjects)
        {
            go.SetActive(true);
        }
    }
}

public class Element
{
    public string name { get; set; }
    public string type { get; set; }
}
