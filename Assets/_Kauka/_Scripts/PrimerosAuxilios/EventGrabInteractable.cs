using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class EventGrabInteractable : MyGrabInteractable
{
    public UnityEvent onEnterInteractionEvent;
    private void Start()
    {
        if (onEnterInteractionEvent == null)
            onEnterInteractionEvent = new UnityEvent();

            onEnterInteractionEvent.AddListener(OnSelectEnter);
    }

    void OnSelectEnter()
    {
        Debug.Log("OnSelectEnter Grab Interactable");
    }

    //protected override void OnSelectEntered(SelectEnterEventArgs args)
    //{
    //    base.OnSelectEntered(args);
    //    onEnterInteractionEvent.Invoke();
    //}
}
