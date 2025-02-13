using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SFB;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class UI_Controller : MonoBehaviour
{
    public string pathUI;               //Путь к модели в ассетах
    public GameObject PanelUI;          
    public GameObject PanelTools;
    public Transform target;            //3D модель     
    public CreateModel CMScript;
    public GameObject coobik;
    [SerializeField] private TMP_InputField PosX;
    [SerializeField] private TMP_InputField PosY;
    [SerializeField] private TMP_InputField PosZ;
    [SerializeField] private TMP_InputField RotX;
    [SerializeField] private TMP_InputField RotY;
    [SerializeField] private TMP_InputField RotZ;

    [SerializeField] private TextMeshProUGUI ErrorsCount;
    [SerializeField] private int erC;
    [SerializeField] private int ntC;
    [SerializeField] private TextMeshProUGUI NotesCount;
    public List<string> ErrorsList;
    public List<string> NotesList;
    public bool NoteOr = false;
    [SerializeField] private TMP_InputField Message;
    [SerializeField] private Button ErrorBtn;
    [SerializeField] private Button NoteBtn;

    [SerializeField] private TMP_InputField NameInput;
    [SerializeField] private Button RenameB;
    [SerializeField] private Toggle ActiveModelT;
    public bool isActive = true;
    [SerializeField] private Button ShowAttributes;
    public TouchScreenKeyboard keyboard;

    void Start()
    {
        CMScript= GameObject.Find("CanvasUI").GetComponent<CreateModel>();
        PosX.text = target.transform.position.x.ToString(); 
        PosY.text = target.transform.position.y.ToString(); 
        PosZ.text = target.transform.position.z.ToString(); 
        RotX.text = target.transform.rotation.x.ToString(); 
        RotY.text = target.transform.rotation.y.ToString(); 
        RotZ.text = target.transform.rotation.z.ToString();
        ErrorsList = new List<string>();
        NotesList = new List<string>();
        erC = 0;
        ntC = 0;
        ErrorBtn.onClick.AddListener(OnErrorBtnClick);
        NoteBtn.onClick.AddListener(OnNoteBtnClick);
    }
    void Update()
    {
/*      PosX.text = target.transform.position.x.ToString();         //визуальное отображение координат
        PosY.text = target.transform.position.y.ToString();
        PosZ.text = target.transform.position.z.ToString();
        RotX.text = target.transform.rotation.x.ToString();
        RotY.text = target.transform.rotation.y.ToString();
        RotZ.text = target.transform.rotation.z.ToString();*/
        if(Input.GetKeyDown(KeyCode.E))
        {

        }
    }
    public void OpenFile()
    {
        var extensions = new[] {  //какие файлы вообще можно открыть
            new ExtensionFilter("dae", "stp", "max", "fbx", "obj", "x3d", "vrml", "3ds", "3mf", "stl", "ifc" ),         //форматы для загрузки
            new ExtensionFilter("All Files", "*" ),
        };
        foreach (string path in StandaloneFileBrowser.OpenFilePanel("Open File", "", extensions, true))
        { //открытие формы для загрузки файла
            Debug.Log(path);
            pathUI = path;
        }
    }
    public void OpenCreateModelUI()
    {
        PanelUI.SetActive(true);
    }
    public void CloseDet()
    {
        PanelTools.SetActive(false);
    }
    public void ChangePosition()
    {
        float xInput = float.Parse(PosX.text);
        float yInput = float.Parse(PosY.text);
        float zInput = float.Parse(PosZ.text);

        Vector3 moveDirection = new Vector3(xInput, yInput, zInput);
        coobik.transform.position = moveDirection;
    }
    public void ChangeRotation()
    {
        float xRotInput = float.Parse(RotX.text);
        float yRotInput = float.Parse(RotY.text);
        float zRotInput = float.Parse(RotZ.text);
        Vector3 rotateDirection = new Vector3(xRotInput, yRotInput, zRotInput);
        coobik.transform.rotation = Quaternion.Euler(rotateDirection);
    }
    public void SendMessage()
    {
        //стринговая переменная в качестве параметра и все манипуляции с ней отсюда было бы хорошо
        if((NoteOr == true) && ErrorsCount.text != null)
        {
            string MessageParse = Message.text;
            NotesList.Add(MessageParse);
            ntC += 1;
            NotesCount.text = ntC.ToString();
        }
        else if((NoteOr == false) && ErrorsCount.text != null)
        {
            string ErrorMessageParse = Message.text;
            ErrorsList.Add(ErrorMessageParse);
            erC += 1;
            ErrorsCount.text = erC.ToString();
        }
    }
    public void OnNoteBtnClick()
    {
        ColorBlock colors = NoteBtn.colors;
        colors.normalColor = new Color(0f, 0f, 0f, 0.5f); // Установка прозрачности кнопки A
        colors.highlightedColor = new Color(0f, 0f, 0f, 0.5f); // Установка прозрачности кнопки A при наведении
        NoteBtn.colors = colors;

        colors = ErrorBtn.colors;
        colors.normalColor = new Color(1f, 1f, 1f, 1f); // Установка непрозрачности кнопки B
        colors.highlightedColor = new Color(1f, 1f, 1f, 1f); // Установка непрозрачности кнопки B при наведении
        ErrorBtn.colors = colors;
        NoteOr = true;
    }
    public void OnErrorBtnClick()
    {
        ColorBlock colors = NoteBtn.colors;
        colors.normalColor = new Color(1f, 1f, 1f, 1f); // Установка непрозрачности кнопки A
        colors.highlightedColor = new Color(1f, 1f, 1f, 1f); // Установка непрозрачности кнопки A при наведении
        NoteBtn.colors = colors;

        colors = ErrorBtn.colors;
        colors.normalColor = new Color(0f, 0f, 0f, 0.5f); // Установка прозрачности кнопки B
        colors.highlightedColor = new Color(0f, 0f, 0f, 0.5f); // Установка прозрачности кнопки B при наведении
        ErrorBtn.colors = colors;
        NoteOr = false;
    }
    public void OnToggleChanged()
    {
        //Получаем объекты, которые нужно переключать
        GameObject[] objectsToToggle = GameObject.FindGameObjectsWithTag("details");
        //Переключаем SetActive для каждого объекта
        foreach (GameObject obj in objectsToToggle)
        {
            obj.SetActive(isActive);
        }
    }
    public void getName()
    {
        NameInput.text = gameObject.name;
    }
    public void setName()
    {
        gameObject.name = NameInput.text;
    }
}
