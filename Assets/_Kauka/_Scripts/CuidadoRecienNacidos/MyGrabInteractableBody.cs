using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MyGrabInteractableBody : MyGrabInteractable
{

    //1. Cuando son cogidos rotan su transform para que sea exactamente el que mantiene la dirección de la mano normal

    //2. Recalcular rotación de la mano.

    public Transform dummy;
    public Transform bodyTransform;
    public float rotationSpeedAxis = 20f;

    private void Start()
    {
        Invoke("LazyStart", 0.3f);
      
    }

    void LazyStart()
    {
      
    }

    bool attached;

    private void FixedUpdate()
    {

        Vector3 dir = dummy.position - bodyTransform.position;


        if (!attached)
        {
            dummy.rotation = Quaternion.Lerp(dummy.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 3f * rotationSpeedAxis);
        }
        else
        {
            dummy.rotation = Quaternion.Lerp(dummy.rotation, Quaternion.LookRotation(-dir), Time.deltaTime * 3f * rotationSpeedAxis);

        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        attached = true;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        attached = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(transform.position, transform.forward);

        Vector3 dir = transform.position - bodyTransform.position;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, dir);
    }


}
