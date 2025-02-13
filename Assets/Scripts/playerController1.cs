using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class playerController1 : MonoBehaviour
{
    public float normalSpeed = 5f; // ���������� ��������
    public float boostedSpeed = 11f; // ���������� ��������
    public float accelerationRate = 5f; //�������� ���������
    public float decelerationRate = 8f; //�������� ����������
    public float jumpForce = 5f; // ���� ������
    public float gravity = -9.81f;
    public SteamVR_Action_Vector2 input;
    public SteamVR_Action_Boolean triggerAction; // ������ �� ����� ������ �����
    public SteamVR_Action_Boolean gripAction; // ������ �� ����� ������ ������� �����
    public SteamVR_Input_Sources hand = SteamVR_Input_Sources.LeftHand;
    private CharacterController characterController;
    public VectorValue pos;
    private float currentSpeed; // ������� ��������
    private bool isBoosting;    // ���� ���������
    private float targetSpeed; // �������� � ������� ���������
    private Vector3 velocity; //�������� �������

    private bool isGrounded; // ���� ��� ��������, �� ����� �� �����


    public void Start()
    {
        transform.position = pos.initialValue;
        characterController = GetComponent<CharacterController>();
        currentSpeed = normalSpeed;
        targetSpeed = normalSpeed;

        if (triggerAction == null)
        {
            Debug.LogError("You must set reference to the trigger action on: " + gameObject.name);
        }
        if (gripAction == null)
        {
            Debug.LogError("You must set reference to the grip action on: " + gameObject.name);
        }
    }

    private void Update()
    {
        if (triggerAction != null)
        {
            // ���������, ����� �� �����
            isBoosting = triggerAction.GetState(hand);
        }
        else
        {
            return;
        }
        if (gripAction != null)
        {
            // ���������, ����� �� grip
            if (gripAction.GetStateDown(hand))
            {
                Jump();
            }
        }
        else
        {
            return;
        }
        //��������, �� ����� �� �����
        isGrounded = characterController.isGrounded;

        if (isBoosting)
        {
            targetSpeed = boostedSpeed;
        }
        else
        {
            targetSpeed = normalSpeed;
        }

        //������� ��������� ��������
        currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, Time.deltaTime * (isBoosting ? accelerationRate : decelerationRate));

        // ��������� ����������
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }


        Vector3 direction = Player.instance.hmdTransform.TransformDirection(new Vector3(input.axis.x, 0, input.axis.y));

        characterController.Move(currentSpeed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up) + velocity * Time.deltaTime);
    }


    // ����� ��� ���������� ������
    private void Jump()
    {
        if (isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

    }
}