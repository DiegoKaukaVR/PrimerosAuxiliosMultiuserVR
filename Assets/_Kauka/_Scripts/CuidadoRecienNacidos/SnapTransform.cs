using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapTransform : MonoBehaviour
{
    [SerializeField] Transform targetObject;
    [SerializeField] bool resetPos;
    [SerializeField] bool resetRot;
    private void Start()
    {
        targetObject.position = transform.position;
        targetObject.parent = transform;

        if (resetPos)
        {
            targetObject.localPosition = Vector3.zero;
        }

        if (resetRot)
        {
            targetObject.localRotation = Quaternion.Euler(Vector3.zero);
        }
       
    }
}

