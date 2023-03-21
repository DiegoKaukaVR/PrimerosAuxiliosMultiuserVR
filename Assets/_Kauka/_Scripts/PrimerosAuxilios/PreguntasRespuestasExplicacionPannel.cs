using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PreguntasRespuestasExplicacionPannel : PreguntasRespuestasPannel
{
    public PreguntasRespuestasExplicacion preguntasRespuestasExplicacion;
    [SerializeField] public Button nextBNotton;
    public TextMeshProUGUI ExplicationTxt;
    public TextMeshProUGUI nextTxt;

    public GameObject ExplicationGO;
    public GameObject NextGO;

    public GameObject QuestionTitle;

    public override void SetStrings()
    {
        preguntasRespuestasExplicacion.SetStrings();
        ClearAllEvents();
        Debug.Log("AssignPreguntas");
        RegisterAnswers(preguntasRespuestasExplicacion);
    }

    protected override void ClearAllEvents()
    {
        base.ClearAllEvents();
        nextBNotton.onClick.RemoveAllListeners();
    }
    protected override void RegisterAnswers(PreguntasRespuestas preguntasRespuestas)
    {
        buttonA.onClick.AddListener(preguntasRespuestas.EvaluateAnswerA);
        buttonB.onClick.AddListener(preguntasRespuestas.EvaluateAnswerB);
        buttonC.onClick.AddListener(preguntasRespuestas.EvaluateAnswerC);
        nextBNotton.onClick.AddListener(preguntasRespuestas.Next);
    }
    /// <summary>
    /// Switch between true or false
    /// </summary>
    /// <param name="value"></param>
    public void ChangeToExplication(bool value)
    {
        buttonA.gameObject.SetActive(!value);
        buttonB.gameObject.SetActive(!value);
        buttonC.gameObject.SetActive(!value);
        QuestionTitle.SetActive(!value);

        ExplicationGO.SetActive(value);
        NextGO.SetActive(value);


    }

    private void OnValidate()
    {
        nextTxt = nextBNotton.GetComponentInChildren<TextMeshProUGUI>();
    }
}
