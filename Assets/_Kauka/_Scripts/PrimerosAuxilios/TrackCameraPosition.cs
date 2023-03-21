using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCameraPosition : MonoBehaviour
{
    public Transform CameraTranform;
    [SerializeField] Transform CameraOffSet;
    [SerializeField] Transform XRRigTransform;


    public float distancePlayerToRig;
    public Vector3 dirToPlayer;

  
    public float GetDistancePlayerToRig()
    {
        transform.position = new Vector3(CameraTranform.transform.position.x, XRRigTransform.position.y, CameraTranform.transform.position.z);
        return Vector3.Distance(transform.position, XRRigTransform.position);
    }

    public Vector3 GetDirToPlayerFromRig()
    {
        return transform.position - XRRigTransform.position;
    }

    Vector3 cameraPos;
    public void RegisterCameraPos()
    {
        cameraPos = CameraTranform.localPosition;
    }

    public void SetCameraOffSet()
    {
        CameraOffSet.localPosition = cameraPos;
    }
    public void ResetCameraOffSet()
    {
        CameraOffSet.localPosition = Vector3.zero;
    }

 
   
}
