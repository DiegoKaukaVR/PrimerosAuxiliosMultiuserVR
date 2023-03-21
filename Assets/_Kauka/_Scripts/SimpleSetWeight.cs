using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
public class SimpleSetWeight : MonoBehaviour
{
    public Rig rig;
    public float velocity = 1f;
    public void SetRig()
    {
        StartCoroutine(LerpPos());
    }

    IEnumerator LerpPos()
    {
        while (rig.weight > 0.1f)
        {
            rig.weight -= Time.deltaTime * velocity;
            yield return null;
        }

        yield return null;
    }
}
