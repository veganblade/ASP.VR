using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private UI_Controller UIScript;
    public CharacterController controller;
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    public float gravity = -9.81f;
    public float speed = 12f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public float jumpHeight = 3f;
    public LayerMask groundMask;
    bool isGrounded;
    Vector3 velocity;
    float xRotation = 0;
    void Start()
    {
        UIScript = GameObject.Find("CanvasUI").GetComponent<UI_Controller>();
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.JoystickButton2))           //открытие меню
        {
            UIScript.OpenCreateModelUI();
        }
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        xRotation -= mouseY;
        
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
        if(Input.GetKeyDown(KeyCode.X))
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
        if(Input.GetKeyDown(KeyCode.Z))
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }

    }
}
