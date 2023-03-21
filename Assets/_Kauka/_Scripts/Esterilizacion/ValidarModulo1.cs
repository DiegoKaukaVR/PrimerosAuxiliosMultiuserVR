using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;

public class ValidarModulo1 : MonoBehaviour
{
    public Image img;
    public Image img2;

    public bool validarIman;
    public bool validarDosManos;
    public bool dosCondiciones;
    SlotsBandeja bandejaFunc;
    // Start is called before the first frame update
    void Start()
    {
        bandejaFunc = GetComponentInParent<SlotsBandeja>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ValidarPosicion()
    {
        if (dosCondiciones)
        {
            img2.color = Color.green;
            if (bandejaFunc)
            {
                bandejaFunc.Validar();
                GetComponent<XRGrabInteractable>().enabled = false;
            }
        }
        else
        {
            GetComponent<MeshRenderer>().enabled = false;
            img.color = Color.green;
        }
    }
    public void ValidarDosManos()
    {
        if(img.color == Color.green && GetComponent<ImanObjeto>().volviendoAPosicion)
        {
            img.color = Color.green;
        }
        else
        {
            if (GetComponent<CustomMultiInteractable>().twoHand)
            {
                img.color = Color.green;
            }
            else
            {
                img.color = Color.red;
            }
        }
        
    }
    public void FuncionObjetivo()
    {
        if (validarIman)
        {
            ValidarPosicion();
        }
        if (validarDosManos)
        {
            ValidarDosManos();
        }
    }

    public void FuncionAuxiliar()
    {
        if (dosCondiciones && GetComponent<ImanObjeto>().volviendoAPosicion)
        {
            ValidarPosicion();
        }
    }
}
