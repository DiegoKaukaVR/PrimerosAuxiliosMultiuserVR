using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Poner en la oreja,
/// llamar a emergencias
/// elegir respuestas con el otro mando en UI (Preguntas y respuestas)
/// </summary>
public class MovilInteractable : EventGrabInteractable
{
    [Tooltip("Trigger que detecta si el movil está en la oreja o no, para su activación y desactivación")]
    [SerializeField] GameObject HearTrigger;

    [Tooltip("Interfaz de la llamada")]
    [SerializeField] GameObject UICall;

    PreguntasRespuestas preguntasRespuestasLlamada;


    private void Start()
    {
        preguntasRespuestasLlamada = GetComponent<PreguntasRespuestas>();
    }

    public void SetHearTrigger(bool value)
    {
        HearTrigger.SetActive(value);
    }

    public void SetUICall(bool value)
    {
        UICall.SetActive(value);
    }
}
