using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;

public class SillonPaciente : MonoBehaviour
{
    public GameObject respaldo;
    public float rotMaximaRespaldo;
    float rotInicialRespaldo;
    bool cogiendoRespaldo;
    bool respaldoColocado;
    // Start is called before the first frame update
    void Start()
    {
        rotInicialRespaldo = respaldo.transform.eulerAngles.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (cogiendoRespaldo)
        {
            ComprobarRespaldo();
        }
    }
    public void ComprobarRespaldo()
    {
        cogiendoRespaldo = true;

        if (Mathf.Abs(Mathf.Abs(respaldo.transform.eulerAngles.x-rotInicialRespaldo) - Mathf.Abs(rotMaximaRespaldo)) < 5)
        {
            respaldo.transform.eulerAngles = new Vector3(rotInicialRespaldo- rotMaximaRespaldo, respaldo.transform.eulerAngles.y, respaldo.transform.eulerAngles.z);
            respaldo.GetComponent<XRGrabInteractable>().enabled = false;
            respaldo.GetComponent<Rigidbody>().useGravity = false;
            respaldoColocado = true;
            GameControllerOdontologia.Instance.sillonSupino = true;
            GameControllerOdontologia.Instance.CheckConditions();
        }
    }
    public void SoltarRespaldo()
    {
        cogiendoRespaldo = false;
    }
}
