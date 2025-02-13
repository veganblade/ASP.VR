using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.IO;
using System;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEditor.UI;
using TMPro.Examples;

public class AddBoxColliderToChildren : MonoBehaviour
{
    [SerializeField] private GameObject modelEP;
    public GameObject modelka;
    public GameObject addBox;
    [SerializeField] public string[] lines;
    public string filePath;
    public List<string> namesStartsWithSlash = new List<string>();
    public string[] allNames;
    public string[] allAttrs;
    [SerializeField] public Dictionary<string, Dictionary<string, string>> namesAndAttrs;
    [SerializeField] public Dictionary<string, string> attrs;
    public TextMeshProUGUI nameText;
/*  public string objectName = "GENSEC 28 of SBFRAMEWORK /Copy-of-ЕП600КЖтест(KM1)";*/
    void Start()
    {
        //Получаем все компоненты скрипта ExampleScript на родительском объекте и его дочерних объектах
        /*modelEP = GameObject.FindGameObjectsWithTag("example");*/
/*        foreach (GameObject obj in modelEP)*/
        if(gameObject.CompareTag("example"))
        {
            AddCollidersRecursively(modelEP.transform);
            filePath = Application.dataPath + "/ObjectDescription.txt"; //путь к текстовому файлу с описаниями
            if (File.Exists(filePath))
            {
                lines = File.ReadAllLines(filePath);   //считывает все строки из файла
            }
            string attrsPath = @"C:\Users\ds_chuvashev\ASP\Assets\ObjectDescription.txt";
            string namesPath = @"C:\Users\ds_chuvashev\ASP\ObjectNames.txt";
            namesAndAttrs = new Dictionary<string, Dictionary<string, string>>();
            allNames = File.ReadAllLines(namesPath);
            namesStartsWithSlash = new List<string>();
            foreach (string line in allNames)
            {
                if (line.StartsWith("/"))
                {
                    namesStartsWithSlash.Add(line);
                }
            }
            allAttrs = File.ReadAllLines(attrsPath);
            for (int i = 0; i < allAttrs.Length; i++)
            {
                if (namesStartsWithSlash.Any(name => allAttrs[i].Contains($"NEW {name}")))
                {
                    attrs = new Dictionary<string, string>();
                    int j = i + 1;
                    while (allAttrs[j].Contains(":="))
                    {
                        string[] attrAndValue = allAttrs[j].Split(new string[] { ":=" }, StringSplitOptions.None);
                        attrs[attrAndValue[0].Trim()] = attrAndValue[1].Trim();
                        j++;
                    }
                    string name = allAttrs[i].Replace("NEW ", "").Trim();
                    namesAndAttrs[allAttrs[i]] = attrs;
                    Debug.Log(namesAndAttrs[allAttrs[i]]);
                }
            }
        }
    }
    public void DisplayDictionary(string objectName)
    {
        string dictionaryString = "Свойства объекта: \n";
        foreach(var outerPair in namesAndAttrs)
        {
            /*Debug.Log(namesAndAttrs[name].ToString());*/
                Debug.Log("HI");
                dictionaryString += outerPair.Key + ":\n";
                foreach (var innerPair in outerPair.Value)
                {
                    dictionaryString += " " + "<color=black>" + innerPair.Key + " </color>" + " = " + "<color=white>" + innerPair.Value + "</color>" + "\n";
                }
                break;
        }
        if (namesAndAttrs[objectName].ToString() == objectName)
        {
            Dictionary<string, string> attributes = namesAndAttrs[objectName];
            foreach (var attribute in attributes)
            {
                Debug.Log($"Attribute: {attribute.Key}, Value: {attribute.Value}");
                // Другие действия с атрибутами
            }
        }
        else
        {
            Debug.Log("No attributes found for this object ");
        }
        nameText.text = dictionaryString;
    }
    void AddCollidersRecursively(Transform parent)              //добавляет дочерним объектам коллайдер, материал и скрипт
    {
        foreach (Transform child in parent)
        {
            if (child.GetComponent<BoxCollider>() == null)
            {
                child.gameObject.tag = "details";
                child.gameObject.AddComponent<BoxCollider>();
                child.gameObject.AddComponent<selectDetails>();
                child.gameObject.AddComponent<MeshRenderer>();
            }
            AddCollidersRecursively(child);
        }
    }
}
 
