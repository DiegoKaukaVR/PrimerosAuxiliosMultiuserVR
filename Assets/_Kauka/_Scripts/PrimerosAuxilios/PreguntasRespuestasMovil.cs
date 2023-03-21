using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreguntasRespuestasMovil : PreguntasRespuestas
{
    /// <summary>
    /// 0. Audio Llamada: 
    /// 1. Audio: Digame
    /// 2. Respuesta usuario
    /// 3. Respuesta emergencias, pregunta:
    /// 4. Respuesta usuario
    /// </summary>
    /// 

    public PreguntasRespuestasPannel preguntasRespuestasPannel;


    protected override void Start()
    {
        base.Start();
        Invoke("LazyStart", 0.1f);
        
    }

    void LazyStart()
    {
        preguntasRespuestasPannel = PreguntasRespuestasManager.instance.GetComponent<PreguntasRespuestasPannel>();
    }
    protected override void NextQuestion()
    {
        base.NextQuestion();
    }

    [SerializeField] protected List<AudioSource> audiosLlamada;
    protected int indexAudio = 0;

    public void StartCall()
    {
        Debug.Log("Start Call");
        preguntasRespuestasPannel.AssignPreguntasRespuestas();
        StartPreguntasRespuestas();
        audiosLlamada[0].Play();
        audiosLlamada[1].PlayDelayed(audiosLlamada[0].clip.length);

        pannelConversation.animator.SetBool("open", true);
    }
    protected void ReproduceAudioCalling()
    {
        audiosLlamada[indexAudio].Play();
    }


    protected override void OnComplete()
    {
        base.OnComplete();
        pannelConversation.animator.SetBool("open", false);
        Invoke("LazyShutDown", 0.3f);
       
    }
    void LazyShutDown()
    {
        pannelConversation.gameObject.SetActive(false);
    }
}
