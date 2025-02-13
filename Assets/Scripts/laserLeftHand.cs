using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.Extras;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TalesFromTheRift;

public class laserLeftHand : SteamVR_LaserPointer
{
    public GameObject Keyboard;
    public CanvasKeyboard CK;

    public override void OnPointerIn(PointerEventArgs e)
    {
        base.OnPointerIn(e);
        if(e.target.CompareTag("w"))
        {
            e.target.GetComponent<Image>().color = Color.blue;
        }
    }

    public override void OnPointerClick(PointerEventArgs e)
    {
        base.OnPointerIn(e);
        if(e.target.CompareTag("w"))
        {
            e.target.GetComponent<Button>().onClick.Invoke();
        }
        else if(e.target.CompareTag("i"))
        {
            e.target.GetComponent<InputField>().ActivateInputField();
/*            CK = GameObject.Find("CanvasKeyboard").GetComponent<CanvasKeyboard>();
            CK.inputObject = e.target.GetComponent<InputField>();   */
            Keyboard.SetActive(true);
        }
    }
    public override void OnPointerOut(PointerEventArgs e)
    {
        OnPointerIn(e);
        if (e.target.CompareTag("w"))
        {
            e.target.GetComponent<Image>().color = Color.white;
        }
        if (e.target.CompareTag("i"))
        {
            e.target.GetComponent<Image>().color = Color.white;
        }
    }
}
