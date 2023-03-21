using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;



public class PreguntasRespuestas : MonoBehaviour
{
    [SerializeField] protected PreguntasRespuestasPannel pannelConversation;

    [Serializable]
    public struct Question
    {
        public string question;

        public string answerA;
        public bool A;

        public string answerB;
        public bool B;

        public string answerC;
        public bool C;

        public QuestionEvents questionEvents;

        public bool hasExplication;
    }

    [Serializable]
    public struct QuestionEvents
    {
        public UnityEvent onQuestion;
        public UnityEvent OnCorrectEvent;
        public UnityEvent OnFailedEvent;
    }

    public List<Question> questionList;
    protected int questionIndex = 0;
    public UnityEvent onCompleteEvet;

    protected virtual void Start()
    {
        PreguntasRespuestasManager.instance.currentPreguntasRespuestas = this;
        SetStrings();   
    }
    public virtual void ResetLogic()
    {
        questionIndex = 0;
        correct = false;
    }
    protected virtual void StartPreguntasRespuestas()
    {
        PreguntasRespuestasManager.instance.currentPreguntasRespuestas = this;
        SetStrings();
    }

    /// <summary>
    /// Prepare the text into the UI
    /// </summary>
    public virtual void SetStrings()
    {
        questionList[questionIndex].questionEvents.onQuestion.Invoke();
        pannelConversation.questionTxt.text = questionList[questionIndex].question;
        pannelConversation.questionAnswerA.text = questionList[questionIndex].answerA;
        pannelConversation.questionAnswerB.text = questionList[questionIndex].answerB;
        pannelConversation.questionAnswerC.text = questionList[questionIndex].answerC;
    }

 


    #region EvaluateAnswer

    protected bool correct;

    public virtual bool CheckCorrectButton(int value)
    {
        if (questionIndex >= questionList.Count)
        {
            return false;
        }
        switch (value)
        {
            case 0:
                if (questionList[questionIndex].A)
                {
                    return true;
                }
                else
                {
                    return false;
                }
               
            case 1:
                if (questionList[questionIndex].B)
                {
                    return true;
                }
                else
                {
                    return false;
                }
          
            case 2:
                if (questionList[questionIndex].C)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            

            default:
                return false;
               
        }
        
    }

    /// <summary>
    /// Called from botton
    /// </summary>
    public virtual void EvaluateAnswerA()
    {
       
        if (questionIndex > questionList.Count)
            return;

        if (completed)
        {
            return;
        }

        if (questionList[questionIndex].A)
        {
            correct = true;
            //pannelConversation.colorBottonCollection[0].SetCompleteColor();

            Debug.Log("Correct Answer A");
        }
        else
        {
            //pannelConversation.colorBottonCollection[0].SetFailColor();
            Debug.Log("Respuesta incorrecta");
        }
        CompleteQuestion();
    }
    public virtual void EvaluateAnswerB()
    {
        if (completed)
        {
            return;
        }
       
        if (questionIndex > questionList.Count)
            return;

      
        if (questionList[questionIndex].B)
        {
            correct = true;
            //pannelConversation.colorBottonCollection[1].SetCompleteColor();
            Debug.Log("Correct Answer B");
        }
        else
        {
            //pannelConversation.colorBottonCollection[1].SetFailColor();
            Debug.Log("Respuesta incorrecta");
        }

        CompleteQuestion();
    }
    public virtual void EvaluateAnswerC()
    {
      
        if (questionIndex > questionList.Count)
            return;
        if (completed)
        {
            return;
        }

        if (questionList[questionIndex].C)
        {
            correct = true;
            //pannelConversation.colorBottonCollection[2].SetCompleteColor();
            Debug.Log("Correct Answer C");
        }
        else
        {
            //pannelConversation.colorBottonCollection[2].SetFailColor();
        }
           
       
        CompleteQuestion();
    }
    #endregion

    

    public virtual void SetStringExplication()
    {

    }

    public virtual void Explication()
    {

    }
    public bool next = false;
    public virtual void Next()
    {
        next = true;
        CompleteQuestion();
    }
    public virtual void CompleteQuestion()
    {
        if (next)
        {
            next = false;
            GameManager.instance.audioManager.PlayComplete();
            Debug.Log("NextQuestion");
            NextQuestion();
            return;
        }
        if (correct)
        {
            questionList[questionIndex].questionEvents.OnCorrectEvent.Invoke();
            GameManager.instance.audioManager.PlayComplete();
            Debug.Log("Question is completed as correct");
            if (questionList[questionIndex].hasExplication)
            {
                Explication();
                return;
            }
        }
        else
        {
            questionList[questionIndex].questionEvents.OnFailedEvent.Invoke();
            GameManager.instance.audioManager.PlayFail();
            Debug.Log("Question is completed as incorrect");



            if (!GameManager.instance.CheckEvaluative())
            {
                Debug.Log("En el modo formativo repites tu respuesta hasta acertar");
                return;
            }
        }
           
        NextQuestion();
    }

    bool completed;

    protected virtual void NextQuestion()
    {
        questionIndex++;
        correct = false;

        if (questionIndex >= questionList.Count)
        {
            completed = true;
            OnComplete();
            return;
        }

        pannelConversation.OnNextQuestion.Invoke();
        SetStrings();
    }

   
    protected virtual void OnComplete()
    {
        Debug.Log("Cuestionario finalizado");
        onCompleteEvet.Invoke();
        pannelConversation.GetComponent<SimplePannel>().ClosePannel();
    }

    
}
