using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class RaycastOnClick : MonoBehaviour
{
    GameObject[] allObjects;
    GameObject originalGameObj = null;
    GameObject combinedMeshGameObj = null;

    private void Start()
    {
        allObjects = GameObject.FindObjectsOfType<GameObject>(true);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (originalGameObj != null && combinedMeshGameObj != null)
            {
                originalGameObj.SetActive(false);
                combinedMeshGameObj.SetActive(true);
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.transform.parent != null)
                {
                    combinedMeshGameObj = hit.collider.transform.parent.gameObject;
                }
                else
                {
                    combinedMeshGameObj = hit.collider.gameObject;
                }

                if (combinedMeshGameObj != null)
                {
                    originalGameObj = allObjects.FirstOrDefault(key => key.name == hit.collider.name.Split("->")[1]);
                }

                if (originalGameObj != null)
                {
                    originalGameObj.SetActive(true);
                    if (hit.collider.transform.parent != null)
                    {
                        hit.collider.transform.parent.gameObject.SetActive(false);
                    }
                    else
                    {
                        hit.collider.gameObject.SetActive(false);
                    }
                    if (Physics.Raycast(ray, out hit))
                    {
                        Debug.Log("Попал в объект: " + hit.collider.name);
                    }
                }
            }
            else
            {
                Debug.Log("Ничего не найдено.");
            }
        }
    }
}
