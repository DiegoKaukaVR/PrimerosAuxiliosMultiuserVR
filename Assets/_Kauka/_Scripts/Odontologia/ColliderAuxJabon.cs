using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderAuxJabon : MonoBehaviour
{
    public Collider colliderGrifo;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hand")
        {
            colliderGrifo.isTrigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Hand")
        {
            colliderGrifo.isTrigger = false;
        }
    }

}
