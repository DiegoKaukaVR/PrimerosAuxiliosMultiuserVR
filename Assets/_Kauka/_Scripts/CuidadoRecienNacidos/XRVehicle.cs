using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class XRVehicle : MonoBehaviour
{
    public float speed = 1f;
    public float acceleration = 1f;
    public float angularSpeed = 1f;
    public Vector3 dirVelocity;

    public Vector2 inputVector;

    Rigidbody rb;

    XRController rightController;
    XRController leftController;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rightController = GameManager.instance.rightController;
        leftController = GameManager.instance.leftController;
    }

    private void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
       Movement(inputVector);
    }

    void GetInput()
    {
        rightController.inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputVector);
        Debug.Log("X Value: " + inputVector.x);
        Debug.Log("Y Value: " + inputVector.y);
    }

    void Movement(Vector2 InputVector)
    {
        /// Forward
        rb.AddForce(InputVector.y * Vector3.forward, ForceMode.Acceleration);
    }




}
