using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GestorAguaConTemperatura : MonoBehaviour
{
    public TMP_Text temperaturaTxt;
    public float temperaturaIdealMinima;
    public float temperaturaIdealMaxima;
    public float temperaturaBase;
    [Space(20)]
    public float temperaturaActual;
    public float cantidadFrio;
    public float cantidadCalor;
    [Space(20)]
    public float temperaturaIdeal;

    [Header("AguaVisual")]
    public GameObject agua;
    public int stepsAgua;
    int stepsActual;
    public Vector3 posicionAgua1;
    public Vector3 posicionAgua2;

    // Start is called before the first frame update
    void Start()
    {
        temperaturaActual = temperaturaBase;
        CheckTemperatura();
        stepsActual = 0;
    }

    public void CheckTemperatura()
    {
        temperaturaActual = temperaturaBase + cantidadCalor - cantidadFrio;
        temperaturaActual = Mathf.Round(temperaturaActual * 10.0f) * 0.1f;
        temperaturaTxt.text = temperaturaActual.ToString() + " º";
        temperaturaTxt.color = Color.Lerp(Color.green, Color.red, Mathf.Abs(temperaturaActual-temperaturaIdeal)/4);
        //if (agua.GetComponent<GestorAguaConTemperatura>().temperaturaActual < agua.GetComponent<GestorAguaConTemperatura>().temperaturaIdealMaxima
        //        && agua.GetComponent<GestorAguaConTemperatura>().temperaturaActual < agua.GetComponent<GestorAguaConTemperatura>().temperaturaIdealMinima)
        //{
        //    GameControllerEsterilizacion.Instance.temperaturaCorrectaAgua = true;
        //    GameControllerEsterilizacion.Instance.CheckConditions();
        //}
    }

    public void SumarAgua()
    {
        stepsActual++;
        if(stepsActual < stepsAgua)
        {
            agua.transform.localPosition = Vector3.Lerp(posicionAgua1, posicionAgua2, (float)stepsActual/(float)stepsAgua);
            print( stepsActual / stepsAgua);
        }
        else
        {
            if(temperaturaActual < temperaturaIdealMaxima 
                && temperaturaActual > temperaturaIdealMinima)
            {
                GameControllerEsterilizacion.Instance.temperaturaCorrectaAgua = true;
                GameControllerEsterilizacion.Instance.CheckConditions();
            }
        }
    }
}
