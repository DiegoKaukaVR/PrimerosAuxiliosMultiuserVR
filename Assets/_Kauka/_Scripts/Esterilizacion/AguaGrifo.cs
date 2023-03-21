using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AguaGrifo : MonoBehaviour
{
    public Slider sliderProgreso;
    public TMP_Text textoProgreso;
    public float cantidadSuma = 0.2f;
    public GestorAguaConTemperatura gestorAgua;

    List<ParticleCollisionEvent> collisionEvents;
    ParticleSystem part;
    public bool detergente;
    Vector3 posInicialPadre;

    float valorAguaManos = 100;
    float valorAguaManoActual = 0;


    // Start is called before the first frame update
    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
        posInicialPadre = transform.parent.transform.position;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Hand")
        {
            sliderProgreso.value += cantidadSuma;
            
            textoProgreso.text = Mathf.Round(sliderProgreso.value * 10.0f) * 0.1f + " %";
            valorAguaManoActual += cantidadSuma;
            if(valorAguaManoActual >= valorAguaManos)
            {
                if (FindObjectOfType<GameControllerEsterilizacion>())
                {
                    transform.parent.GetComponentInChildren<ManivelaGrifo>().ResetearPosicion();
                    GameControllerEsterilizacion.Instance.manosEnjuagadas = true;
                    GameControllerEsterilizacion.Instance.CheckConditions();
                }
                if (FindObjectOfType<GameControllerOdontologia>() && GameControllerOdontologia.Instance.modulo1)
                {
                    //transform.parent.GetComponentInChildren<ManivelaGrifo>().ResetearPosicion();
                    if(GameControllerOdontologia.Instance.stepActual == 1)
                    {
                        GameControllerOdontologia.Instance.manosEnjuagadas1 = true;
                    }
                    else if(GameControllerOdontologia.Instance.stepActual == 8)
                    {
                        GameControllerOdontologia.Instance.manosEnjuagadas2 = true;
                    }
                    else if (GameControllerOdontologia.Instance.stepActual == 23)
                    {
                        GameControllerOdontologia.Instance.manosEnjuagadas3 = true;
                    }
                    GameControllerOdontologia.Instance.CheckConditions();
                }
                if (FindObjectOfType<GameControllerOdontologia>() && GameControllerOdontologia.Instance.modulo2)
                {
                    //transform.parent.GetComponentInChildren<ManivelaGrifo>().ResetearPosicion();
                    if (GameControllerOdontologia.Instance.stepActual == 1)
                    {
                        GameControllerOdontologia.Instance.manosEnjuagadas1 = true;
                    }
                    else if (GameControllerOdontologia.Instance.stepActual == 8)
                    {
                        GameControllerOdontologia.Instance.manosEnjuagadas2 = true;
                    }
                    else if (GameControllerOdontologia.Instance.stepActual == 23)
                    {
                        GameControllerOdontologia.Instance.manosEnjuagadas3 = true;
                    }
                    GameControllerOdontologia.Instance.CheckConditions();
                }
            }
        }
        if(other.tag == "Fregadero" && !detergente && gestorAgua!=null)
        {
            gestorAgua.SumarAgua();
        }
        if (other.tag == "Fregadero" && detergente)
        {
            sliderProgreso.value += cantidadSuma;
            textoProgreso.text = sliderProgreso.value + " %";
            if(sliderProgreso.value >= sliderProgreso.maxValue)
            {
                GameControllerEsterilizacion.Instance.detergenteVertido = true;
                GameControllerEsterilizacion.Instance.CheckConditions();
                transform.parent.transform.position = posInicialPadre;
            }
        }
        if (other.tag == "HerramientaEsterilizacion")
        {
            if (other.GetComponent<ParteSuciaHerramienta>())
            {
               
                if (!other.GetComponent<ParteSuciaHerramienta>().limpio && this.gameObject.tag == other.GetComponent<ParteSuciaHerramienta>().tagHerramientaDeLimpieza)
                {
                    other.GetComponent<ParteSuciaHerramienta>().LimpiezaManual(collisionEvents[0].intersection);
                }
                else if (other.GetComponent<ParteSuciaHerramienta>().limpio && !other.GetComponent<ParteSuciaHerramienta>().enjuagado && this.gameObject.tag == "Water" && GameControllerEsterilizacion.Instance.modoFormativo)
                {
                    other.GetComponent<ParteSuciaHerramienta>().Enjuagar(collisionEvents[0].intersection);
                }
                else if (!other.GetComponent<ParteSuciaHerramienta>().enjuagado && this.gameObject.tag == "Water" && GameControllerEsterilizacion.Instance.modoEvaluativo)
                {
                    other.GetComponent<ParteSuciaHerramienta>().Enjuagar(collisionEvents[0].intersection);
                }
                else if(other.GetComponent<ParteSuciaHerramienta>().limpio && other.GetComponent<ParteSuciaHerramienta>().enjuagado && !other.GetComponent<ParteSuciaHerramienta>().secado 
                    && this.gameObject.tag == other.GetComponent<ParteSuciaHerramienta>().tagHerramientaSecado && GameControllerEsterilizacion.Instance.modoFormativo)
                {
                    other.GetComponent<ParteSuciaHerramienta>().Secar(collisionEvents[0].intersection);
                }
                else if (!other.GetComponent<ParteSuciaHerramienta>().secado && this.gameObject.tag == other.GetComponent<ParteSuciaHerramienta>().tagHerramientaSecado && GameControllerEsterilizacion.Instance.modoEvaluativo)
                {
                    other.GetComponent<ParteSuciaHerramienta>().Secar(collisionEvents[0].intersection);
                }
            }
        }
    }
}
