using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfectoLinea : MonoBehaviour
{
    LineRenderer linea;
    public Transform punto1;
    public Transform punto2;
    // Start is called before the first frame update
    void Start()
    {
        linea = GetComponent<LineRenderer>();
        linea.SetPosition(0, punto1.position);
        linea.SetPosition(1, punto2.position);
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
