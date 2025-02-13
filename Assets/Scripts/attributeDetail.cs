using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class attributeDetail : MonoBehaviour
{
    public GameObject selectDetails;        //������ � ����� ������� ����� ����������
    public string objectName;
    [SerializeField] private string[] parts;
    [SerializeField] private List<string> matchingAtributes = new List<string>();
    public AddBoxColliderToChildren ABCTC;
    
    void Start()
    {
        //����������� ���������
        ABCTC = GameObject.Find("EXAMPLE-1").GetComponent<AddBoxColliderToChildren>();
        Debug.Log("XYI");
        selectDetails = this.gameObject;
        objectName = selectDetails.name;     //�������� ��� �������
        foreach (string line in ABCTC.lines)
        {
            parts = line.Split(new char[] { ':', '=' }, StringSplitOptions.RemoveEmptyEntries);           //��������� ������ �� ����� �� ������� ":"
            if (parts.Length == 2)
            {
                string attributeName = parts[0].Trim();
                string attributeValue = parts[1].Trim();
                if (attributeName == objectName)
                {
                    if (attributeValue == "NAME")
                    {
                        matchingAtributes.Add(line);
                        Debug.Log("Details: " + attributeValue);
                        if (matchingAtributes.Count > 0)
                        {
                            Debug.Log("������� ����������: ");
                            foreach (string attribute in matchingAtributes)
                            {
                                Debug.Log(attribute);
                                break;
                            }
                        }
                    }
                }
                else
                {
                    Debug.Log("���������� �� �������");
                }
                if (parts[0] == "END")                    //���� ��� ������� � ��������� ��������� ��������� � ������ � ����� 
                {
                    Debug.Log("Description: " + parts[1]);
                    break;                                //����� �� �����, ��� ��� ����� ������ ��������
                }
            }
            else
            {
                Debug.LogError("File not found: " );  //������� ������, ���� ���� �� ������
            }
        }
    }
}
