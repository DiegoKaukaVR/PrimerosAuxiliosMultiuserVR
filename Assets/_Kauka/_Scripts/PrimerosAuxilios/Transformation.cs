using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformation : MonoBehaviour
{
    public Vector3 offSet;
    public Vector3 rotation;

    Transform myTransform;

    public float timeToExecute;
    public bool enableScript = false;
    private void Awake()
    {
        if (!enableScript)
        {
            this.enabled = false;
        }
       
        
        myTransform = transform;
        StartCoroutine(CoroutineTransformation());
    }

    public void MakeTransformation()
    {
        myTransform.position += offSet;
        myTransform.rotation.eulerAngles.Set(rotation.x, rotation.y, rotation.z);
    }

    IEnumerator CoroutineTransformation()
    {
        yield return new WaitForSeconds(timeToExecute);
        MakeTransformation();
    }
    private void OnValidate()
    {
       
    }
    bool set;
    Vector3 newPoint;
    private void OnDrawGizmos()
    {
        //if (!set)
        //{
        //    set = true;
        //    newPoint = transform.position += offSet;
        //}
        //Gizmos.DrawCube(transform.position, 0.3f * Vector3.one);
        //Gizmos.DrawCube(newPoint, 0.3f * Vector3.one);
    }

}
