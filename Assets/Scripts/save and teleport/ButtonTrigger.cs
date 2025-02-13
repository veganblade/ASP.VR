using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ButtonTrigger : MonoBehaviour
{
    public GameObject levelTransitionUI;
    public bool playerInTrigger = false; // Флаг, указывающий, находится ли игрок в триггере
    public levelChanger levelChanger;
    public DoorConfig doorConfig;
/*    private VectorValue pos;*/
    private GameObject player;
    public SteamVR_Action_Boolean xButtonAction; // Ссылка на SteamVR Action для кнопки X
    public SteamVR_Input_Sources hand = SteamVR_Input_Sources.RightHand; // Рука, с которой мы будем считывать нажатие кнопки
    public void Start()
    {
        /*        player = GameObject.Find("PlayerTest");*/
        if (xButtonAction == null)
        {
            Debug.LogError("You must set reference to the X button action on: " + gameObject.name);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            levelTransitionUI.SetActive(true);
            playerInTrigger = true;
            Debug.Log("OnTriggerEnter: " + gameObject.name + ", Player entered");
        }
    }
    public void OnTriggerExit(Collider other)
    {
        levelTransitionUI.SetActive(false);
        playerInTrigger = false;
        Debug.Log("OnTriggerExit: " + gameObject.name + ", Player entered");
    }
    void Update()
    {
        if (playerInTrigger && xButtonAction != null && xButtonAction.GetStateDown(hand))
        {
            //Ищем индекс текущего конфига в массиве
            int index = System.Array.IndexOf(levelChanger.doorConfigs, doorConfig);
            Debug.Log($"Дверь: {gameObject.name}, Индекс: {index}"); // Отладочный вывод
            levelChanger.FadeToLevel(index); // Передаем индекс двери в FadeToLevel
/*            player.transform.position = pos.initialValue;*/
        }
    }
}
