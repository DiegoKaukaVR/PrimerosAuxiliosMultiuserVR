using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderRecibirMaterial : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 8)
        {
            GameControllerEsterilizacion.Instance.materialRecibido = true;
        }
    }
}
