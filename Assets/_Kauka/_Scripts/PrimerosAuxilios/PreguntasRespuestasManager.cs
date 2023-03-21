using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PreguntasRespuestasManager : MonoBehaviour
{
    public static PreguntasRespuestasManager instance;
    public PreguntasRespuestas currentPreguntasRespuestas;

    protected void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Overritten Singleton");
            instance = this;
        }
        else
        {
            instance = this;
        }
    }


}
