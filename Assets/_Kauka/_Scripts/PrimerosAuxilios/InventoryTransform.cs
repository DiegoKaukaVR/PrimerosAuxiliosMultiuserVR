using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTransform : MonoBehaviour
{
    public Transform CameraTransform;


    [Tooltip("Posicion relativa al transform de la cámara, para poner el bolsillo posicion abajo a los lados")]
    [SerializeField] Vector3 offSet;


    private void Awake()
    {
        CameraTransform = transform.parent.transform;
    }

    private void FixedUpdate()
    {
        transform.position = CameraTransform.position + offSet;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);


        if (CameraTransform == null)
        {
            CameraTransform = transform.parent.transform;
        }
        Gizmos.DrawCube(CameraTransform.position + offSet, Vector3.one * 0.2f);
    }
}
