using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderPosicionMovilidad : MonoBehaviour
{
    public Collider colliderObjetivo;
    

    private void Start()
    {
        colliderObjetivo.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hand")
        {
            colliderObjetivo.enabled = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Hand")
        {
            colliderObjetivo.enabled = false;
        }
    }
}
