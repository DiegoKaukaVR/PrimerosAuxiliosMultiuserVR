using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketController : MonoBehaviour
{
    [SerializeField] XRSocketInteractor socket;
    XRGrabInteractable movil;

    private void Awake()
    {
        movil = GetComponent<XRGrabInteractable>();
        socket.startingSelectedInteractable = movil;
    }
}
