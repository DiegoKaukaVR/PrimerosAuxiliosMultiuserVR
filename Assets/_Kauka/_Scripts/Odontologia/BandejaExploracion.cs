using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;

public class BandejaExploracion : MonoBehaviour
{
    public AnimationCurve curvaV;
    public GameObject bandejaObj;
    [Header("Herramientas")]
    public GameObject sondaExploracion;
    public GameObject espejoIntraoral;
    public GameObject pinza;

    public Transform[] huecosHerramienta;
    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(sondaExploracion, huecosHerramienta[0].transform);
        //Instantiate(espejoIntraoral, huecosHerramienta[1].transform);
        //Instantiate(pinza, huecosHerramienta[2].transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
    public void SoltarBandeja()
    {
        StartCoroutine(ComprobarPosicionBandeja());
    }

    IEnumerator ComprobarPosicionBandeja()
    {
        yield return new WaitForSeconds(.5f);
        if (bandejaObj.GetComponent<ImanObjeto>().volviendoAPosicion)
        {
            bandejaObj.GetComponent<XRGrabInteractable>().enabled = false;
            GameControllerOdontologia.Instance.instrumentalEnBandeja = true;
            GameControllerOdontologia.Instance.CheckConditions();
        }
    }
}
