using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CreateModel : MonoBehaviour
{ 
    [SerializeField] private string modelPath;
    [SerializeField] private string modelName;
    [SerializeField] private string destinationPath = "Assets/StreamingAssets/NewUserModel1.fbx";       //стандартное имя сохраненной модели
    [SerializeField] private UI_Controller UIScript;                                                    
    [SerializeField] private bool IsSaved = false;                                                      //сохранение модели
    public GameObject newObject;                                                                        //родительский объект
    [SerializeField] private Transform firstObject;                                                     //координаты родительского объекта
    public AssetBundle myAssetBundle;                                                                   //штука для сохранения модели (НЕ работает)

    void Start()
    {
        UIScript = GameObject.Find("CanvasUI").GetComponent<UI_Controller>();
    }
    private void Update()
    {

    }
    public void SaveModelToFolder()
    {
        modelName = UIScript.pathUI;
        /*FileUtil.CopyFileOrDirectory(modelName, destinationPath);*/
        IsSaved = true;
    }
    public void OpenModelFromFolder()
    {
        myAssetBundle = AssetBundle.LoadFromFile(modelPath);
        newObject = new GameObject("myobject");
        //отображение модели на сцене
        GameObject obj = myAssetBundle.LoadAsset<GameObject>("NewUserModel1");
        if (myAssetBundle != null)
        {
         if (IsSaved == true)
                {
                    // Использование загруженных ресурсов
                    Instantiate(obj, Vector3.zero, Quaternion.identity); // Создание экземпляра модели в сцене
                    newObject.transform.SetParent(firstObject);
                    MeshFilter meshFilter = obj.AddComponent<MeshFilter>();
        /*          meshFilter.sharedMesh = obj.GetComponent<MeshFilter>().sharedMesh;*/
                    MeshRenderer meshRenderer = obj.AddComponent<MeshRenderer>();
                    BoxCollider boxCollider = obj.AddComponent<BoxCollider>();
        /*          meshRenderer.sharedMaterial = obj.GetComponent<MeshRenderer>().sharedMaterial;*/
                }
                else
                {
                    Debug.LogError("Произошла ошибка при загрузки файлов из: " + destinationPath);
                }
                UIScript.PanelUI.SetActive(false);
            }
        }
}

