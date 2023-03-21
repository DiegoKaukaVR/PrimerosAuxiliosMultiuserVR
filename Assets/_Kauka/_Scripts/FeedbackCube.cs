using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackCube : MonoBehaviour
{

    public BoxCollider collider;

    public float scaler = 1.5f;
    private void Start()
    {
        
        collider = GetComponentInParent<BoxCollider>();
        scaler = 0.6f;
        SetScale(collider.size);

    }

    private void OnValidate()
    {
        if (collider == null)
        {
            collider = transform.parent?.GetComponent<BoxCollider>();
        }

        if (collider == null)
        {
            return;
        }
        else
        {
            SetScale(collider.size);
        }
       
    }
    public void SetScale(Vector3 scale)
    {
        transform.localScale = scale * scaler;
        transform.rotation.eulerAngles.Set(0, 0, 0);
        transform.localPosition = Vector3.zero;
    }
}
