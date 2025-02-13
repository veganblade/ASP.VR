using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class cordinates : MonoBehaviour
{
    public float axisLength = 5f; // Длина осей
    public GameObject xAxis; // Префаб модели линии оси X
    public GameObject yAxis; // Префаб модели линии оси Y
    public GameObject zAxis; // Префаб модели линии оси Z
    public GameObject firstObj;
    public Transform secondObj;
    private void Start()
    {

    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * axisLength); // Ось X

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up * axisLength); // Ось Y

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.forward * axisLength); // Ось Z
    }
    public void CordEye()
    {
        secondObj = firstObj.transform.GetChild(0);
        // Создаем модели линий осей и размещаем их в соответствии с координатными осями
        Instantiate(xAxis, secondObj.transform.position, Quaternion.identity);
        Instantiate(yAxis, secondObj.transform.position, Quaternion.Euler(0, 0, 90));
        Instantiate(zAxis, secondObj.transform.position, Quaternion.Euler(90, 0, 0));
    }
}