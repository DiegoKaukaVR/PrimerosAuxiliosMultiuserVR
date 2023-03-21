using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportRoomsManager : MonoBehaviour
{
    public TeleportRooms[] teleportRooms;
    public Transform XRTransform;

    private void Start()
    {
       
        //teleportRooms = GetComponentsInChildren<TeleportRooms>();
       
       
    }

    private void OnValidate()
    {
        //if (teleportRooms.Length==0)
        //{
        //    teleportRooms = GetComponentsInChildren<TeleportRooms>();
        //}
    }

    public void TeleportPlayer(int index)
    {
        XRTransform.position = teleportRooms[index].transform.position;
    }

}
