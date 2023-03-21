using UnityEngine;
using UnityEngine.XR;

/// <summary>
/// This scripts tracks Hand Controller Velocity and Acceleration
/// </summary>
public class ControllerVelocity : MonoBehaviour
{
    InputDevice LeftControllerDevice;
    InputDevice RightControllerDevice;
    
    [HideInInspector] public Vector3 LeftControllerVelocity;
    [HideInInspector] public Vector3 RightControllerVelocity;

    [HideInInspector] public Vector3 LeftControllerAceleration;
    [HideInInspector] public Vector3 RightControllerAceleration;
    void Start()
    {
        LeftControllerDevice = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        RightControllerDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }

 
    public void GetHandsVelocity()
    {
        LeftControllerDevice.TryGetFeatureValue(CommonUsages.deviceVelocity, out LeftControllerVelocity);
        RightControllerDevice.TryGetFeatureValue(CommonUsages.deviceVelocity, out RightControllerVelocity);
    }
    public void GetHandsAcceleration()
    {
        LeftControllerDevice.TryGetFeatureValue(CommonUsages.deviceAcceleration, out LeftControllerAceleration);
        RightControllerDevice.TryGetFeatureValue(CommonUsages.deviceAcceleration, out RightControllerAceleration);
    }

    public Vector3 GetLeftHandAcceleration()
    {
        LeftControllerDevice.TryGetFeatureValue(CommonUsages.deviceAcceleration, out LeftControllerAceleration);
        return LeftControllerAceleration;
    }
    public Vector3 GetRightHandAcceleration()
    {
        RightControllerDevice.TryGetFeatureValue(CommonUsages.deviceAcceleration, out RightControllerAceleration);
        return RightControllerAceleration;
    }

    public Vector3 GetLeftHandVelocity()
    {
        LeftControllerDevice.TryGetFeatureValue(CommonUsages.deviceVelocity, out LeftControllerVelocity);
        return LeftControllerVelocity;
    }
    public Vector3 GetRightHandVelocity()
    {
        RightControllerDevice.TryGetFeatureValue(CommonUsages.deviceVelocity, out RightControllerVelocity);
        return RightControllerVelocity;
    }


    #region Debug
    //void Update()
    //{
    //    GetHandsVelocity();
    //    GetHandsAcceleration();
    //    if (LeftControllerAceleration.y > 1)
    //    {
    //        Debug.Log("HandsAcceleration: " + LeftControllerAceleration);
    //    }

    //}
    #endregion

}

