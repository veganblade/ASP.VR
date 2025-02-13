using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class replaceMaterial : MonoBehaviour
{
    public string targetMaterialName = "���� 167Translucent(0.00)";
    public Material newMaterial;
    void Start()
    {
        Renderer[] renderers = FindObjectsOfType<Renderer>();
        foreach(Renderer renderer in renderers)
        {
            foreach(Material material in renderer.materials)
            {
                if(material.name == targetMaterialName)
                {
                    renderer.material = newMaterial;
                    Debug.Log($"�������� ������ � �������: {renderer.gameObject.name}");
                    break;
                }
            }
        }
    }
    void Update()
    {

    }
}
