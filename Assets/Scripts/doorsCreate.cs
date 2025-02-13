using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorsCreate : MonoBehaviour
{
    public List<GameObject> doorsObjects = new List<GameObject>(); 
    // Start is called before the first frame update
    void Start()
    {
        FindDoorsObjects();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FindDoorsObjects()
    {
        //�������� ��� ������� �� �����
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach(GameObject obj in allObjects)
        {
            //�������� �� ������� � ����� "FITTING"
            if (obj.name.StartsWith("FITTING"))
            {
                {
                    doorsObjects.Add(obj);
                }
            }
            //�������� MeshCollider
            RemoveMeshColliders();
        }
    }
    void RemoveMeshColliders()
    {
        foreach(GameObject obj in doorsObjects)
        {
            MeshCollider meshCollider = obj.GetComponent<MeshCollider>();
            if(meshCollider != null)
            {
                Destroy(meshCollider);
            }
        }
    }
}
