using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;
using UnityEngine.Events;

public class AgarreMovilidad : MonoBehaviour
{
    public int numeroAgarre;

    Transform origParent;
    bool seleccionando;

    Vector3 posInicial;
    bool hasPhysics;

    Vector3 eulerinicial;
    Quaternion rotInicialMando;
    Vector3 posInicialMando;
    Vector3 eulerInicialMando;
    Vector3 posInicialObjeto;
    Vector3 posInicialObjeto2;
    Vector3 posInicialObjeto3;
    Vector3 posInicialObjeto4;

    public bool soloRotacion;
    public bool sentidoContrario;
    public bool enX;
    public bool enY;
    public bool enZ;
    public GameObject objetoARotar; 
    public GameObject objetoARotar2;
    public float coefRot = 1.5f;
    public GameObject objetoTrasladado;
    public GameObject objetoTrasladado2;
    public GameObject objetoTrasladado3;
    public GameObject objetoTrasladado4;
    public Vector3 cantidadTrasladado;
    public Vector3 cantidadTrasladado2;
    public Vector3 cantidadTrasladado3;
    public Vector3 cantidadTrasladado4;

    public float limiteMin;
    public float limiteMax;
    public bool toMax;
    public Transform posicionInicial;
    public GameObject[] objetosActivadosAuxiliar;
    public GameObject[] objetosDesactivadosAuxiliar;
    public UnityEvent funcionAlSoltar;

    float rotAplicada;
    bool primeraVez;

    Color colorNormal;
    public Color colorInteractuar;
    float alfaNormal;
    float alfaInteractuar = 1;

     Vector3 eulerInicialObjeto;
    bool puedeRotar;

    void Start()
    {
        origParent = transform.parent;
        //eulerinicial = transform.localEulerAngles;
        
        posInicial = transform.position;
        if (GetComponent<XRGrabInteractable>())
        {
            if (GetComponent<XRGrabInteractable>().colliders[0] && GetComponent<XRGrabInteractable>().colliders[0].gameObject.GetComponent<MeshRenderer>().materials[0].GetColor("_BaseColor") != null) { colorNormal = GetComponent<XRGrabInteractable>().colliders[0].gameObject.GetComponent<MeshRenderer>().materials[0].GetColor("_BaseColor"); }
            if (GetComponent<XRGrabInteractable>().colliders[0] && GetComponent<XRGrabInteractable>().colliders[0].gameObject.GetComponent<MeshRenderer>().materials[0]) { alfaNormal = GetComponent<XRGrabInteractable>().colliders[0].gameObject.GetComponent<MeshRenderer>().materials[0].GetFloat("_Alpha"); }
        }
        
        if (GetComponent<Rigidbody>().useGravity) { hasPhysics = true; }
        //foreach (GameObject go in objetosDesactivadosAuxiliar) { go.GetComponent<MultiParentConstraint>().weight = 0; }
        //foreach (GameObject go in objetosDesactivadosAuxiliar) { go.GetComponent<MultiParentConstraint>().weight = 1;}
        if (posicionInicial) { transform.position = posicionInicial.position; } 

    }

    void Update()
    {
        if (seleccionando)
        {
            Seleccionar();
        }
    }

