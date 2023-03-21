using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionHeadSetHeight : ConditionEvent
{
    [Range(0,10)]
    [SerializeField] float minTotalHeight = 2;

    Transform cameraTransform;

    private void Start()
    {
        cameraTransform = GameManager.instance.xrRig.GetComponentInChildren<TrackCameraPosition>().CameraTranform;
    }

    public override bool Condition(GameObject GO)
    {
        Debug.Log("Camera Transform Position Y = " + cameraTransform.position.y);
        if (cameraTransform.position.y < minTotalHeight)
        {
            Debug.Log("La cabeza está por debajo del límite: " + cameraTransform.position.y);
            return true;
        }
        else
        {
            return false;
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0, 0.1f);   
        Gizmos.DrawCube(new Vector3(transform.position.x, transform.position.y + minTotalHeight, transform.position.z), new Vector3(1f, 0.1f, 1f));
    }
}
