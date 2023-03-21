using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonMachine : MonoBehaviour
{
    public UnityEvent[] EventArray;

    public void PlayBehavior(int i)
    {
        EventArray[i].Invoke();
    }
}
