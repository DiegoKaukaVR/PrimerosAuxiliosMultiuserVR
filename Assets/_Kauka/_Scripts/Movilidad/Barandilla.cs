using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;
using UnityEngine.Events;
public class Barandilla : MonoBehaviour
{
    bool seleccionado;
    public float rotMaxima;
    Transform parent;
    public UnityEvent funcionAlSoltar;
    Vector3 posInicial;
    // Start is called before the first frame update
    void Start()
    {
        posInicial = transform.position;
        parent = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        if (seleccionado)
        {
            Coger();
        }
    }
    public void Coger()
    {
        seleccionado = true;
        transform.parent = parent;
        //if (Mathf.Abs(Mathf.Abs(transform.localEulerAngles.z) - Mathf.Abs(rotMaxima)) < 40)
        //{
        //    transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, rotMaxima);
        //    GetComponent<XRGrabInteractable>().enabled = false;
        //    funcionAlSoltar.Invoke();
        //}


        if (transform.position.y  - (posInicial.y + GetComponent<LimitInteractableMovement>().movimientoPermitidoY)<0.02f)
        {

            funcionAlSoltar.Invoke();
            
        }
    }
    public void Soltar()
    {
        seleccionado = false;
    }
}
