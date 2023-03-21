using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class OnTriggerEventHands : OnTriggerEvent
{
    ControllerVelocity controllerVelocity;
    [SerializeField]Transform rightHandTransform;

    

    float acc;
    [SerializeField] float minAcceleration;

    private void Start()
    {
        controllerVelocity = GameManager.instance.xrRig.GetComponent<ControllerVelocity>();
        rightHandTransform = GameManager.instance.rightController.transform;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        acc = GetAccelerationToObjetive();

        if (!CheckAllCondition(other.gameObject))
        {
            return;
        }
        if (acc > minAcceleration)
        {
            Debug.Log("Push Detected, event called");
            onEnterTrigger.Invoke();
        }
    }


    Vector3 rightHandAcc;
    Vector3 finalHandAcc;
    Vector3 dirToObjectiveRight;
    float totalAcceleration;

    float GetAccelerationToObjetive()
    {
        rightHandAcc = controllerVelocity.GetRightHandAcceleration();
        dirToObjectiveRight = transform.position - rightHandTransform.position;
        dirToObjectiveRight.Normalize();

        finalHandAcc = new Vector3(dirToObjectiveRight.x * rightHandAcc.x, dirToObjectiveRight.y * rightHandAcc.y, dirToObjectiveRight.z * rightHandAcc.z);
        totalAcceleration = finalHandAcc.magnitude;
        return totalAcceleration;
    }

}
