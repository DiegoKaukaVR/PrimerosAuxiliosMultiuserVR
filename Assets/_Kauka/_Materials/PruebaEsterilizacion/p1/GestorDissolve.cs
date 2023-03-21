using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorDissolve : MonoBehaviour
{
    public GameObject xRaySphere;
    public GameObject nucleoExpositor;
    public GameObject tuerca;
    public float alturaCentro;
    public GameObject pastilla;
    public GameObject[] pastillas;
    public GameObject particulasDisolver;
    public float distancia;
    public bool xray = false;
    bool corteT = false;
    Vector3 originalPos;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (xray)
        {
            if (pastilla)
            {
                pastilla.GetComponent<MeshRenderer>().material.SetVector("_PlayerPos", xRaySphere.transform.position);
            }
            if (pastillas.Length > 0)
            {
                foreach(GameObject go in pastillas)
                {
                    go.GetComponent<MeshRenderer>().material.SetVector("_PlayerPos", xRaySphere.transform.position);
                    //if(Vector3.Distance(go.transform.position, xRaySphere.transform.position) < distancia)
                    //{
                    //    go.GetComponent<Rigidbody>().useGravity = true;
                    //}
                }

            }
            particulasDisolver.GetComponent<ParticleSystemRenderer>().material.SetVector("_PlayerPos", xRaySphere.transform.position);
            //Vector3 dirAlPlayer = (Camera.main.transform.position - (nucleoExpositor.transform.position)).normalized;
            //Vector3 posObjetivo = new Vector3(dirAlPlayer.x, dirAlPlayer.y, dirAlPlayer.z) * distancia;
            //xRaySphere.transform.localPosition = posObjetivo + (tuerca.transform.position - originalPos) + new Vector3(0, alturaCentro, 0);
        }
    }
    //public void ActivarXRay()
    //{
    //    if (xray == false)
    //    {

    //        xray = true;

    //        ei = FindObjectsOfType<ExpositorInteractable>();
    //        for (int i = 0; i < ei.Length; i++)
    //        {
    //            if (!ei[i].GetComponentInParent<AsignarPadreExpositor>())
    //            {
    //                ei[i].gameObject.GetComponent<MeshRenderer>().material = ei[i].gameObject.GetComponent<ExpositorInteractable>().materialSecundario;
    //                ei[i].gameObject.GetComponent<MeshRenderer>().material.SetVector("_PlayerPos", xRaySphere.transform.position);

    //            }

    //        }
    //    }
    //    else
    //    {
    //        for (int i = 0; i < ei.Length; i++)
    //        {
    //            ei[i].gameObject.GetComponent<MeshRenderer>().material = ei[i].gameObject.GetComponent<ExpositorInteractable>().materialPrimario;

    //        }
    //        xray = false;
    //    }
    //}
    //public void ActivarCorteT()
    //{
    //    if (corteT == false)
    //    {
    //        corteT = true;
    //    }
    //    else
    //    {
    //        corteT = false;
    //    }
    //}
}
