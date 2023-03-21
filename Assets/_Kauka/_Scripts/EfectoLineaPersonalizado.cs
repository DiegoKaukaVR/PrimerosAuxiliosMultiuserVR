using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfectoLineaPersonalizado : MonoBehaviour
{
    LineRenderer linea;
    public Transform punto1;
    public Transform punto2;

    public float localScaleFeedback = 1f;

    /// <summary>
    /// Transforms attached to the target
    /// </summary>
    [Tooltip("Mete aquí Point1")]
    public Transform target1;
    [Tooltip("Mete aquí Point2")]
    public Transform target2;

    public bool OnGrab = true;
    public OnGrabAnimation ongrabAnimation;

    public GameObject parentGO;
  
    public void LazyStart()
    {
        //Debug.Log("Lazy Start");
        linea = GetComponent<LineRenderer>();

        if (OnGrab)
        {
            punto1.position = ongrabAnimation.posA;
            punto2.position = ongrabAnimation.posB;

            punto1.localScale = Vector3.one * localScaleFeedback;
            punto2.localScale = Vector3.one * localScaleFeedback;
        }


        linea.SetPosition(0, punto1.position);
        linea.SetPosition(1, punto2.position);
  
    }

    public void OnEnable()
    {
        StopCoroutine(Parpadeo());
        StartCoroutine(Parpadeo());
    }



    IEnumerator Parpadeo()
    {
        float t = 0;
        while (t < .5f)
        {
            t += Time.deltaTime;
            if (t < .25f)
            {
                punto1.gameObject.GetComponent<MeshRenderer>().materials[0].SetFloat("_Alpha", Mathf.Lerp(0.6f, 1, t / .25f));
            }
            else
            {
                punto1.gameObject.GetComponent<MeshRenderer>().materials[0].SetFloat("_Alpha", Mathf.Lerp(2, .6f, t / .5f));
            }
            yield return null;
        }
        t = 0;
        while (t < .5f)
        {
            t += Time.deltaTime;
            if (t < .25f)
            {
                punto2.gameObject.GetComponent<MeshRenderer>().materials[0].SetFloat("_Alpha", Mathf.Lerp(0.6f, 1, t / .25f));
            }
            else
            {
                punto2.gameObject.GetComponent<MeshRenderer>().materials[0].SetFloat("_Alpha", Mathf.Lerp(2, .6f, t / .5f));
            }
            yield return null;
        }

        yield return new WaitForSeconds(.7f);
        StartCoroutine(Parpadeo());
    }
}
