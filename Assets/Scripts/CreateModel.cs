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
    [SerializeField] private string destinationPath = "Assets/StreamingAssets/NewUserModel1.fbx";       //����������� ��� ����������� ������
    [SerializeField] private UI_Controller UIScript;                                                    
    [SerializeField] private bool IsSaved = false;                                                      //���������� ������
    public GameObject newObject;                                                                        //������������ ������
    [SerializeField] private Transform firstObject;                                                     //���������� ������������� �������
    public AssetBundle myAssetBundle;                                                                   //����� ��� ���������� ������ (�� ��������)

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
        //����������� ������ �� �����
        GameObject obj = myAssetBundle.LoadAsset<GameObject>("NewUserModel1");
        if (myAssetBundle != null)
        {
         if (IsSaved == true)
                {
                    // ������������� ����������� ��������
                    Instantiate(obj, Vector3.zero, Quaternion.identity); // �������� ���������� ������ � �����
                    newObject.transform.SetParent(firstObject);
                    MeshFilter meshFilter = obj.AddComponent<MeshFilter>();
        /*          meshFilter.sharedMesh = obj.GetComponent<MeshFilter>().sharedMesh;*/
                    MeshRenderer meshRenderer = obj.AddComponent<MeshRenderer>();
                    BoxCollider boxCollider = obj.AddComponent<BoxCollider>();
        /*          meshRenderer.sharedMaterial = obj.GetComponent<MeshRenderer>().sharedMaterial;*/
                }
                else
                {
                    Debug.LogError("��������� ������ ��� �������� ������ ��: " + destinationPath);
                }
                UIScript.PanelUI.SetActive(false);
            }
        }
}

