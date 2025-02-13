using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ButtonTrigger : MonoBehaviour
{
    public GameObject levelTransitionUI;
    public bool playerInTrigger = false; // ����, �����������, ��������� �� ����� � ��������
    public levelChanger levelChanger;
    public DoorConfig doorConfig;
/*    private VectorValue pos;*/
    private GameObject player;
    public SteamVR_Action_Boolean xButtonAction; // ������ �� SteamVR Action ��� ������ X
    public SteamVR_Input_Sources hand = SteamVR_Input_Sources.RightHand; // ����, � ������� �� ����� ��������� ������� ������
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
            //���� ������ �������� ������� � �������
            int index = System.Array.IndexOf(levelChanger.doorConfigs, doorConfig);
            Debug.Log($"�����: {gameObject.name}, ������: {index}"); // ���������� �����
            levelChanger.FadeToLevel(index); // �������� ������ ����� � FadeToLevel
/*            player.transform.position = pos.initialValue;*/
        }
    }
}
