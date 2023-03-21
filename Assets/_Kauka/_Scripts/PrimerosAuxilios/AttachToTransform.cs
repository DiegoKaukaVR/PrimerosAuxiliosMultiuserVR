using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AttachToTransform : MonoBehaviour
{
    [SerializeField, Tooltip("Nuevo Parent del objeto")] protected Transform attachedTransform;
    [SerializeField, Tooltip("Si quieres resetear la posicion local a 0 o no")] protected bool resetPosition;
    [SerializeField, Tooltip("Si quieres resetear la posicion local a 0 o no")] protected bool setRotation;
    [SerializeField, Tooltip("Si quieres resetear la posicion local a 0 o no")] protected Vector3 offsetRot;

    protected virtual void Start()
    {
        transform.parent = attachedTransform;

        if (resetPosition)
        {
            transform.localPosition = Vector3.zero;
        }
      
    }

    [SerializeField] bool onValidate = true;
    protected virtual void OnValidate()
    {
        if (!onValidate)
        {
            return;
        }
        if (attachedTransform == null)
        {
            Debug.LogWarning("Introduce Attached Transform");
            return;
        }
        transform.position = attachedTransform.position;
    }
   
   
    protected virtual void OnEnable()
    {
        AttachToTransformFuntion();
    }

    public virtual void AttachToTransformFuntion()
    {
        transform.parent = attachedTransform;

        if (resetPosition)
        {
            transform.localPosition = Vector3.zero;
        }
        if (setRotation)
        {
            SetRotation();
        }
    }

    protected virtual void SetRotation()
    {
        transform.localRotation = Quaternion.identity;
    }

    
    
}
