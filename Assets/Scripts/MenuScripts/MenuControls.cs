using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;

public class MenuControls : MonoBehaviour
{
    public Panel exitPanel;
    public AudioMixer am;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayPressed()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void ExitMenu()
    {
        
    }
    public void ExitPressed()
    {
        Debug.Log("Exit pressed!");
    }
    public void AudioVolume(float sliderValue)
    {
        am.SetFloat("masterVolume", sliderValue);
    }
    public void Quality(int q)
    {
        QualitySettings.SetQualityLevel(q);
    }
    public void ReplaceScene()
    {
        SceneManager.LoadScene("Level1");
    }


}
