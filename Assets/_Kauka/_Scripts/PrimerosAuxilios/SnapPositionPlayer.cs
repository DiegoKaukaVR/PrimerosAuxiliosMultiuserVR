using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SpatialTracking;
using UnityEngine.Events;


public class SnapPositionPlayer : MonoBehaviour
{
    
    TrackedPoseDriver trackedPoseDriver;
    [SerializeField] Transform initialPlayerPos;
    [SerializeField] Transform finalPlayerPos;
    [SerializeField] Vector3 offSetSnapPos;
    Transform XRRigtransform;
    TrackCameraPosition trackCamera;

    public float timeLerp = 1f;

    [SerializeField] bool toDestination;
    [SerializeField] bool freezeTrackPose = true;

    public UnityEvent OnSnapEvent;
    public UnityEvent OnComplete;
    public bool RotLerp = true;
    public bool activated = true;
    bool snaped;

    private void Awake()
    {
        XRRigtransform = GameManager.instance.xrRig.transform;
        trackCamera = XRRigtransform.GetComponentInChildren<TrackCameraPosition>();
        trackedPoseDriver = XRRigtransform.GetComponentInChildren<TrackedPoseDriver>();

     
    }
    public void SnapPlayerPos()
    {
        if (snaped)
        {
            return;
        }
       
        OnSnapEvent.Invoke();
        snaped = true;

        if (!activated)
        {
            return;
        }
        // lerp Position
        if (!toDestination)
        {
            StartCoroutine(LerpPosition(initialPlayerPos.position + offSetSnapPos, timeLerp));
        }
        else
        {
            StartCoroutine(LerpPosition(finalPlayerPos.position, timeLerp));
        }

        //Debug.DrawLine(XRRigtransform.position, initialPlayerPos.position + offSetSnapPos, Color.red, 10f);

        if (RotLerp)
        {
            StartCoroutine(LerpRotation(finalPlayerPos.rotation, timeLerp));
        }
       
    }
    IEnumerator LerpPosition(Vector3 targetPos, float duration)
    {
        float time = 0;
        Vector3 startPosition = XRRigtransform.position;

        while (time < duration)
        {
            XRRigtransform.position = Vector3.Lerp(startPosition, targetPos - trackCamera.GetDirToPlayerFromRig().normalized * trackCamera.GetDistancePlayerToRig(), time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        XRRigtransform.position = targetPos - trackCamera.GetDirToPlayerFromRig().normalized * trackCamera.GetDistancePlayerToRig();

        if (freezeTrackPose)
        {
            TrackPose(false);
        }

        OnComplete.Invoke();

        this.gameObject.SetActive(false);



    }

    IEnumerator LerpRotation(Quaternion targetRotation, float duration)
    {
        float timeRot = 0;
        Quaternion startRot = XRRigtransform.rotation;

        while (timeRot < duration)
        {
            XRRigtransform.rotation = Quaternion.Lerp(startRot, targetRotation, timeRot / duration);
            timeRot += Time.deltaTime;
            yield return null;
        }

    }

    public void TrackPose(bool value)
    {
        if (!value)
        {
            trackCamera.RegisterCameraPos();
            trackedPoseDriver.trackingType = TrackedPoseDriver.TrackingType.RotationOnly;
            trackCamera.SetCameraOffSet();
        }
        else
        {
            trackCamera.ResetCameraOffSet();
            trackedPoseDriver.trackingType = TrackedPoseDriver.TrackingType.RotationAndPosition;
        }
    }





    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawCube(initialPlayerPos.position, Vector3.one * 0.3f);


        Gizmos.DrawCube(finalPlayerPos.position + offSetSnapPos, Vector3.one * 0.3f);
        Gizmos.DrawRay(finalPlayerPos.position + offSetSnapPos, finalPlayerPos.forward);
    }

}
