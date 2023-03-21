using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class PreguntasRespuestasExplicacionConversacion : PreguntasRespuestas
{
    public PreguntasRespuestasExplicacionPannel explicationPannel;
    public int indexExplications;
    public List<QuestionExplication> listExplications;

    protected override void Start()
    {
        base.Start();

        explicationPannel.ChangeToExplication(false);
        
    }

    [Serializable]
    public struct QuestionExplication
    {
        //[Multiline]
        public string Explication;
        [Multiline]
        public string ButtonText;
        public UnityEvent onExplication;
        public UnityEvent OnCorrectEvent;
    }

    
    public override void Explication()
    {
        base.Explication();
        SetStringsExplicacion();
        explicationPannel.SetPreguntasRespuestasPannel();
        explicationPannel.ChangeToExplication(true);

    }

    public override void Next()
    {
        next = true;
        CompleteQuestion();
        explicationPannel.ChangeToExplication(false);
        indexExplications++;
    }

    /// <summary>
    /// Prepare the text into the UI
    /// </summary>
    protected virtual void SetStringsExplicacion()
    {
        explicationPannel.nextBNotton.onClick.AddListener(Next);
        questionList[questionIndex].questionEvents.onQuestion.Invoke();
        explicationPannel.ExplicationTxt.text = listExplications[indexExplications].Explication;
        explicationPannel.nextTxt.text = listExplications[indexExplications].ButtonText;

    }


    

}
