using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportObject : MonoBehaviour
{
    [SerializeField] GameObject Object;
    [SerializeField] Transform targetPos;

    public void ChangeObjectPosition()
    {
        if (Object == null)
        {
            if (targetPos != null)
            {
                transform.position = targetPos.position;
            }
            else
            {
                Debug.LogError("Target Pos is not assigned");
            }
        }
        else
        {
            if (targetPos != null)
            {
                Object.transform.position = targetPos.position;
            }
            else
            {
                Debug.LogError("Target Pos is not assigned");
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(targetPos.position, Vector3.one * 0.1f);
    }

}
