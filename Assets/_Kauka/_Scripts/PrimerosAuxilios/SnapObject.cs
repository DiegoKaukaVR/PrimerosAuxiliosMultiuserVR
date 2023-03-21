using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SnapObject : MonoBehaviour
{
    [SerializeField] Transform snapPos;

    [SerializeField] GameObject snapObject;

    public UnityEvent OnSnap;

    public Vector3 offSet;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    
    public void SnapObjectToTarget()
    {
        if (snapObject == null)
        {
            transform.position = snapPos.position;
            transform.rotation = snapPos.rotation;
        }
        else
        {
            snapObject.transform.position = snapPos.position;
            snapObject.transform.rotation = snapPos.rotation;
        }


        OnSnap.Invoke();
    }

    public void RigidbodyKinnematic()
    {
        rb.isKinematic = true;
    }

    public void BackRigidbodyKinematic()
    {
        rb.isKinematic = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1,0, 0, 0.4f);

        if (snapObject == null)
        {
            return;
        }
        if (snapPos == null)
        {
            Debug.LogError("Tienes que meter la posición de snapeo.");
            return;
        }

        Gizmos.DrawCube(snapPos.position, Vector3.one * 0.1f);

    
        Gizmos.color = new Color(1, 0, 0, 1f);
        Gizmos.DrawLine(snapObject.transform.position, snapPos.position);
    }
}
