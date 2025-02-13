using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class DeviceManager : MonoBehaviour
{
    [SerializeField] private InputDevice LeftController;
    [SerializeField] private InputDevice RightController;


    public void Start()
    {
        //�������� ����������� �������/���������
        if (!XRSettings.isDeviceActive)
        {
            Debug.Log("������ �� ���������");
        }
        else if (XRSettings.isDeviceActive && (XRSettings.loadedDeviceName == "Mock HMD"
            || XRSettings.loadedDeviceName == "MockHMDDisplay"))
        {
            Debug.Log("������������ Mock HMD ������");
        }
        else
        {
            Debug.Log("������������ ��������� " + XRSettings.loadedDeviceName);
        }
    }
    private void Awake()
    {
        var listDevices = new List<InputDevice>();
        InputDevices.GetDevices(listDevices);

        foreach (var inputDevice in listDevices)
        {
            InitController(inputDevice);
        }

        InputDevices.deviceConnected += InitController;
    }

    private void OnDestroy()
    {
        InputDevices.deviceConnected -= InitController;
    }

    private void InitController(InputDevice inputDevice)
    {
        if (!inputDevice.characteristics.HasFlag(InputDeviceCharacteristics.Controller))
        {
            return;
        }

        if (inputDevice.characteristics.HasFlag(InputDeviceCharacteristics.Left))
        {
            LeftController = inputDevice;
        }
        else
        {
            RightController = inputDevice;
        }
    }
}
