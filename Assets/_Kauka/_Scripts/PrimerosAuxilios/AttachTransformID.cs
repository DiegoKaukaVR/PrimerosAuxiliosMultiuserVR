using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachTransformID : AttachToTransform
{
    [Header("Transform ID Config")]
    [Tooltip("ID that is going to assign from RigConfiguration")]
    public int IDTransform;

    protected override void Start()
    {
        base.Start();
        SetTarget();

        transform.parent = attachedTransform;

        if (resetPosition)
        {
            transform.localPosition = Vector3.zero;
        }

    }

    /// <summary>
    /// Set target by ID introduced in Editor
    /// </summary>
    public void SetTarget()
    {
        attachedTransform = RigConfiguration.instance.TransformByID(IDTransform);
    }

}
