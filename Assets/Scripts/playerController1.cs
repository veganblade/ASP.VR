using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class playerController1 : MonoBehaviour
{
    public float normalSpeed = 5f; // Нормальная скорость
    public float boostedSpeed = 11f; // Ускоренная скорость
    public float accelerationRate = 5f; //Скорость ускорения
    public float decelerationRate = 8f; //Скорость замедления
    public float jumpForce = 5f; // Сила прыжка
    public float gravity = -9.81f;
    public SteamVR_Action_Vector2 input;
    public SteamVR_Action_Boolean triggerAction; // Ссылка на экшен левого курка
    public SteamVR_Action_Boolean gripAction; // Ссылка на экшен левого нижнего курка
    public SteamVR_Input_Sources hand = SteamVR_Input_Sources.LeftHand;
    private CharacterController characterController;
    public VectorValue pos;
    private float currentSpeed; // Текущая скорость
    private bool isBoosting;    // Флаг ускорения
    private float targetSpeed; // Скорость к которой стремимся
    private Vector3 velocity; //Скорость падения

    private bool isGrounded; // Флаг для проверки, на земле ли игрок


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
            // Проверяем, нажат ли курок
            isBoosting = triggerAction.GetState(hand);
        }
        else
        {
            return;
        }
        if (gripAction != null)
        {
            // Проверяем, нажат ли grip
            if (gripAction.GetStateDown(hand))
            {
                Jump();
            }
        }
        else
        {
            return;
        }
        //Проверка, на земле ли игрок
        isGrounded = characterController.isGrounded;

        if (isBoosting)
        {
            targetSpeed = boostedSpeed;
        }
        else
        {
            targetSpeed = normalSpeed;
        }

        //Плавное изменение скорости
        currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, Time.deltaTime * (isBoosting ? accelerationRate : decelerationRate));

        // Применяем гравитацию
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


    // Метод для реализации прыжка
    private void Jump()
    {
        if (isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

    }
}