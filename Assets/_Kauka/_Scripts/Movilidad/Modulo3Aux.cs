using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modulo3Aux : MonoBehaviour
{
    public GameObject manoIzq;
    public GameObject manoDer;
    public GameObject rotateAroundGO;
    Vector3 posinicialIzq;
    Vector3 posinicialDer;

    bool block;

    bool canRotate;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(block == true)
        {
            manoIzq.transform.position = posinicialIzq;
            manoDer.transform.position = posinicialDer;
        }
        if (canRotate)
        {
            RotateAroundCintura();
        }
    }
    public void CogerPosiciones()
    {
        posinicialIzq = manoIzq.transform.position;
        posinicialDer = manoDer.transform.position;
        block = true;
    }

    public void Release()
    {
        block = false;
    }
    public void CanRotate()
    {
        canRotate = true;
    }
    public void RotateAroundCintura()
    {
        //manoIzq.transform.RotateAround(rotateAroundGO.transform.position, Vector3.up, );
        manoDer.transform.position = posinicialDer;
    }

    public void StopRotateAround()
    {
        canRotate = false;
    }
}
