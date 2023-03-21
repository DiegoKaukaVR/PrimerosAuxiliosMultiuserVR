using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AwakeEvent : MonoBehaviour
{

    public UnityEvent InitializeEvent;
    void Awake()
    {
        InitializeEvent.Invoke();
    }


}