    public void Seleccionar()
    {
        
        if (soloRotacion)
        {
            if (seleccionando == false)
            {
                eulerInicialMando = GetComponent<XRGrabInteractable>().interactorsSelecting[0].transform.eulerAngles;
                rotInicialMando = GetComponent<XRGrabInteractable>().interactorsSelecting[0].transform.rotation;
                posInicialMando = GetComponent<XRGrabInteractable>().interactorsSelecting[0].transform.position;

                //eulerInicialMando = GetComponent<XRGrabInteractable>().interactorsSelecting[0].transform.eulerAngles;
                if (objetoARotar2)
                {
                    eulerinicial = objetoARotar2.transform.eulerAngles;
                }
                else
                {
                    eulerinicial = objetoARotar.transform.localEulerAngles;
                }
                posInicialObjeto = objetoTrasladado.transform.position;
                
                if (objetoTrasladado2)
                {
                    posInicialObjeto2 = objetoTrasladado2.transform.position;
                }
                if (objetoTrasladado3)
                {
                    posInicialObjeto3 = objetoTrasladado3.transform.position;
                }
                if (objetoTrasladado4)
                {
                    posInicialObjeto4 = objetoTrasladado4.transform.position;
                }
            }

            //rotAplicada = Quaternion.Angle(rotInicialMando, GetComponent<XRGrabInteractable>().interactorsSelecting[0].transform.rotation);
            float direccionAplicada = (((eulerInicialMando.z - GetComponent<XRGrabInteractable>().interactorsSelecting[0].transform.eulerAngles.z) + 360f) % 360f) > 180f ? -1 : 1;
            float distZ = posInicialMando.z - GetComponent<XRGrabInteractable>().interactorsSelecting[0].transform.position.z;
            rotAplicada = distZ * 200;
            if (enY)
            {
                if (objetoARotar2)
                {
                    objetoARotar2.transform.eulerAngles = new Vector3(Mathf.Clamp((eulerinicial.x + rotAplicada * coefRot), limiteMin, limiteMax + eulerinicial.x), eulerinicial.y, eulerinicial.z);
                    //objetoARotar2.transform.localEulerAngles = new Vector3(Mathf.Clamp((eulerinicial.z - rotAplicada), limiteMin, limiteMax + eulerinicial.z), eulerinicial.y, eulerinicial.z);
                }
                else
                {
                    objetoARotar.transform.localEulerAngles = new Vector3(eulerinicial.x, Mathf.Clamp((eulerinicial.z - rotAplicada), limiteMin, limiteMax - eulerinicial.z), eulerinicial.z);
                }
                //if (toMax)
                //{
                //    //objetoTrasladado.transform.position = posInicialObjeto + new Vector3(Mathf.Lerp(0, 0.4f, rotAplicada / limiteMax), 0, 0);
                //    objetoTrasladado.transform.position = posInicialObjeto + new Vector3(0, 0, -.3f);
                //}
                //else
                //{
                //    //objetoTrasladado.transform.position = posInicialObjeto + new Vector3(Mathf.Lerp( 0, 0.4f, rotAplicada / limiteMin), 0, 0);
                //    objetoTrasladado.transform.position = posInicialObjeto + new Vector3(0, 0, -.3f);
                //}
                if (eulerinicial.z - rotAplicada < limiteMax)
                {

                    objetoTrasladado.transform.position = posInicialObjeto - new Vector3(Mathf.Lerp(0, cantidadTrasladado.x, rotAplicada / -limiteMin), Mathf.Lerp(0, cantidadTrasladado.y, rotAplicada / 90), Mathf.Lerp(0, cantidadTrasladado.z, rotAplicada / 90));
                    //objetoTrasladado2.transform.position = posInicialObjeto2 - new Vector3(Mathf.Clamp(rotAplicada / 90, 0, cantidadTrasladado2.x), Mathf.Clamp(rotAplicada / 90, 0, cantidadTrasladado2.y), Mathf.Clamp(rotAplicada / 90, 0, cantidadTrasladado2.z));
                    //objetoTrasladado3.transform.position = posInicialObjeto3 - new Vector3(Mathf.Clamp(rotAplicada / 90, 0, cantidadTrasladado2.x), Mathf.Clamp(rotAplicada / 90, 0, cantidadTrasladado2.y), Mathf.Clamp(rotAplicada / 90, 0, cantidadTrasladado3.z));

                    if (objetoTrasladado2)
                    {
                        objetoTrasladado2.transform.position = posInicialObjeto2 - new Vector3(Mathf.Lerp(0, cantidadTrasladado2.x, rotAplicada / -limiteMin), Mathf.Lerp(0, cantidadTrasladado2.y, rotAplicada / 90), Mathf.Lerp(0, cantidadTrasladado2.z, rotAplicada / 90));
                    }
                    if (objetoTrasladado3)
                    {
                        objetoTrasladado3.transform.position = posInicialObjeto3 - new Vector3(Mathf.Lerp(0, cantidadTrasladado3.x, rotAplicada / -limiteMin), Mathf.Lerp(0, cantidadTrasladado3.y, rotAplicada / 90), Mathf.Lerp(0, cantidadTrasladado3.z, rotAplicada / 90));
                    }
                    if (objetoTrasladado4)
                    {
                        objetoTrasladado4.transform.position = posInicialObjeto4 - new Vector3(Mathf.Lerp(0, cantidadTrasladado4.x, rotAplicada / -limiteMin), Mathf.Lerp(0, cantidadTrasladado4.y, rotAplicada / 90), Mathf.Lerp(0, cantidadTrasladado4.z, rotAplicada / 90));
                    }

                }
            }
            //if (enY)
            //{
            //    if (sentidoContrario)
            //    {
            //        if (direccionAplicada == 1)
            //        {
            //            //transform.localEulerAngles = new Vector3(eulerinicial.x, Mathf.Clamp((eulerinicial.z - rotAplicada), limiteMin, limiteMax), eulerinicial.z);
            //            //objetoARotar.transform.localEulerAngles += new Vector3(0, Mathf.Clamp((eulerinicial.z - rotAplicada), limiteMin, limiteMax), 0);
            //            if (objetoARotar2)
            //            {

            //                objetoARotar2.transform.localEulerAngles = new Vector3(eulerinicial.x, Mathf.Clamp((eulerinicial.z - rotAplicada), limiteMin, limiteMax - eulerinicial.z), eulerinicial.z);
            //            }
            //            else
            //            {
            //                objetoARotar.transform.localEulerAngles += new Vector3(0, Mathf.Clamp((eulerinicial.z - rotAplicada), limiteMin, limiteMax), 0);
            //            }

            //            //if (toMax)
            //            //{
            //            //    objetoTrasladado.transform.position = posInicialObjeto + new Vector3(Mathf.Lerp(0, 0.4f, rotAplicada / limiteMax), 0, 0);
            //            //}
            //            //else
            //            //{
            //            //    objetoTrasladado.transform.position = posInicialObjeto + new Vector3(Mathf.Lerp(0, 0.4f, limiteMin / rotAplicada), 0, 0);
            //            //}
            //            if (eulerinicial.z - rotAplicada < limiteMax)
            //            {

            //                objetoTrasladado.transform.position = posInicialObjeto - new Vector3(Mathf.Lerp(0, cantidadTrasladado.x, rotAplicada / -limiteMin), Mathf.Lerp(0, cantidadTrasladado.y, rotAplicada / 90), Mathf.Lerp(0, cantidadTrasladado.z, rotAplicada / 90));
            //                //objetoTrasladado2.transform.position = posInicialObjeto2 - new Vector3(Mathf.Clamp(rotAplicada / 90, 0, cantidadTrasladado2.x), Mathf.Clamp(rotAplicada / 90, 0, cantidadTrasladado2.y), Mathf.Clamp(rotAplicada / 90, 0, cantidadTrasladado2.z));
            //                //objetoTrasladado3.transform.position = posInicialObjeto3 - new Vector3(Mathf.Clamp(rotAplicada / 90, 0, cantidadTrasladado2.x), Mathf.Clamp(rotAplicada / 90, 0, cantidadTrasladado2.y), Mathf.Clamp(rotAplicada / 90, 0, cantidadTrasladado3.z));


            //                //EL SENTIDO ELIGE EL MOVIMIENTO, CAMBIAR
            //                if (objetoTrasladado2)
            //                {
            //                    objetoTrasladado2.transform.position = posInicialObjeto2 - new Vector3(Mathf.Lerp(0, cantidadTrasladado2.x, rotAplicada / -limiteMin), Mathf.Lerp(0, cantidadTrasladado2.y, rotAplicada / 90), Mathf.Lerp(0, cantidadTrasladado2.z, rotAplicada / 90));
            //                }
            //                if (objetoTrasladado3)
            //                {
            //                    objetoTrasladado3.transform.position = posInicialObjeto3 - new Vector3(Mathf.Lerp(0, cantidadTrasladado3.x, rotAplicada / -limiteMin), Mathf.Lerp(0, cantidadTrasladado3.y, rotAplicada / 90), Mathf.Lerp(0, cantidadTrasladado3.z, rotAplicada / 90));
            //                }
            //            }
            //        }
            //        else
            //        {
            //            //transform.localEulerAngles = new Vector3(eulerinicial.x, Mathf.Clamp((eulerinicial.z + rotAplicada), limiteMin, limiteMax), eulerinicial.z);
            //            //objetoARotar.transform.localEulerAngles += new Vector3(0, Mathf.Clamp((eulerinicial.z + rotAplicada), limiteMin, limiteMax), 0);
            //            if (objetoARotar2)
            //            {
            //                objetoARotar2.transform.localEulerAngles = new Vector3(Mathf.Clamp((eulerinicial.z + rotAplicada), limiteMin, limiteMax + eulerinicial.z), eulerinicial.y, eulerinicial.z);
            //            }
            //            else
            //            {
            //                objetoARotar.transform.localEulerAngles += new Vector3(0, Mathf.Clamp((eulerinicial.z + rotAplicada), limiteMin, limiteMax), 0);
            //            }

            //            //if (toMax)
            //            //{
            //            //    objetoTrasladado.transform.position = posInicialObjeto + new Vector3(Mathf.Lerp(0, 0.4f, rotAplicada / limiteMax), 0, 0);
            //            //}
            //            //else
            //            //{
            //            //    objetoTrasladado.transform.position = posInicialObjeto + new Vector3(Mathf.Lerp(0, 0.4f, limiteMin / rotAplicada), 0, 0);
            //            //}
            //            if (eulerinicial.z - rotAplicada < limiteMax)
            //            {

            //                objetoTrasladado.transform.position = posInicialObjeto - new Vector3(Mathf.Lerp(0, cantidadTrasladado.x, rotAplicada / -limiteMin), Mathf.Lerp(0, cantidadTrasladado.y, rotAplicada / 90), Mathf.Lerp(0, cantidadTrasladado.z, rotAplicada / 90));
            //                //objetoTrasladado2.transform.position = posInicialObjeto2 - new Vector3(Mathf.Clamp(rotAplicada / 90, 0, cantidadTrasladado2.x), Mathf.Clamp(rotAplicada / 90, 0, cantidadTrasladado2.y), Mathf.Clamp(rotAplicada / 90, 0, cantidadTrasladado2.z));
            //                //objetoTrasladado3.transform.position = posInicialObjeto3 - new Vector3(Mathf.Clamp(rotAplicada / 90, 0, cantidadTrasladado2.x), Mathf.Clamp(rotAplicada / 90, 0, cantidadTrasladado2.y), Mathf.Clamp(rotAplicada / 90, 0, cantidadTrasladado3.z));

            //                if (objetoTrasladado2)
            //                {
            //                    objetoTrasladado2.transform.position = posInicialObjeto2 - new Vector3(Mathf.Lerp(0, cantidadTrasladado2.x, rotAplicada / -limiteMin), Mathf.Lerp(0, cantidadTrasladado2.y, rotAplicada / 90), Mathf.Lerp(0, cantidadTrasladado2.z, rotAplicada / 90));
            //                }
            //                if (objetoTrasladado3)
            //                {
            //                    objetoTrasladado3.transform.position = posInicialObjeto3 - new Vector3(Mathf.Lerp(0, cantidadTrasladado3.x, rotAplicada / -limiteMin), Mathf.Lerp(0, cantidadTrasladado3.y, rotAplicada / 90), Mathf.Lerp(0, cantidadTrasladado3.z, rotAplicada / 90));
            //                }
            //            }
            //        }
            //    }
            //    else
            //    {
            //        if (direccionAplicada == 1)
            //        {
            //            //transform.localEulerAngles = new Vector3(eulerinicial.x, Mathf.Clamp((eulerinicial.z + rotAplicada), limiteMin, limiteMax), eulerinicial.z);
            //            //objetoARotar.transform.localEulerAngles += new Vector3(0, Mathf.Clamp((eulerinicial.z + rotAplicada), limiteMin, limiteMax), 0);

            //            //objetoARotar.transform.localEulerAngles = new Vector3(eulerinicial.x, Mathf.Clamp((eulerinicial.z + rotAplicada), limiteMin , limiteMax - eulerinicial.z), eulerinicial.z);
            //            if (objetoARotar2)
            //            {
            //                objetoARotar2.transform.eulerAngles = new Vector3(Mathf.Clamp((eulerinicial.x - rotAplicada*coefRot), limiteMin, limiteMax + eulerinicial.x), eulerinicial.y, eulerinicial.z);
            //                //objetoARotar2.transform.localEulerAngles = new Vector3(Mathf.Clamp((eulerinicial.z - rotAplicada), limiteMin, limiteMax + eulerinicial.z), eulerinicial.y, eulerinicial.z);
            //            }
            //            else
            //            {
            //                objetoARotar.transform.localEulerAngles = new Vector3(eulerinicial.x, Mathf.Clamp((eulerinicial.z + rotAplicada), limiteMin, limiteMax - eulerinicial.z), eulerinicial.z);
            //            }
            //            //if (toMax)
            //            //{
            //            //    //objetoTrasladado.transform.position = posInicialObjeto + new Vector3(Mathf.Lerp(0, 0.4f, rotAplicada / limiteMax), 0, 0);
            //            //    objetoTrasladado.transform.position = posInicialObjeto + new Vector3(0, 0, -.3f);
            //            //}
            //            //else
            //            //{
            //            //    //objetoTrasladado.transform.position = posInicialObjeto + new Vector3(Mathf.Lerp( 0, 0.4f, rotAplicada / limiteMin), 0, 0);
            //            //    objetoTrasladado.transform.position = posInicialObjeto + new Vector3(0, 0, -.3f);
            //            //}
            //            if (eulerinicial.z - rotAplicada < limiteMax)
            //            {

            //                objetoTrasladado.transform.position = posInicialObjeto - new Vector3(Mathf.Lerp(0, cantidadTrasladado.x, rotAplicada / -limiteMin), Mathf.Lerp(0, cantidadTrasladado.y, rotAplicada / 90), Mathf.Lerp(0, cantidadTrasladado.z, rotAplicada / 90));
            //                //objetoTrasladado2.transform.position = posInicialObjeto2 - new Vector3(Mathf.Clamp(rotAplicada / 90, 0, cantidadTrasladado2.x), Mathf.Clamp(rotAplicada / 90, 0, cantidadTrasladado2.y), Mathf.Clamp(rotAplicada / 90, 0, cantidadTrasladado2.z));
            //                //objetoTrasladado3.transform.position = posInicialObjeto3 - new Vector3(Mathf.Clamp(rotAplicada / 90, 0, cantidadTrasladado2.x), Mathf.Clamp(rotAplicada / 90, 0, cantidadTrasladado2.y), Mathf.Clamp(rotAplicada / 90, 0, cantidadTrasladado3.z));

            //                if (objetoTrasladado2)
            //                {
            //                    objetoTrasladado2.transform.position = posInicialObjeto2 - new Vector3(Mathf.Lerp(0, cantidadTrasladado2.x, rotAplicada / -limiteMin), Mathf.Lerp(0, cantidadTrasladado2.y, rotAplicada / 90), Mathf.Lerp(0, cantidadTrasladado2.z, rotAplicada / 90));
            //                }
            //                if (objetoTrasladado3)
            //                {
            //                    objetoTrasladado3.transform.position = posInicialObjeto3 - new Vector3(Mathf.Lerp(0, cantidadTrasladado3.x, rotAplicada / -limiteMin), Mathf.Lerp(0, cantidadTrasladado3.y, rotAplicada / 90), Mathf.Lerp(0, cantidadTrasladado3.z, rotAplicada / 90));
            //                }

            //            }
            //        }
            //        else
            //        {
            //            //transform.localEulerAngles = new Vector3(eulerinicial.x, Mathf.Clamp((eulerinicial.z - rotAplicada), limiteMin, limiteMax), eulerinicial.z);
            //            //objetoARotar.transform.localEulerAngles += new Vector3(0, Mathf.Clamp((eulerinicial.z - rotAplicada), limiteMin, limiteMax),0);
            //            print("-");
            //            //objetoARotar.transform.localEulerAngles = new Vector3(eulerinicial.x, Mathf.Clamp((eulerinicial.z - rotAplicada), limiteMin , limiteMax - eulerinicial.z), eulerinicial.z);
            //            if (objetoARotar2)
            //            {

            //                objetoARotar2.transform.eulerAngles = new Vector3(Mathf.Clamp((eulerinicial.x + rotAplicada* coefRot), limiteMin, limiteMax + eulerinicial.x), eulerinicial.y, eulerinicial.z);
            //                //objetoARotar2.transform.localEulerAngles = new Vector3(Mathf.Clamp((eulerinicial.z + rotAplicada), limiteMin, limiteMax + eulerinicial.z), eulerinicial.y, eulerinicial.z);
            //            }
            //            else
            //            {
            //                objetoARotar.transform.localEulerAngles = new Vector3(eulerinicial.x, Mathf.Clamp((eulerinicial.z - rotAplicada), limiteMin, limiteMax - eulerinicial.z), eulerinicial.z);
            //            }

            //            //if (toMax)
            //            //{
            //            //    //objetoTrasladado.transform.position = posInicialObjeto + new Vector3(Mathf.Lerp(0, 0.4f, rotAplicada / limiteMax), 0, 0);
            //            //    objetoTrasladado.transform.position = posInicialObjeto + new Vector3(0, 0, -.3f);
            //            //}
            //            //else
            //            //{
            //            //    //objetoTrasladado.transform.position = posInicialObjeto + new Vector3(Mathf.Lerp(0, 0.4f, rotAplicada / limiteMin), 0, 0);
            //            //    objetoTrasladado.transform.position = posInicialObjeto + new Vector3(0, 0, -.3f);
            //            //}
            //            if (eulerinicial.z - rotAplicada < limiteMax)
            //            {

            //                objetoTrasladado.transform.position = posInicialObjeto - new Vector3(Mathf.Lerp(0, cantidadTrasladado.x, rotAplicada / -limiteMin), Mathf.Lerp(0, cantidadTrasladado.y, rotAplicada / 90), Mathf.Lerp(0, cantidadTrasladado.z, rotAplicada / 90));                            //objetoTrasladado2.transform.position = posInicialObjeto2 - new Vector3(Mathf.Clamp(rotAplicada / 90, 0, cantidadTrasladado2.x), Mathf.Clamp(rotAplicada / 90, 0, cantidadTrasladado2.y), Mathf.Clamp(rotAplicada / 90, 0, cantidadTrasladado2.z));
            //                //objetoTrasladado3.transform.position = posInicialObjeto3 - new Vector3(Mathf.Clamp(rotAplicada / 90, 0, cantidadTrasladado2.x), Mathf.Clamp(rotAplicada / 90, 0, cantidadTrasladado2.y), Mathf.Clamp(rotAplicada / 90, 0, cantidadTrasladado3.z));


            //                if (objetoTrasladado2)
            //                {
            //                    objetoTrasladado2.transform.position = posInicialObjeto2 - new Vector3(Mathf.Lerp(0, cantidadTrasladado2.x, rotAplicada / -limiteMin), Mathf.Lerp(0, cantidadTrasladado2.y, rotAplicada / 90), Mathf.Lerp(0, cantidadTrasladado2.z, rotAplicada / 90));
            //                }
            //                if (objetoTrasladado3)
            //                {
            //                    objetoTrasladado3.transform.position = posInicialObjeto3 - new Vector3(Mathf.Lerp(0, cantidadTrasladado3.x, rotAplicada / -limiteMin), Mathf.Lerp(0, cantidadTrasladado3.y, rotAplicada / 90), Mathf.Lerp(0, cantidadTrasladado3.z, rotAplicada / 90));
            //                }


            //            }
            //        }
            //    }
            //}
            if (enZ)
            {

                if (sentidoContrario)
                {
                    if (direccionAplicada == 1)
                    {
                        transform.localEulerAngles =  new Vector3(eulerinicial.x, eulerinicial.y, Mathf.Clamp((eulerinicial.z - rotAplicada), limiteMin, limiteMax));
                        objetoTrasladado.transform.position += new Vector3(Mathf.Clamp(rotAplicada, 0, 0.2f), 0,0);
                    }
                    else
                    {
                        transform.localEulerAngles = new Vector3(eulerinicial.x, eulerinicial.y, Mathf.Clamp((eulerinicial.z + rotAplicada), limiteMin, limiteMax));
                        objetoTrasladado.transform.position += new Vector3(Mathf.Clamp(rotAplicada, 0, 0.2f), 0, 0);
                    }
                }
                else
                {
                    if (direccionAplicada == 1)
                    {
                        transform.localEulerAngles = new Vector3(eulerinicial.x, eulerinicial.y, Mathf.Clamp((eulerinicial.z + rotAplicada), limiteMin, limiteMax));
                        objetoTrasladado.transform.position += new Vector3(Mathf.Clamp(rotAplicada, 0, 0.2f), 0, 0);
                    }
                    else
                    {
                        transform.localEulerAngles = new Vector3(eulerinicial.x, eulerinicial.y, Mathf.Clamp((eulerinicial.z - rotAplicada), limiteMin, limiteMax));
                        objetoTrasladado.transform.position += new Vector3(Mathf.Clamp(rotAplicada, 0, 0.2f), 0, 0);
                    }
                }
            }
        }
        foreach(Collider col in GetComponent<XRGrabInteractable>().colliders)
        {
            col.gameObject.GetComponent<MeshRenderer>().materials[0].SetColor("_BaseColor", colorInteractuar);
        }
        //if (GetComponent<XRGrabInteractable>().colliders[0]) { GetComponent<XRGrabInteractable>().colliders[0].gameObject.GetComponent<MeshRenderer>().materials[0].SetColor("_BaseColor", colorInteractuar); }
        foreach (Collider col in GetComponent<XRGrabInteractable>().colliders)
        {
            col.gameObject.GetComponent<MeshRenderer>().materials[0].SetFloat("_Alpha", alfaInteractuar);
        }
        
        //if (GetComponent<XRGrabInteractable>().colliders[0]) { GetComponent<XRGrabInteractable>().colliders[0].gameObject.GetComponent<MeshRenderer>().materials[0].SetFloat("_Alpha", alfaInteractuar); }
        seleccionando = true;
        //if (GetComponent<XRGrabInteractable>().trackRotation)
        //{
        //    puedeRotar = true;
        //    GetComponent<XRGrabInteractable>().trackRotation = false;
        //}
        //if (puedeRotar)
        //{
        //    //float direccionAplicada = (((eulerInicialMando.x - GetComponent<XRGrabInteractable>().interactorsSelecting[0].transform.eulerAngles.x) + 360f) % 360f) > 180f ? -1 : 1;
        //    if (!trackRotContrario)
        //    {
        //        transform.eulerAngles = eulerInicialObjeto + (GetComponent<XRGrabInteractable>().interactorsSelecting[0].transform.eulerAngles - eulerInicialMando);
        //    }
        //    else
        //    {
        //        transform.eulerAngles = eulerInicialObjeto - (GetComponent<XRGrabInteractable>().interactorsSelecting[0].transform.eulerAngles - eulerInicialMando);
        //    }
        //}

        
        transform.parent = origParent;
        
        foreach (GameObject go in objetosActivadosAuxiliar) { go.GetComponent<MultiParentConstraint>().weight = 1; }
        foreach (GameObject go in objetosDesactivadosAuxiliar) { go.GetComponent<MultiParentConstraint>().weight = 0; }
        //if (hasPhysics) { GetComponent<Rigidbody>().useGravity = false; }
        //if (GetComponentInChildren<Rigidbody>()) { GetComponentInChildren<Rigidbody>().useGravity = false; }    
       
    }

