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
    //Берем все объекты из нашей модели
    //Прогоняем их через условие размеров платформы
    //Привязываем к оставшимся объектамм скрипт и платформу и компонент
    //Проверяем
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
                    Debug.Log("Этот объект больше 5 в длину");          //Вывод сообщения в консоль для проверки срабатывания условия 
                    obj.AddComponent<AddBoxColliderToChildren>();          //Привязка скрипта для того чтобы сделать
                    /*obj.AddComponent<Rigidbody>();*/
/*               }*/
            }
        }
    }
}
