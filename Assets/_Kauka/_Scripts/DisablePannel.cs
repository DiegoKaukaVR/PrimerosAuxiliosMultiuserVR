using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DisablePannel : MonoBehaviour
{
    public UnityEvent onDisbaleEvent;

    public void ClosePannel()
    {
        GetComponent<Animator>().SetBool("open", false);
        onDisbaleEvent.Invoke();
    }

}
