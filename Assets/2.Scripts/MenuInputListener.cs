using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;
using UnityEngine.Events;

public class MenuInputListener : MonoBehaviour
{
    public XRNode controllerNode;
    public SceneManager sceneManager;
    List<InputDevice> inputDevices;
    bool isPressed = false;

    // Awake will be called even if script is disabled
    private void Awake()
    {
        inputDevices = new List<InputDevice>();
    }

    void Start()
    {
        GetDevice();
    }

    // Update is called once per frame
    void Update()
    {
        GetDevice();
        foreach (InputDevice device in inputDevices)
        {
            if (device.isValid)
            {
                bool inputValue;
                if (device.TryGetFeatureValue(CommonUsages.menuButton, out inputValue) && inputValue)
                {
                    if (!isPressed)
                    {
                        isPressed = true;
                        sceneManager.EnterScene(0);
                        Debug.Log("OnPress event is called");
                    }
                }
                else if (isPressed)
                {
                    isPressed = false;
                    Debug.Log("OnRelease event is called");
                }
            }
        }
    }

    void GetDevice()
    {
        InputDevices.GetDevicesAtXRNode(controllerNode, inputDevices);
    }
}
