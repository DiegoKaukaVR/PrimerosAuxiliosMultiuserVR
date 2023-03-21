using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;

public class ManivelaGrifoSimple : MonoBehaviour
{
    public ParticleSystem aguaGrifo;
    public ParticleSystem aguaGrifo2;


    public float emissionActualAgua;

    float rotInicial;
    Vector3 posInicial;
    Vector3 eulerinicial;
    bool seleccionado;
    public float aperturaMaxima;
    // Start is called before the first frame update
    void Start()
    {
        var emissionRateAgua = aguaGrifo.emission;
        emissionRateAgua.rateOverTime = 0;
        var emissionRateAgua2 = aguaGrifo2.emission;
        emissionRateAgua2.rateOverTime = 0;
        rotInicial = transform.eulerAngles.x;
        posInicial = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (seleccionado)
        {
            CheckApertura();
        }
    }

    public void CheckApertura()
    {
        seleccionado = true;
        GetComponent<Rigidbody>().isKinematic = false;
       
        
        //if (transform.eulerAngles.x > rotInicial + (aperturaMaxima - 10))
        //{
        //    print("Limite superior");
        //    //GetComponent<Rigidbody>().isKinematic = true;
        //    transform.eulerAngles = new Vector3(rotInicial + aperturaMaxima, transform.eulerAngles.y, transform.eulerAngles.z);
        //    //GetComponent<Collider>().enabled = false;
        //}
        //if (transform.eulerAngles.x < rotInicial + 10)
        //{
        //    print("Limite inferior");
        //    //GetComponent<Rigidbody>().isKinematic = true;
        //    transform.eulerAngles = new Vector3(rotInicial, transform.eulerAngles.y, transform.eulerAngles.z);
        //    //GetComponent<Collider>().enabled = false;
        //}
        float valorApertura = Mathf.Abs(transform.eulerAngles.x - rotInicial) / aperturaMaxima;
        
        var emissionRateAgua = aguaGrifo.emission;
        emissionRateAgua.rateOverTime = Mathf.Lerp(0, 50, valorApertura/10);
        var emissionRateAgua2 = aguaGrifo2.emission;
        emissionRateAgua2.rateOverTime = Mathf.Lerp(0, 50, valorApertura / 10);
    }
    public void SalirInteraccion()
    {
        seleccionado = false;
        if (Mathf.Abs(Mathf.Abs(transform.eulerAngles.x - rotInicial) - Mathf.Abs(aperturaMaxima)) < 10)
        {
            transform.eulerAngles = new Vector3(rotInicial + aperturaMaxima, transform.eulerAngles.y, transform.eulerAngles.z);
        }

        if (Mathf.Abs(transform.eulerAngles.x - rotInicial) < 25)
        {
            transform.eulerAngles = new Vector3(rotInicial, transform.eulerAngles.y, transform.eulerAngles.z);
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }
    public void ResetearPosicion()
    {
        transform.eulerAngles = new Vector3(rotInicial, transform.eulerAngles.y, transform.eulerAngles.z);
        transform.position = posInicial;
        var emissionRateAgua = aguaGrifo.emission;
        emissionRateAgua.rateOverTime = 0;
        var emissionRateAgua2 = aguaGrifo2.emission;
        emissionRateAgua2.rateOverTime = 0;
        // StartCoroutine(Reseteando());
    }

    //IEnumerator Reseteando()
    //{
    //    float t = 0;
    //    Vector3 rotIn = transform.eulerAngles;
    //    Vector3 posIn = transform.position;
    //    while (t < 1)
    //    {
    //        t += Time.deltaTime;
    //        transform.eulerAngles = Vector3.Lerp(rotIn, new Vector3(rotInicial, rotIn.y, rotIn.z), t);
    //        transform.position = Vector3.Lerp(posIn, posInicial, t);
    //        float valorApertura = Mathf.Abs(transform.eulerAngles.x - rotInicial) / aperturaMaxima;

    //        var emissionRateAgua = aguaGrifo.emission;
    //        emissionRateAgua.rateOverTime = Mathf.Lerp(0, 50, valorApertura / 14);
    //        var emissionRateAgua2 = aguaGrifo2.emission;
    //        emissionRateAgua2.rateOverTime = Mathf.Lerp(0, 50, valorApertura / 14);
    //        yield return null;
    //    }
    //}
    //
    //TEST CON FISICAS
    //


    //public void AbrirAgua()
    //{
    //    //transform.eulerAngles -= new Vector3(velocidadRotacion,0,0);
    //    if (puedeSubir)
    //    {
    //        GetComponent<Collider>().enabled = true;
    //        triggerBajar.SetActive(true);
    //        GetComponent<Rigidbody>().isKinematic = false;
    //        if (transform.eulerAngles.x > rotInicial + (aperturaMaxima - 10))
    //        {
    //            print("Limite superior");
    //            //GetComponent<Rigidbody>().isKinematic = true;
    //            transform.eulerAngles = new Vector3(rotInicial + aperturaMaxima, transform.eulerAngles.y, transform.eulerAngles.z);
    //            //GetComponent<Collider>().enabled = false;
    //            triggerSubir.SetActive(false);
    //        }
    //    }
    //    else
    //    {
    //        GetComponent<Collider>().enabled = false;
    //    }
    //}
    //public void CerrarAgua()
    //{
    //    if (puedeBajar)
    //    {
    //        GetComponent<Collider>().enabled = true;
    //        GetComponent<Rigidbody>().isKinematic = false;
    //        triggerSubir.SetActive(true);
    //        if (transform.eulerAngles.x < rotInicial + 10)
    //        {
    //            print("Limite inferior");
    //            //GetComponent<Rigidbody>().isKinematic = true;
    //            transform.eulerAngles = new Vector3(rotInicial, transform.eulerAngles.y, transform.eulerAngles.z);
    //            //GetComponent<Collider>().enabled = false;
    //            triggerBajar.SetActive(false);
    //        }
    //    }
    //    else
    //    {
    //        GetComponent<Collider>().enabled = false;
    //    }
    //    //transform.eulerAngles += new Vector3(velocidadRotacion, 0, 0);

    //}


}
   
