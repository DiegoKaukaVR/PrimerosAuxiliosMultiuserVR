using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class MedidoresJabon : MonoBehaviour
{
    public int numPasos;
    public Slider sliderProgreso;
    public TMP_Text textoProgreso;
    public GameObject bubujasVFX;
    public GameObject gotaPadre;
    bool derecha;
    bool izquierda;
    bool arriba;
    bool abajo;
    public UnityEvent onComplete;
    // Start is called before the first frame update
    private void Start()
    {
        derecha = true;
        izquierda = true;
        abajo = true;
        arriba = true;
    }
    public void ComprobarLimites(Vector3 posInicial, float movimientoPermitido)
    {
        if ((transform.localPosition.x > posInicial.x + ((movimientoPermitido * 8) / 10)) && derecha)
        {
            GetComponent<MedidoresJabon>().SumarMedidor();
            derecha = false;
            izquierda = true;
        }
        if ((transform.localPosition.x < posInicial.x - ((movimientoPermitido * 8) / 10)) && izquierda)
        {
            GetComponent<MedidoresJabon>().SumarMedidor();
            derecha = true;
            izquierda = false;
        }
        if ((transform.localPosition.z > posInicial.z + ((movimientoPermitido * 8) / 10)) && arriba)
        {
            GetComponent<MedidoresJabon>().SumarMedidor();
            arriba = false;
            abajo = true;
        }
        if ((transform.localPosition.z < posInicial.z - ((movimientoPermitido * 8) / 10)) && abajo)
        {
            GetComponent<MedidoresJabon>().SumarMedidor();
            arriba = true;
            abajo = false;
        }
    }
    public void SumarMedidor()
    {
        Instantiate(bubujasVFX, transform.position, Quaternion.identity, null);
        sliderProgreso.value = sliderProgreso.value + (100f / numPasos);
        textoProgreso.text = sliderProgreso.value + " %"; 

        if(sliderProgreso.value >= sliderProgreso.maxValue )
        {
            if (FindObjectOfType<GameControllerEsterilizacion>())
            {
                GameControllerEsterilizacion.Instance.manosLavadas = true;
                GameControllerEsterilizacion.Instance.CheckConditions();
            }
            if (FindObjectOfType<GameControllerOdontologia>() && GameControllerOdontologia.Instance.modulo1)
            {
                if (GameControllerOdontologia.Instance.stepActual == 0)
                {
                    GameControllerOdontologia.Instance.manosLavadas1 = true;
                }
                else if (GameControllerOdontologia.Instance.stepActual == 7)
                {
                    GameControllerOdontologia.Instance.manosLavadas2 = true;
                }
                else if (GameControllerOdontologia.Instance.stepActual == 21)
                {
                    GameControllerOdontologia.Instance.manosLavadas3 = true;
                }
                GameControllerOdontologia.Instance.CheckConditions();
            }
            if (FindObjectOfType<GameControllerOdontologia>() && GameControllerOdontologia.Instance.modulo2)
            {
                if (GameControllerOdontologia.Instance.stepActual == 0)
                {
                    GameControllerOdontologia.Instance.manosLavadas1 = true;
                }
                else if (GameControllerOdontologia.Instance.stepActual == 7)
                {
                    GameControllerOdontologia.Instance.manosLavadas2 = true;
                }
                else if (GameControllerOdontologia.Instance.stepActual == 21)
                {
                    GameControllerOdontologia.Instance.manosLavadas3 = true;
                }
                GameControllerOdontologia.Instance.CheckConditions();
            }
            onComplete.Invoke();

            Destroy(gotaPadre);
        }
    }
   
}
