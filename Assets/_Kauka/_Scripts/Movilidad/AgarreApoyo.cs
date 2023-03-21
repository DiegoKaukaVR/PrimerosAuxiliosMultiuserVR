using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;
using UnityEngine.Animations.Rigging;

public class AgarreApoyo : MonoBehaviour
{
    public GameObject colliderDeshabilitado;
    public int indice;
    public bool habilitado;
    bool seleccionado;
    Vector3 posInit;
    Vector3 eulerinicial;
    Vector3 posInicial;
    public GameObject objetoARotar;
    public GameObject objetoAMover;
    public GameObject objetoTrasladado;
    public GameObject objetoTrasladado2;
    public GameObject objetoTrasladado3;
    public GameObject objetoTrasladado4;
    public GameObject objetoTrasladado5;
    public Vector3 cantidadTrasladado;
    public Vector3 cantidadTrasladado2;
    public Vector3 cantidadTrasladado4;
    public Vector3 cantidadTrasladado5;
    public Vector3 cantidadRotado3;
    Vector3 posInicialObjeto;
    Vector3 posInicialObjeto2;
    Vector3 posInicialObjeto4;
    Vector3 posInicialObjeto5;
    Vector3 eulerInicialObjeto3;
    public float limiteMin;
    public float limiteMax;



    public GameObject[] objetosActivadosAuxiliar;
    public GameObject[] objetosDesactivadosAuxiliar;

    public float expGirar = 230;

    public AnimationCurve curvaMovimientoM4X;
    public AnimationCurve curvaMovimientoM4Y;
    public float refMover;

    Transform parent;
    // Start is called before the first frame update
    void Start()
    {
       
        parent = transform.parent;
    }
    private void Update()
    {
        if (seleccionado)
        {
            transform.parent = parent;
            if (objetoARotar)
            {
                DetectMove();
            }
            if (objetoAMover)
            {
                MovimientoM4();
            }

        }
    }
    public void Agarre()
    {
        if (colliderDeshabilitado)
        {
            if (colliderDeshabilitado.GetComponent<XRGrabInteractable>())
            {

                foreach (Collider col in colliderDeshabilitado.GetComponent<XRGrabInteractable>().colliders)
                {
                    col.gameObject.SetActive(true);
                }
                colliderDeshabilitado.GetComponent<XRGrabInteractable>().enabled = true;
            }
            if (colliderDeshabilitado.GetComponent<XRSimpleInteractable>())
            {
                foreach (Collider col in colliderDeshabilitado.GetComponent<XRSimpleInteractable>().colliders)
                {
                    col.gameObject.SetActive(true);
                }
                colliderDeshabilitado.GetComponent<XRSimpleInteractable>().enabled = true;
            }
        }
        
        GetComponent<MeshRenderer>().materials[0].SetFloat("_Alpha", 0.5f);
    }
    public void SoltarAgarre()
    {
        seleccionado = false;
        if (colliderDeshabilitado)
        {
            if (colliderDeshabilitado.GetComponent<XRGrabInteractable>())
            {

                foreach (Collider col in colliderDeshabilitado.GetComponent<XRGrabInteractable>().colliders)
                {
                    col.gameObject.SetActive(false);
                }
                colliderDeshabilitado.GetComponent<XRGrabInteractable>().enabled = false;
            }
            if (colliderDeshabilitado.GetComponent<XRSimpleInteractable>())
            {
                foreach (Collider col in colliderDeshabilitado.GetComponent<XRSimpleInteractable>().colliders)
                {
                    col.gameObject.SetActive(false);
                }
                colliderDeshabilitado.GetComponent<XRSimpleInteractable>().enabled = false;
            }

        }
        //GetComponent<MeshRenderer>().materials[0].SetFloat("_Alpha", 0);
    }
    public void CambiarHabilitado()
    {
        if(habilitado == false)
        {
            habilitado = true;
        }
        if(habilitado == true)
        {
            habilitado = false;
        }
    }

