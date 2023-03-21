using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PreguntasRespuestasPannel : MonoBehaviour
{
    public Animator animator;
    [SerializeField] protected PreguntasRespuestas preguntasRespuestas;
    [SerializeField] protected Button buttonA;
    [SerializeField] protected Button buttonB;
    [SerializeField] protected Button buttonC;

    [SerializeField] public TextMeshProUGUI questionTxt;
    [SerializeField] public TextMeshProUGUI questionAnswerA;
    [SerializeField] public TextMeshProUGUI questionAnswerB;
    [SerializeField] public TextMeshProUGUI questionAnswerC;

    [Header("Color bend")]
    public ColorBotton[] colorBottonCollection;


    public UnityEvent OnNextQuestion;

    protected virtual void Awake()
    {
        animator=GetComponent<Animator>();
        colorBottonCollection = GetComponentsInChildren<ColorBotton>();

        for (int i = 0; i < colorBottonCollection.Length; i++)
        {
            colorBottonCollection[i].preguntasRespuetas = preguntasRespuestas;
            OnNextQuestion.AddListener(colorBottonCollection[i].SetOriginalColor);
        }

        SetPreguntasRespuestasPannel();
    }

    public virtual void SetPreguntasRespuestasPannel()
    {
        //preguntasRespuestas = PreguntasRespuestasManager.instance?.currentPreguntasRespuestas;

        if (preguntasRespuestas != null)
        {
            AssignPreguntasRespuestas();
        }
        else
        {
            Debug.LogError("Deja apagadas los paneles de Llamadas telefonicas o mete preguntas respuestas ");
        }
    }
    public virtual void AssignPreguntasRespuestas()
    {
        preguntasRespuestas = PreguntasRespuestasManager.instance.currentPreguntasRespuestas;
        ClearAllEvents();
        Debug.Log("AssignPreguntas");
        RegisterAnswers(preguntasRespuestas);
    }

    protected virtual void RegisterAnswers(PreguntasRespuestas preguntasRespuestas)
    {
        buttonA.onClick.AddListener(preguntasRespuestas.EvaluateAnswerA);
        //buttonA.onClick.AddListener(colorBottonCollection[0].);

        buttonB.onClick.AddListener(preguntasRespuestas.EvaluateAnswerB);
        buttonC.onClick.AddListener(preguntasRespuestas.EvaluateAnswerC);
    }

    protected virtual void ClearAllEvents()
    {
        buttonA.onClick.RemoveAllListeners();
        buttonB.onClick.RemoveAllListeners();
        buttonC.onClick.RemoveAllListeners();
    }


    public virtual void SetStrings() { }
}
