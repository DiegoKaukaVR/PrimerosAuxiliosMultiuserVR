using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    public Transform target;
    public void TeleportPlayerTarget()
    {
        GameManager.instance.xrRig.transform.position = target.position;
        GameManager.instance.xrRig.transform.eulerAngles = target.eulerAngles;
        GameManager.instance.xrRig.transform.rotation = target.rotation;
        GameManager.instance.offset.rotation = target.rotation;
    }

    private void OnDrawGizmos()
    {
        if (target == null)
        {
            return;
        }
        Gizmos.DrawCube(target.position, Vector3.one * 0.1f);
        Gizmos.DrawRay(target.position, target.forward);
    }


}
