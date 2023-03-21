using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;

public class PartesTalla : MonoBehaviour
{
    HingeJoint hj;
    bool agarrando;
    public float rotMinima;
    public float rotMaxima;
    // Start is called before the first frame update
    void Start()
    {
        hj = GetComponent<HingeJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if(agarrando == true)
        {
            AgarrarJoint();
        }
        if(GetComponent<XRGrabInteractable>().enabled == false)
        {
            agarrando = false;
        }
    }


    public void AgarrarJoint()
    {
        agarrando = true;
        if(Mathf.Abs(transform.eulerAngles.x) - Mathf.Abs(rotMinima) < 20)
        {
            GetComponent<XRGrabInteractable>().enabled = false;
            transform.root.GetComponentInChildren<CajaTallas>().ComprobarTallas();
            //StartCoroutine(AjustarRotacion(transform.eulerAngles));
        }
    }
   
    public IEnumerator AjustarRotacion(Vector3 rotInicial)
    {
        float tiempo = 0;
        while(tiempo < 1)
        {
            transform.eulerAngles = Vector3.Lerp(rotInicial, new Vector3(rotMinima, transform.eulerAngles.y, transform.eulerAngles.z), tiempo / 1);
            yield return null;
        }
        GetComponentInParent<CajaTallas>().ComprobarTallas();
    }

}
