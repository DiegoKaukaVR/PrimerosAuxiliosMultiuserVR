using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;

public class ManivelaGrifo : MonoBehaviour
{
    public ParticleSystem aguaGrifo;
    public ParticleSystem aguaGrifo2;

    public bool conTemperatura;
    public bool frio;
    public bool calor;
    public bool sentidoContrario;

    public GestorAguaConTemperatura gat;

    public ManivelaGrifo manivelaAux;
    public float emissionActualAgua;

    public Quaternion rotInicial;
    Vector3 eulerinicial;
    Quaternion rotInicialMando;
    Vector3 eulerInicialMando;
    bool seleccionado;
    bool aux;
    // Start is called before the first frame update
    void Start()
    {
        var emissionRateAgua = aguaGrifo.emission;
        emissionRateAgua.rateOverTime = 0;
        var emissionRateAgua2 = aguaGrifo2.emission;
        emissionRateAgua2.rateOverTime = 0;
        rotInicial = transform.rotation;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (seleccionado)
        {
            ComprobarRotacionManivela();
        }
    }

    public void ComprobarRotacionManivela()
    {
        if(seleccionado == false)
        {
            rotInicialMando = GetComponent<XRGrabInteractable>().interactorsSelecting[0].transform.rotation;
            eulerInicialMando = GetComponent<XRGrabInteractable>().interactorsSelecting[0].transform.eulerAngles;
            eulerinicial = transform.localEulerAngles;
        }

        seleccionado = true;

        float rotAplicada = Quaternion.Angle(rotInicialMando, GetComponent<XRGrabInteractable>().interactorsSelecting[0].transform.rotation) / 1.7f;
        
        float direccionAplicada = (((eulerInicialMando.x - GetComponent<XRGrabInteractable>().interactorsSelecting[0].transform.eulerAngles.x) + 360f) % 360f) > 180f ? -1 : 1;
        if (sentidoContrario)
        {
            if (direccionAplicada == 1)
            {
                transform.rotation = Quaternion.Euler(eulerinicial.x - rotAplicada, -90, 90);
            }
            else
            {
                transform.rotation = Quaternion.Euler(eulerinicial.x + rotAplicada, -90, 90);
            }
        }
        else
        {
            if (direccionAplicada == 1)
            {
                transform.rotation = Quaternion.Euler(eulerinicial.x + rotAplicada, -90, 90);
            }
            else
            {
                transform.rotation = Quaternion.Euler(eulerinicial.x - rotAplicada, -90, 90);
            }
        }



        float rotFinalManivela = Quaternion.Angle(rotInicial, transform.rotation);

        
        if (rotFinalManivela >20)
        {
            emissionActualAgua = Mathf.Lerp(0, 50, rotFinalManivela / 100);
            if (manivelaAux)
            {
                var emissionRateAgua = aguaGrifo.emission;
                emissionRateAgua.rateOverTime = Mathf.Lerp(0 + manivelaAux.emissionActualAgua, 50 + manivelaAux.emissionActualAgua, rotFinalManivela / 100);

                var emissionRateAgua2 = aguaGrifo2.emission;
                emissionRateAgua2.rateOverTime = Mathf.Lerp(0, 20, rotFinalManivela / 100);
            }
            else
            {
                var emissionRateAgua = aguaGrifo.emission;
                emissionRateAgua.rateOverTime = Mathf.Lerp(0, 50, rotFinalManivela / 100);

                var emissionRateAgua2 = aguaGrifo2.emission;
                emissionRateAgua2.rateOverTime = Mathf.Lerp(0, 20, rotFinalManivela / 100);
            }
        }
        else
        {
            emissionActualAgua = 0;
            if (manivelaAux)
            {

                var emissionRateAgua = aguaGrifo.emission;
                emissionRateAgua.rateOverTime = 0 + manivelaAux.emissionActualAgua;

                var emissionRateAgua2 = aguaGrifo2.emission;
                emissionRateAgua2.rateOverTime = 0 + manivelaAux.emissionActualAgua;
            }
            else
            {

                var emissionRateAgua = aguaGrifo.emission;
                emissionRateAgua.rateOverTime = 0;

                var emissionRateAgua2 = aguaGrifo2.emission;
                emissionRateAgua2.rateOverTime = 0;
            }
        }

        if (conTemperatura)
        {
            if (gat!=null)
            {
                if (calor)
                {
                    if(rotFinalManivela > 20)
                    {
                        gat.cantidadCalor = (rotFinalManivela / 8);
                    }
                    else
                    {
                        gat.cantidadCalor = 0;
                    }
                }
                if(frio)
                {
                    if (rotFinalManivela > 20)
                    {
                        gat.cantidadFrio = (rotFinalManivela / 8);
                    }
                    else
                    {
                        gat.cantidadFrio = 0;
                    }
                }

               gat.CheckTemperatura();
            }
        }
    }
    public void SalirInteraccion()
    {
        seleccionado = false;
    }
    public void ResetearPosicion()
    {
        transform.rotation = rotInicial;
        var emissionRateAgua = aguaGrifo.emission;
        emissionRateAgua.rateOverTime = 0;

        var emissionRateAgua2 = aguaGrifo2.emission;
        emissionRateAgua2.rateOverTime = 0;
    }
    
}
   
