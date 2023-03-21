using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartPosition : MonoBehaviour
{
    public int indexModule = 0;
    public Transform[] startedPoint;
    public Transform XROrigin;

    bool play;

    public int DefaultStart = 7;

    private void Start()
    {
        XROrigin = GameManager.instance.xrRig.transform;

        if (GameManager.instance.testing)
        {
            TeleportPlayer(GameManager.instance.moduleIndex);
        }
        else
        {
            TeleportPlayer(DefaultStart);
        }
     
      

        play = true;
    }

    public void TeleportPlayer(int index)
    {
        XROrigin.transform.position = startedPoint[index].position;
        XROrigin.transform.rotation = startedPoint[index].rotation;
    }

    //private void OnValidate()
    //{
    //    if (play || !GetComponent<GameManager>().testing)
    //    {
    //        return;
    //    }
    //    TeleportPlayer(indexModule);
    //}
    private void OnDrawGizmos()
    {
        if (play || !GetComponent<GameManager>().testing)
        {
            return;
        }
        Gizmos.color = Color.red;
        for (int i = 0; i < startedPoint.Length; i++)
        {
            Gizmos.DrawCube(startedPoint[i].position, Vector3.one * 0.2f);
            Gizmos.color = Color.green;
            Gizmos.DrawRay(startedPoint[i].position, startedPoint[i].forward);
        }

        TeleportPlayer(indexModule);
    }
}
