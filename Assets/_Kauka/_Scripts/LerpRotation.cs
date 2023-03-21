using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpRotation : MonoBehaviour
{
    public Transform targetRot;
    public float lerpTime = 1f;
    Transform XRRigtransform;

    private void Awake()
    {
        XRRigtransform = GameManager.instance.xrRig.transform;
    }
    public void StartRotation()
    {
        StartCoroutine(LerpRotationCoroutine(targetRot.rotation, lerpTime));
    }
    public IEnumerator LerpRotationCoroutine(Quaternion targetRotation, float duration)
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

    private void OnDrawGizmos()
    {
        if (targetRot != null)
        {
            Gizmos.DrawRay(targetRot.position, targetRot.forward);
        }
    }
}