    public void Soltar()
    {
        seleccionando = false;
        //print(transform.eulerAngles.y);
        if (soloRotacion && funcionAlSoltar != null)
        {
            float ang = 0;
            if (toMax)
            {
                
                if (objetoARotar2)
                {
                    ang = Mathf.Abs(objetoARotar2.transform.eulerAngles.x) ;
                }
                else
                {
                    ang = Mathf.Abs(objetoARotar.transform.localEulerAngles.y - limiteMax);
                }

            }
            else
            {
                if (objetoARotar2)
                {

                    ang = Mathf.Abs(objetoARotar2.transform.eulerAngles.x - limiteMin);
                }
                else
                {
                    ang = Mathf.Abs(objetoARotar.transform.localEulerAngles.y - limiteMin);
                }
            }
            if (ang >= 360)
            {
                ang -= 360;
            }
            if (Mathf.Abs(ang)< 20)
            {
                funcionAlSoltar.Invoke();
            }
        }
        if (GetComponent<XRGrabInteractable>().colliders[0]) { GetComponent<XRGrabInteractable>().colliders[0].gameObject.GetComponent<MeshRenderer>().materials[0].SetColor("_BaseColor", colorNormal); }
        if (GetComponent<XRGrabInteractable>().colliders[0]) { GetComponent<XRGrabInteractable>().colliders[0].gameObject.GetComponent<MeshRenderer>().materials[0].SetFloat("_Alpha", alfaNormal); }
        if (hasPhysics) { GetComponent<Rigidbody>().useGravity = true; }
        if (GetComponentInChildren<Rigidbody>()) { GetComponentInChildren<Rigidbody>().useGravity = true; }

        if (soloRotacion)
        {
            if (transform.localEulerAngles.y > 180)
            {
                eulerinicial = new Vector3(eulerinicial.x, eulerinicial.y, transform.localEulerAngles.y - 360);
            }
            else
            {
                eulerinicial = new Vector3(eulerinicial.x, eulerinicial.y, transform.localEulerAngles.y);
            }
        }
        //foreach (GameObject go in objetosDesactivadosAuxiliar) { go.GetComponent<MultiParentConstraint>().weight = 0; }
        //foreach (GameObject go in objetosDesactivadosAuxiliar) { go.GetComponent<MultiParentConstraint>().weight = 1; }

    }

    public void AsignarPosicion(Transform posTransformObj)
    {
        transform.position = posTransformObj.position;
    }
    public void Printear()
    {
        print("EnPos");
    }
    public void Levantar()
    {
        if (toMax)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, limiteMax);

        }
        else
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, limiteMin);
        }
    }
}
