using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class floor : MonoBehaviour
{
    public GameObject platform;
    public float xLength;
    public float yLength;
    public AddBoxColliderToChildren ABCTC;
    public GameObject[] objects;
    //����� ��� ������� �� ����� ������
    //��������� �� ����� ������� �������� ���������
    //����������� � ���������� ��������� ������ � ��������� � ���������
    //���������
    void Start()
    {
        objects = GameObject.FindObjectsOfType<GameObject>();
    }
    void Update()
    {
        /*gameObject.AddComponent<Rigidbody>();*/
        foreach (GameObject obj in objects)
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            if(/*renderer != null && */renderer.bounds.size.x > 5) 
            {
/*                if (obj.GetComponent<BoxCollider>() == null)
                {*/
                    Debug.Log("���� ������ ������ 5 � �����");          //����� ��������� � ������� ��� �������� ������������ ������� 
                    obj.AddComponent<AddBoxColliderToChildren>();          //�������� ������� ��� ���� ����� �������
                    /*obj.AddComponent<Rigidbody>();*/
/*               }*/
            }
        }
    }
}