    public void DetectMove()
    {
        if (seleccionado == false)
        {
            posInit = GetComponent<XRGrabInteractable>().interactorsSelecting[0].transform.position;
            //eulerinicial = objetoARotar.transform.localEulerAngles;   
            eulerinicial = objetoARotar.transform.eulerAngles;
            posInicialObjeto = objetoTrasladado.transform.position;

            if (objetoTrasladado2)
            {
                posInicialObjeto2 = objetoTrasladado2.transform.position;
            }
            if (objetoTrasladado3)
            {
                eulerInicialObjeto3 = objetoTrasladado3.transform.eulerAngles;
            }
            if (objetoTrasladado4)
            {
                posInicialObjeto4 = objetoTrasladado4.transform.position;
            }
            if (objetoTrasladado5)
            {
                posInicialObjeto5 = objetoTrasladado5.transform.position;
            }
        }
        seleccionado = true;
        float dist = posInit.y - GetComponent<XRGrabInteractable>().interactorsSelecting[0].transform.position.y;
       // objetoARotar.transform.localEulerAngles = new Vector3(eulerinicial.x, Mathf.Clamp((eulerinicial.z + dist*expGirar), limiteMin, limiteMax), eulerinicial.z);
       if(dist<=0)
       {
            foreach (GameObject go in objetosActivadosAuxiliar) { go.GetComponent<MultiParentConstraint>().weight = 1; }
            foreach (GameObject go in objetosDesactivadosAuxiliar) { go.GetComponent<MultiParentConstraint>().weight = 0; }


            objetoARotar.transform.eulerAngles = new Vector3(Mathf.Clamp((eulerinicial.z + dist * expGirar), limiteMin, limiteMax), eulerinicial.y, eulerinicial.z);

            //if (Mathf.Abs(objetoARotar.transform.localEulerAngles.y - 180) < 20)
            //{
            //    objetoARotar.transform.localEulerAngles = new Vector3(eulerinicial.x, 180, eulerinicial.z);
            //    GameControllerMovilidad.Instance.primeraCondicionActual = true;
            //    GameControllerMovilidad.Instance.CheckConditions();
            //}

            objetoTrasladado.transform.position = posInicialObjeto - new Vector3(Mathf.Lerp(0, cantidadTrasladado.x, -dist * 2), Mathf.Lerp(0, cantidadTrasladado.y, -dist * 2), Mathf.Lerp(0, cantidadTrasladado.z, -dist * 2));
            //objetoTrasladado2.transform.position = posInicialObjeto2 - new Vector3(Mathf.Clamp(rotAplicada / 90, 0, cantidadTrasladado2.x), Mathf.Clamp(rotAplicada / 90, 0, cantidadTrasladado2.y), Mathf.Clamp(rotAplicada / 90, 0, cantidadTrasladado2.z));
            //objetoTrasladado3.transform.position = posInicialObjeto3 - new Vector3(Mathf.Clamp(rotAplicada / 90, 0, cantidadTrasladado2.x), Mathf.Clamp(rotAplicada / 90, 0, cantidadTrasladado2.y), Mathf.Clamp(rotAplicada / 90, 0, cantidadTrasladado3.z));

            if (objetoTrasladado2)
            {
                objetoTrasladado2.transform.position = posInicialObjeto2 - new Vector3(Mathf.Lerp( 0, cantidadTrasladado2.x, - dist * 2), Mathf.Lerp(0,cantidadTrasladado2.y, -dist * 2), Mathf.Lerp(0,cantidadTrasladado2.z, -dist * 2));
            }
            if (objetoTrasladado3)
            {
                //objetoTrasladado3.transform.eulerAngles = new Vector3(eulerInicialObjeto3.z, eulerInicialObjeto3.y, Mathf.Clamp((eulerInicialObjeto3.z - dist * expGirar), limiteMin, limiteMax));
                objetoTrasladado3.transform.eulerAngles = new Vector3(Mathf.Lerp(eulerInicialObjeto3.x, -180, -dist * 2), eulerInicialObjeto3.y, eulerInicialObjeto3.z );
            }
            if (objetoTrasladado4)
            {
                objetoTrasladado4.transform.position = posInicialObjeto4 - new Vector3(Mathf.Lerp(0, cantidadTrasladado4.x, -dist * 2), Mathf.Lerp(0, cantidadTrasladado4.y, -dist * 2), Mathf.Lerp(0, cantidadTrasladado4.z, -dist * 2));
            }
            if (objetoTrasladado5)
            {
                objetoTrasladado5.transform.position = posInicialObjeto5 - new Vector3(Mathf.Lerp(0, cantidadTrasladado5.x, -dist * 2), Mathf.Lerp(0, cantidadTrasladado5.y, -dist * 2), Mathf.Lerp(0, cantidadTrasladado5.z, -dist * 2));
            }
            //if (Mathf.Abs(objetoARotar.transform.eulerAngles.x - 180) < 20)
            if (eulerinicial.z + dist * expGirar < limiteMin +10)
            {
                //objetoARotar.transform.eulerAngles = new Vector3(180, eulerinicial.y, eulerinicial.z);

                GameControllerMovilidad.Instance.primeraCondicionActual = true;
                GameControllerMovilidad.Instance.CheckConditions();
            }
       }

    }

    public void MovimientoM4()
    {
        if (seleccionado == false)
        {
            posInit = GetComponent<XRGrabInteractable>().interactorsSelecting[0].transform.position;
            posInicial = objetoAMover.transform.position;
        }
        seleccionado = true;
        float dist = posInit.x - GetComponent<XRGrabInteractable>().interactorsSelecting[0].transform.position.x;

        objetoAMover.transform.position = new Vector3(posInicial.x + curvaMovimientoM4X.Evaluate(dist/refMover), posInicial.y + curvaMovimientoM4Y.Evaluate(dist/refMover), posInicial.z);

        //if (Vector3.Distance(objetoAMover.transform.position, new Vector3(curvaMovimientoM4X.Evaluate(1), curvaMovimientoM4Y.Evaluate(1), objetoAMover.transform.position.z)) < .02f)
        if (dist/refMover > .9f)
        {
            objetoAMover.transform.position = new Vector3(posInicial.x + curvaMovimientoM4X.Evaluate(1), posInicial.y + curvaMovimientoM4Y.Evaluate(1), posInicial.z);
            GameControllerMovilidad.Instance.ConfirmarCondicionAuxiliar(5);
            GameControllerMovilidad.Instance.CheckConditions();
            seleccionado = false;
        }
    }

}
