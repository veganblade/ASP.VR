using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerForTeleport : MonoBehaviour
{
    public VectorValue pos;

    public CharacterController controller;
    public Transform playerCamera;
    public float speed = 5f;
    public float sprintSpeed = 10f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;
    public float lookSpeed = 2f;

    private float ySpeed;
    private float rotationX = 0f;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = pos.initialValue;
    }
    // Update is called once per frame
    void Update()
    {
        // �������� ������
        rotationX -= Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        playerCamera.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        // �������� ������ �� ��� Y
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        // ��������� �������� ��������
        float moveSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : speed;
        // ������
        if (controller.isGrounded)
        {
            ySpeed = -0.5f; // ��������� �������� ��� �������������� "�������"
            if (Input.GetButtonDown("Jump"))
            {
                ySpeed = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }
        ySpeed += gravity * Time.deltaTime;
        // ���������� ��������
        controller.Move((move * moveSpeed + Vector3.up * ySpeed) * Time.deltaTime);
        // �������� ������ �� ��� Y � ����������� �� �������� ����
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        transform.Rotate(Vector3.up * mouseX);
    }

}
