using System.Collections.Generic;
using UnityEngine;

public class Haptic : MonoBehaviour
{
    public enum Contol {left, right}
    List<UnityEngine.XR.InputDevice> leftHandedControllers = new List<UnityEngine.XR.InputDevice>();
    List<UnityEngine.XR.InputDevice> rightHandedControllers = new List<UnityEngine.XR.InputDevice>();

    public bool left = false;
    public bool right = false;

    void Update()
    {
        if (leftHandedControllers.Count <= 0){
            var desiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Left | UnityEngine.XR.InputDeviceCharacteristics.Controller;
            UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, leftHandedControllers);
            HapticImpulse(Contol.left, 0.5f, 1f);
        }

        if (rightHandedControllers.Count <= 0)
        {
            var rdesiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Right | UnityEngine.XR.InputDeviceCharacteristics.Controller;
            UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(rdesiredCharacteristics, rightHandedControllers);
            HapticImpulse(Contol.right, 0.5f, 1f);
        }

        if (left){
            left = false;
            HapticImpulse(Contol.left, 0.5f, 1f);
        }

        if (right){
            right = false;
            HapticImpulse(Contol.right, 0.5f, 1f);
        }
    }
    
    public void ActivarImpulse(){
        HapticImpulse(Haptic.Contol.right, 2f, 0.5f);
        HapticImpulse(Haptic.Contol.left, 2f, 0.5f);
    }

    public void HapticImpulse (Haptic.Contol control, float amplitude, float duration) 
    {
        switch(control){
            case Contol.left: 
                foreach (UnityEngine.XR.InputDevice device in leftHandedControllers)
                {
                    UnityEngine.XR.HapticCapabilities capabilities;
                    if (device.TryGetHapticCapabilities(out capabilities))
                    {
                        if (capabilities.supportsImpulse)
                        {
                            uint channel = 0;
                            device.SendHapticImpulse(channel, amplitude, duration);
                        }
                    }
                }
                break;
            case Contol.right:
                foreach (UnityEngine.XR.InputDevice device in rightHandedControllers)
                {
                    UnityEngine.XR.HapticCapabilities capabilities;
                    if (device.TryGetHapticCapabilities(out capabilities))
                    {
                        if (capabilities.supportsImpulse)
                        {
                            uint channel = 0;
                            device.SendHapticImpulse(channel, amplitude, duration);
                        }
                    }
                }
                break;
            default:
                Debug.LogError("Incorrect device");
                break;
        }
    }
}
