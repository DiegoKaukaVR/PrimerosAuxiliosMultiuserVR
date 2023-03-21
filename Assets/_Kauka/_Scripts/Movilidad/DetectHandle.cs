using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;

public class DetectHandle : MonoBehaviour
{
    XRDirectInteractor ccController;
    public bool derecha;
    bool checking;
    // Start is called before the first frame update
    void Start()
    {
        ccController = GetComponent<XRDirectInteractor>();
    }
    private void Update()
    {
        if(checking == true)
        {
            CheckInteraction();
        }
    }

    public void CheckInteraction()
    {
        checking = true;
        if (ccController.isSelectActive)
        {
            if (derecha)
            {
                GameControllerMovilidad.Instance.numeroManoDerecha = ccController.interactablesSelected[0].transform.gameObject.GetComponent<AgarreMovilidad>().numeroAgarre;
            }
            else
            {
                GameControllerMovilidad.Instance.numeroManoIzquierda = ccController.interactablesSelected[0].transform.gameObject.GetComponent<AgarreMovilidad>().numeroAgarre;
            }
            
        }
    }
    public void DeactivateInteraction()
    {
        checking = false;
        if (derecha)
        {
            GameControllerMovilidad.Instance.numeroManoDerecha = -1;
        }
        else
        {
            GameControllerMovilidad.Instance.numeroManoIzquierda = -1;
        }
    }
}
