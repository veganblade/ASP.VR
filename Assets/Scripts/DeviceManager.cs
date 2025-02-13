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
        //Проверка подключения плагина/гарнитуры
        if (!XRSettings.isDeviceActive)
        {
            Debug.Log("Плагин не подключен");
        }
        else if (XRSettings.isDeviceActive && (XRSettings.loadedDeviceName == "Mock HMD"
            || XRSettings.loadedDeviceName == "MockHMDDisplay"))
        {
            Debug.Log("Используется Mock HMD девайс");
        }
        else
        {
            Debug.Log("Используется гарнитура " + XRSettings.loadedDeviceName);
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
