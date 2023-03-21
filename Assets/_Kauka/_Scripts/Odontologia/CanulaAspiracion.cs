using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanulaAspiracion : MonoBehaviour
{
    bool sujetandoCanula;
    public bool canulaEnPosicion;
    public bool canulaCorrecta;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (sujetandoCanula)
        {
            CheckCanula();
        }
    }
    public void CheckCanula()
    {
        sujetandoCanula = true;
        if (GetComponent<ImanObjeto>().volviendoAPosicion)
        {
            canulaEnPosicion = true;
            GetComponent<ImanObjeto>().enabled = false;
            GetComponent<InteractableOneHand>().trackRotation = false;
            GetComponent<InteractableOneHand>().trackPosition = false;
           
        }
        if(canulaEnPosicion && GetComponent<InteractableOneHand>().rightHand)
        {
            canulaCorrecta = true;
        }
        else
        {
            canulaCorrecta = false;
        }
    }
    public void SoltarCanula()
    {
        sujetandoCanula = false;
        if (GetComponent<ImanObjeto>().volviendoAPosicion)
        {
            canulaEnPosicion = true;
            GetComponent<ImanObjeto>().enabled = false;
            GetComponent<InteractableOneHand>().trackRotation = false;
            GetComponent<InteractableOneHand>().trackPosition = false;
            GameControllerOdontologia.Instance.canulaAspiracionSujeta = true;
            GameControllerOdontologia.Instance.CheckConditions();
        }
    }
}
