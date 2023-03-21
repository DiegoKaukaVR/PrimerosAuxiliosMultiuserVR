using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;

public class TriggerHerramientaEspecificaDentista : MonoBehaviour
{

    public bool sonda;
    public bool espejo;
    public bool placadigital;


    public string herramientaActual;
    public int indiceActual;
    public bool colliderDisponible;

    public Transform posicionInstanciar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (sonda)
    //    {
    //        if(other.gameObject.tag == "SondaExploracion" && other.GetComponent<DoubleInteractor>().seleccionando == false)
    //        {
    //            //GetComponentInParent<GestorHerramientasMedico>().detectandoSonda = true;
    //            GameControllerOdontologia.Instance.sondaAcercada = true;
    //            if (GameControllerOdontologia.Instance.espejoAcercado)
    //            {
    //                GetComponentInParent<IntercambioDeMaterial>().DetectarAcercarHerramienta();
    //            }
    //            print("SondaCorrecta");
    //            Destroy(other.gameObject);
    //            if (!other.GetComponent<InteractableOneHand>().leftHand)
    //            {

    //            }

    //            sonda = false;
    //            GameControllerOdontologia.Instance.CheckConditions();
    //        }
    //    }
    //    if (espejo)
    //    {
    //        if (other.gameObject.tag == "EspejoIntraoral" && other.GetComponent<DoubleInteractor>().seleccionando == false)
    //        {
    //            //GetComponentInParent<GestorHerramientasMedico>().detectandoEspejo = true;
    //            GameControllerOdontologia.Instance.espejoAcercado = true;
    //            if (GameControllerOdontologia.Instance.sondaAcercada)
    //            {
    //                GetComponentInParent<IntercambioDeMaterial>().DetectarAcercarHerramienta();
    //            }
    //            print("EspejoCorrecto");
    //            Destroy(other.gameObject);
    //            if (!other.GetComponent<InteractableOneHand>().leftHand)
    //            {

    //            }
    //            espejo = false;
    //            GameControllerOdontologia.Instance.CheckConditions();
    //        }
    //    }
    //    if (colliderDisponible)
    //    {
    //        if (other.gameObject.tag == herramientaActual && other.GetComponent<DoubleInteractor>().seleccionando == false)
    //        {
    //            GameControllerOdontologia.Instance.cantidadCorrectaIntercambioMaterial--;
                
    //            GetComponentInParent<IntercambioDeMaterial>().DetectarAcercarHerramienta();
    //            print("herrrminet");
    //            Destroy(other.gameObject);
    //            if (!other.GetComponent<InteractableOneHand>().leftHand)
    //            {

    //            }
    //            herramientaActual = null;
    //            colliderDisponible = false;
    //            if (GameControllerOdontologia.Instance.cantidadCorrectaIntercambioMaterial <= 0)
    //            {
    //                GameControllerOdontologia.Instance.CheckConditions();
    //            }
    //        }
    //    }
    //}
    private void OnTriggerStay(Collider other)
    {
        if (sonda)
        {
            if (other.gameObject.tag == "SondaExploracion" && other.GetComponent<DoubleInteractor>().seleccionando == false)
            {
                //GetComponentInParent<GestorHerramientasMedico>().detectandoSonda = true;
                GameControllerOdontologia.Instance.sondaAcercada = true;
                if (GameControllerOdontologia.Instance.espejoAcercado)
                {
                    GetComponentInParent<IntercambioDeMaterial>().DetectarAcercarHerramienta();
                }
                print("SondaCorrecta");
                Destroy(other.gameObject);
                if (!other.GetComponent<InteractableOneHand>().leftHand)
                {

                }

                sonda = false;
                GameControllerOdontologia.Instance.CheckConditions();
            }
        }
        if (espejo)
        {
            if (other.gameObject.tag == "EspejoIntraoral" && other.GetComponent<DoubleInteractor>().seleccionando == false)
            {
                //GetComponentInParent<GestorHerramientasMedico>().detectandoEspejo = true;
                GameControllerOdontologia.Instance.espejoAcercado = true;
                if (GameControllerOdontologia.Instance.sondaAcercada)
                {
                    GetComponentInParent<IntercambioDeMaterial>().DetectarAcercarHerramienta();
                }
                print("EspejoCorrecto");
                Destroy(other.gameObject);
                if (!other.GetComponent<InteractableOneHand>().leftHand)
                {

                }
                espejo = false;
                GameControllerOdontologia.Instance.CheckConditions();
            }
        }
        if (colliderDisponible)
        {
            if (other.gameObject.tag == herramientaActual && other.GetComponent<DoubleInteractor>() &&  other.GetComponent<DoubleInteractor>().seleccionando == false)
            {
                GameControllerOdontologia.Instance.cantidadCorrectaIntercambioMaterial--;

                GetComponentInParent<IntercambioDeMaterial>().DetectarAcercarHerramienta();
                print("herrrminet");
                Destroy(other.gameObject);
                if (!other.GetComponent<InteractableOneHand>().leftHand)
                {

                }
                herramientaActual = null;
                colliderDisponible = false;
                if (GameControllerOdontologia.Instance.cantidadCorrectaIntercambioMaterial <= 0)
                {
                    GameControllerOdontologia.Instance.CheckConditions();
                }
            }
            if(other.gameObject.GetComponent<IndiceObjetos>() && other.gameObject.GetComponent<IndiceObjetos>().numeroObjeto == indiceActual && GameControllerOdontologia.Instance.modulo2)
            {
                if(other.gameObject.GetComponent<IndiceObjetos>().numeroObjeto == 0)
                {
                    GameControllerOdontologia.Instance.placaDigitalAcercada = true;
                    GameControllerOdontologia.Instance.CheckConditions();
                }
                if (other.gameObject.GetComponent<IndiceObjetos>().numeroObjeto == 1)
                {

                }
                if (other.gameObject.GetComponent<IndiceObjetos>().numeroObjeto == 2)
                {

                }
                if (other.gameObject.GetComponent<IndiceObjetos>().numeroObjeto == 3)
                {

                }
            }
        }
        //if (placadigital)
        //{
        //    if (other.gameObject.tag == "PlacaDigital")
        //    {
        //        //GetComponentInParent<GestorHerramientasMedico>().detectandoEspejo = true;
        //        GameControllerOdontologia.Instance.placaDigitalAcercada = true;
                
        //        print("PlacaDigital");
        //        Destroy(other.gameObject);
        //        if (!other.GetComponent<InteractableOneHand>().leftHand)
        //        {

        //        }
        //        espejo = false;
        //        GameControllerOdontologia.Instance.CheckConditions();
        //    }
        //}
    }
}
