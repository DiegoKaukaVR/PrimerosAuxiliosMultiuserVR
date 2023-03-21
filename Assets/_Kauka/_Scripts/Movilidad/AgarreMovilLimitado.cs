using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;

public class AgarreMovilLimitado : MonoBehaviour
{

    public float movimientoPermitidoX;
    public float movimientoPermitidoY;
    public float movimientoPermitidoZ;


    public GameObject rotationHint;
    public GameObject rotationTarget;
    public GameObject secondRotationTarget;

    public float expSeguir;
    public float expSeguirSecundario;

    bool seleccionado;

    Vector3 posInicialMando;
    Vector3 posInicial;
    Vector3 posRelativa;

    Transform origParent;
    Vector3 posCorrecta;
    Vector3 eulerInicial;
    Vector3 secondEulerInicial;
    float distanciaEntreReferencia;

    bool nullX;
    bool nullY;
    bool nullZ;

    public bool puedeSeguir;

    
    // Start is called before the first frame update
    void Start()
    {
        origParent = transform.parent;
        posInicial = transform.position;
        eulerInicial = transform.eulerAngles;
        if (secondRotationTarget)
        {
            secondEulerInicial = secondRotationTarget.transform.eulerAngles;
            distanciaEntreReferencia = Vector3.Distance(transform.position, secondRotationTarget.transform.position);
        }
        puedeSeguir = true;
    }
    private void Update()
    {
        if (rotationHint && rotationTarget)
        {
            if (puedeSeguir)
            {
                Vector3 direccionHint = rotationTarget.transform.position - rotationHint.transform.position;
                //float angulo = Vector3.Angle(upInicial, new Vector3(transform.up.x,transform.up.y,direccionHint.z));
                transform.eulerAngles = eulerInicial + new Vector3(transform.eulerAngles.x, direccionHint.z * expSeguir, direccionHint.y * expSeguir * 1);
                //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direccionHint, rotationTarget.transform.up), expSeguirSecundario * Time.deltaTime);

                //secondRotationTarget.transform.eulerAngles = secondEulerInicial + new Vector3(secondEulerInicial.x, direccionHint.z, secondEulerInicial.z);


                // este // secondRotationTarget.transform.eulerAngles = secondEulerInicial + new Vector3(0, direccionHint.z * expSeguir, direccionHint.y * expSeguir*1);


                secondRotationTarget.transform.eulerAngles = secondEulerInicial + new Vector3(0, direccionHint.z * expSeguir, 0);

                //secondRotationTarget.transform.rotation = Quaternion.RotateTowards(secondRotationTarget.transform.rotation, Quaternion.Euler(90, -transform.rotation.x, -90), expSeguirSecundario * Time.deltaTime);


                //secondRotationTarget.transform.eulerAngles = Vector3.Lerp(secondRotationTarget.transform.eulerAngles,new Vector3(secondRotationTarget.transform.eulerAngles.x, transform.eulerAngles.y, secondRotationTarget.transform.eulerAngles.z), 1);

                //secondRotationTarget.transform.rotation = Quaternion.RotateTowards(secondRotationTarget.transform.rotation, new Quaternion(transform.rotation.x, secondRotationTarget.transform.rotation.y, secondRotationTarget.transform.rotation.z,0), expSeguirSecundario * Time.deltaTime);
            }
        }

        if (seleccionado)
        {
            Coger();
            //if (rotationHint && rotationTarget)
            //{
            //    Vector3 direccionHint = rotationTarget.transform.position - rotationHint.transform.position;
            //    //float angulo = Vector3.Angle(upInicial, new Vector3(transform.up.x,transform.up.y,direccionHint.z));
            //    transform.eulerAngles = eulerInicial + new Vector3(0, direccionHint.z * expSeguir, 0);
            //    secondRotationTarget.transform.eulerAngles = secondEulerInicial + new Vector3(0, direccionHint.z * expSeguirSecundario, 0);
            //}
        }
    }
    public void VolverAActivar()
    {
        //secondEulerInicial += new Vector3(-170, 0, 0);
        //eulerInicial += new Vector3(-170, 0, 0);
        secondEulerInicial = secondEulerInicial = secondRotationTarget.transform.eulerAngles;
        eulerInicial = transform.eulerAngles;
        puedeSeguir = true;
    }
    public void DesactivarSeguimiento()
    {
        
        puedeSeguir = false;
    }
    public void Coger()
    {
        if (seleccionado == false)
        {
            posInicialMando = GetComponent<XRGrabInteractable>().interactorsSelecting[0].transform.position;
            posRelativa = transform.position;
        }

        seleccionado = true;

        float diferenciaActualX = posInicialMando.x - GetComponent<XRGrabInteractable>().interactorsSelecting[0].transform.position.x;
        float diferenciaActualY = posInicialMando.y - GetComponent<XRGrabInteractable>().interactorsSelecting[0].transform.position.y;
        float diferenciaActualZ = posInicialMando.z - GetComponent<XRGrabInteractable>().interactorsSelecting[0].transform.position.z;

        //if (secondRotationTarget)
        //{
        //    transform.position = new Vector3(posRelativa.x - diferenciaActualX, posRelativa.y - diferenciaActualY, posRelativa.z - diferenciaActualZ);
            
        //}


        transform.position = new Vector3(posRelativa.x - diferenciaActualX, posRelativa.y - diferenciaActualY, posRelativa.z -diferenciaActualZ);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, posInicial.x-movimientoPermitidoX, posInicial.x+movimientoPermitidoX), Mathf.Clamp(transform.position.y, posInicial.y-movimientoPermitidoY,posInicial.y+ movimientoPermitidoY), Mathf.Clamp(transform.position.z, posInicial.z-movimientoPermitidoZ,posInicial.z+ movimientoPermitidoZ));
        
        posCorrecta = transform.position;
        //if (secondRotationTarget)
        //{
        //    transform.position = (posCorrecta * distanciaEntreReferencia) / Vector3.Distance(posCorrecta, secondRotationTarget.transform.position);
        //    posCorrecta = transform.position;
        //}

        transform.parent = origParent;
    }
    
    public void Soltar()
    {
        seleccionado = false;
        //StartCoroutine(CorreccionPos());
        CorreccionPos2();
    }

    IEnumerator CorreccionPos()
    {
        yield return new WaitForSeconds(.02f);
        Vector3 result = posCorrecta;
        if (movimientoPermitidoX == 0) { result = new Vector3(posInicial.x, result.y, result.z); }
        if (movimientoPermitidoY == 0) { result = new Vector3(result.x, posInicial.y, result.z); };
        if (movimientoPermitidoZ == 0) { result = new Vector3(result.x, result.y, posInicial.z); };

        
        transform.position = result;
        
    }

    void CorreccionPos2()
    {
        Vector3 result = posCorrecta;
        if (movimientoPermitidoX == 0) { result = new Vector3(posInicial.x, result.y, result.z); }
        if (movimientoPermitidoY == 0) { result = new Vector3(result.x, posInicial.y, result.z); };
        if (movimientoPermitidoZ == 0) { result = new Vector3(result.x, result.y, posInicial.z); };


        transform.position = result;

    }

    public void RestartInitPos()
    {
        posInicial = transform.position;
    }
}
