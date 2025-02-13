using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class cordinates : MonoBehaviour
{
    public float axisLength = 5f; // ����� ����
    public GameObject xAxis; // ������ ������ ����� ��� X
    public GameObject yAxis; // ������ ������ ����� ��� Y
    public GameObject zAxis; // ������ ������ ����� ��� Z
    public GameObject firstObj;
    public Transform secondObj;
    private void Start()
    {

    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * axisLength); // ��� X

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up * axisLength); // ��� Y

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.forward * axisLength); // ��� Z
    }
    public void CordEye()
    {
        secondObj = firstObj.transform.GetChild(0);
        // ������� ������ ����� ���� � ��������� �� � ������������ � ������������� �����
        Instantiate(xAxis, secondObj.transform.position, Quaternion.identity);
        Instantiate(yAxis, secondObj.transform.position, Quaternion.Euler(0, 0, 90));
        Instantiate(zAxis, secondObj.transform.position, Quaternion.Euler(90, 0, 0));
    }
}