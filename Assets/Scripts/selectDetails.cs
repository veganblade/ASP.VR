using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using UnityEngine;

public class selectDetails : MonoBehaviour, IInteractable
{
    public bool isOn = true;                        //��������� ������
    private Renderer rendererr;                      
    public Color colorToFill = Color.red;           //���� ���������� ��������
    public GameObject firstObj;
    public cordinates crdSrc;
    public AddBoxColliderToChildren ABCTC;
    public playerInteraction playerI;
    public string oName = "NEW /2005-KP";

    void Start()
    {
        ABCTC = GameObject.Find("EXAMPLE-1").GetComponent<AddBoxColliderToChildren>();
        rendererr = GetComponent<Renderer>();
        firstObj = GameObject.Find("firstObject");
        crdSrc = GameObject.Find("CordinObj").GetComponent<cordinates>();
        playerI = GameObject.Find("XR Origin (XR Rig)").GetComponent<playerInteraction>();
    }
    void Update()
    {
       if(rendererr.material.color == colorToFill)          //��������� ��������, ���� ������� ������!!!
       {
            transform.SetParent(firstObj.transform);
            ABCTC.DisplayDictionary(oName);
       }
    }
    public string GetDescription()
    {
        if(isOn)
        {
            return "View";
        }
        else
        {
            return "ViewSet";
        }
    }
    public void Interact()              //�������� ������� ������ ������
    {        
        isOn = !isOn;
        rendererr.material.color = Color.red;        
    }
}
