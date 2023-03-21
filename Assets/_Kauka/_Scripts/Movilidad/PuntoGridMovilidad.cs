using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntoGridMovilidad : MonoBehaviour
{
    bool puedeEscalar;
    bool escalado;
    float t = 0;
    Vector3 initialScale;
    Vector3 actualInitialScale;
    Vector3 actualScale;
    float maxSize;
    float tiempoCrecer;
    bool puntoCorrecto;
    // Start is called before the first frame update
    void Start()
    {
        initialScale = transform.localScale;
        actualInitialScale = initialScale;
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (puedeEscalar && !escalado)
        {
            t += Time.deltaTime;
            transform.localScale = Vector3.Lerp(actualInitialScale, actualInitialScale * maxSize, t/tiempoCrecer);
            if (t >= tiempoCrecer)
            {
                t = 0;
                escalado = true;
                actualScale = transform.localScale;
                this.enabled = false;
            }
        }
        if(!puedeEscalar && escalado)
        {
            t += Time.deltaTime;
            transform.localScale = Vector3.Lerp(actualScale, actualInitialScale, t / tiempoCrecer);
            if (t >= tiempoCrecer)
            {
                t = 0;
                escalado = false;
                this.enabled = false;
            }
        }
    }
    public void Crecer(float sizeObj, float tiempoObj)
    {
        maxSize = sizeObj;
        tiempoCrecer = tiempoObj;

        puedeEscalar = true;
    }

    public void DeshacerCrecer(float tiempoObj)
    {
        tiempoCrecer = tiempoObj;

        puedeEscalar = false;
    }

    public void VolverOrigen()
    {
        //tiempoCrecer = tiempoObj;
        //escalado = true;
        //puedeEscalar = false;
        actualInitialScale = initialScale;
        transform.localScale = actualInitialScale;
        this.enabled = false;
    }
    public void SetSecondInitialScale()
    {
        actualInitialScale = transform.localScale;
    }
}
