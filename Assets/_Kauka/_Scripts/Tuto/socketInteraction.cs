using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class socketInteraction : MonoBehaviour
{
    bool ocupado;
    public socketInteraction otroSocket;
    public scr_StepController stp_Ctrl;
    public void socketOcupado(bool b)
    {
        ocupado = b;
    }

    public void stepSiguiente()
    {
        GetComponent<XRSocketInteractor>().selectTarget.GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<MeshRenderer>().enabled = false;
        if (ocupado && otroSocket.ocupado && DatosImportantes.socketInt == 0)
        {
            stp_Ctrl.NextStep();
            DatosImportantes.socketInt++;
        }
    }

    public void rigiddesbodiear()
    {
        GetComponent<XRSocketInteractor>().selectTarget.GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<MeshRenderer>().enabled = true;
    }
}
