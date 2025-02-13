using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class levelChanger : MonoBehaviour
{
    private Animator anim;
    public VectorValue playerStorage;
    public Slider sliderLoading;
    public GameObject loadingScreen;
    public AnimationClip animSlider;
    public DoorConfig[] doorConfigs;
    public bool playerInTrigger = false; // ����, �����������, ��������� �� ����� � ��������
    public ButtonTrigger BTScript;


    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void FadeToLevel(int doorIndex)
    {
        anim.SetTrigger("fade");

        if (doorIndex >= 0 && doorIndex < doorConfigs.Length)
        {
            Debug.Log(doorIndex);
            // ������������� ������� ������ �� ������ ������� �����
            playerStorage.initialValue = doorConfigs[doorIndex].spawnPosition;
            StartCoroutine(LoadingScreenOnFade(doorConfigs[doorIndex].levelToLoad));
        }
    }
    private void Update()
    {
        // ������ �� ����� ������� ������ �����, ��� ��� � ButtonTrigger
    }

    IEnumerator LoadingScreenOnFade(int levelToLoad)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelToLoad);
        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            anim.Play("New AnimationSlider");
            float progress = Mathf.Clamp01(operation.progress / 0.9f); // �������� 0.1 �� 0.9
            sliderLoading.value = progress;
            yield return null;
        }
    }
/*    public void OnFadeComplete()
    {

    }*/
}
