using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Esto se usa porque en recien nacidos necesitamos hacer attach a un bebe dependiendo del caso clínico
/// </summary>
public class AttachTransformMultipliesBodies : MonoBehaviour
{
    [SerializeField, Tooltip("Nuevo Parent del objeto")] 
    protected Transform[] attachedTransforms = new Transform[3];

    [SerializeField, Tooltip("Si quieres resetear la posicion local a 0 o no")] protected bool resetPosition;

    protected void Start()
    {
        Invoke("LateStart", 0.25f);
    }

    protected virtual void LateStart()
    {
        for (int i = 0; i < attachedTransforms.Length; i++)
        {
            if (attachedTransforms[i].gameObject.activeInHierarchy)
            {
              AttachToTransform(i);
            }
        }
       
    }
  

    private void AttachToTransform(int index)
    {
        transform.parent = attachedTransforms[index];

        if (resetPosition)
        {
            transform.localPosition = Vector3.zero;
        }
    }
}
