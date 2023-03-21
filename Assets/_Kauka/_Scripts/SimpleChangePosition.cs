using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleChangePosition : MonoBehaviour
{
    [SerializeField] public Transform newPos;
    public void ChangePosition()
    {
        StartCoroutine(LerpPos(5));
       
    }

    IEnumerator LerpPos(float time)
    {
        while (Vector3.Distance(transform.position, newPos.position)>0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, newPos.position, time );
            yield return null;
        }

        yield return null;
    }

}
