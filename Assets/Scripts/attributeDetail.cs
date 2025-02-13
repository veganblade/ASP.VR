using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class attributeDetail : MonoBehaviour
{
    public GameObject selectDetails;        //объект в юнити который будем сравнивать
    public string objectName;
    [SerializeField] private string[] parts;
    [SerializeField] private List<string> matchingAtributes = new List<string>();
    public AddBoxColliderToChildren ABCTC;
    
    void Start()
    {
        //ОТОБРАЖЕНИЕ АТРИБУТОВ
        ABCTC = GameObject.Find("EXAMPLE-1").GetComponent<AddBoxColliderToChildren>();
        Debug.Log("XYI");
        selectDetails = this.gameObject;
        objectName = selectDetails.name;     //получаем имя объекта
        foreach (string line in ABCTC.lines)
        {
            parts = line.Split(new char[] { ':', '=' }, StringSplitOptions.RemoveEmptyEntries);           //разделяет строку на части по символу ":"
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
                            Debug.Log("Найдены совпадения: ");
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
                    Debug.Log("Совпадения не найдены");
                }
                if (parts[0] == "END")                    //если имя объекта в текстовом документе совпадает с именем в юнити 
                {
                    Debug.Log("Description: " + parts[1]);
                    break;                                //выход из цикла, так как нашли нужное описание
                }
            }
            else
            {
                Debug.LogError("File not found: " );  //выводим ошибку, если файл не найден
            }
        }
    }
}
