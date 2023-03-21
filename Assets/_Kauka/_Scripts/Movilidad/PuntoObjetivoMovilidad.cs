using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntoObjetivoMovilidad : MonoBehaviour
{
    public GameObject objetoVisual;
    public int indiceAgarreObjetivo;

    GridPerso grid;

    bool puedeEscalar;
    bool escalado;
    float t = 0;
    Vector3 initialScale;
    Vector3 actualInitialScale;
    Vector3 actualScale;
    float maxSize;
    float tiempoCrecer;
    bool puntoCorrecto;
    public bool tomarRot;


    public GameObject agarre2Pos;
    public int indiceAgarre2;

    void Start()
    {
        //grid = FindObjectOfType<GridPerso>();

        //Vector3 finalPosition = grid.GetNearestPointOnGrid(transform.position, grid.colliderGeneral);

        //transform.position = finalPosition;
        objetoVisual.SetActive(false);
        //if (finalPosition == Vector3.zero)
        //{
        //    transform.position = lastGridPos;
        //}
        //else
        //{
        //    transform.position = finalPosition;
        //    lastGridPos = finalPosition;
        //}
        initialScale = transform.localScale;
        actualInitialScale = initialScale;
        //this.enabled = false;
    }
    void Update()
    {
        //if (puedeEscalar && !escalado)
        //{
        //    t += Time.deltaTime;
        //    transform.localScale = Vector3.Lerp(actualInitialScale, actualInitialScale * maxSize, t / tiempoCrecer);
        //    if (t >= tiempoCrecer)
        //    {
        //        t = 0;
        //        escalado = true;
        //        actualScale = transform.localScale;
        //        //this.enabled = false;
        //        print("Creciendo");
        //    }
        //}
        //if (!puedeEscalar && escalado)
        //{
        //    t += Time.deltaTime;
        //    transform.localScale = Vector3.Lerp(actualScale, actualInitialScale, t / tiempoCrecer);
        //    if (t >= tiempoCrecer)
        //    {
        //        t = 0;
        //        escalado = false;
        //        //this.enabled = false;
        //        print("small");
        //    }
        //}
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
        //this.enabled = false;
    }
    public void SetSecondInitialScale()
    {
        actualInitialScale = transform.localScale;
    }

}
