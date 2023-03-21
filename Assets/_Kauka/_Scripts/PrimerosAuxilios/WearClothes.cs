using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WearClothes : MonoBehaviour
{
    public UnityEvent OnInputEvent;


    bool inputA;
    private void OnTriggerStay(Collider other)
    {
        if (inputA)
        {
            OnInputEvent.Invoke();
        }
    }
}
