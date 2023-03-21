using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// DESA device
/// </summary>
public class AudioMachine : MonoBehaviour
{
    [SerializeField] GrabCollection grabCollection;
    [SerializeField] List<AudioSource> audioList;
    [SerializeField] BottonBehavior[] bottonCollection;

    public states currentState;
    public enum states
    {
        off,
        start,
        parches,
        rcp,
        descarga
    }

    [Header("Analysis")]
    [SerializeField] float timeAnalysis;
    [SerializeField] bool recommend;


    [SerializeField] BottonBehavior descargaBotton;
    [Header("Debug")]
    [SerializeField] GameObject sphereView;
    [SerializeField] Material greenMat;
    [SerializeField] Material redMat;

    [Header("RCP")]
    [SerializeField] GameObject RCPGo;


    private void Start()
    {
        currentState = states.off;
        grabCollection = this.transform.parent.GetComponentInChildren<GrabCollection>();
        grabCollection.enabled = false;
        //grabCollection.gameObject.SetActive(false);
        bottonCollection = GetComponentsInChildren<BottonBehavior>();
    }

    /// <summary>
    /// Called from steps
    /// </summary>
    public void StartLogic()
    {
        for (int i = 0; i < bottonCollection.Length; i++)
        {
            bottonCollection[i].ActivateBotton();
        }
    }

    public void ChangeRecommend()
    {
        recommend = true;
    }
   

    /// <summary>
    /// Start All logic
    /// </summary>
    public void TurnOnMachine()
    {
        Debug.Log("Machine start");
        PlayAudioSource(0);
        ChangeState(states.start);
        OnStartMachineEvent.Invoke();
       
    }

    public UnityEvent OnStartMachineEvent;
    public UnityEvent ParchesEvent;
    /// <summary>
    /// 
    /// </summary>
    public void SetPatchesInBody()
    {
        ChangeState(states.parches);
        grabCollection.enabled = true;
        grabCollection.gameObject.SetActive(true);
        grabCollection.SetAvaibleInteraction();
        ParchesEvent.Invoke();
    }


    public void Analysis()
    {
        PlayAudioSource(1);
        StartCoroutine(FinishAnalysis());
    }

    IEnumerator FinishAnalysis()
    {
        yield return new WaitForSecondsRealtime(timeAnalysis);

        if (!recommend)
        {
            NoRecomend();
        }
        else
        {
            Recommend();
        }
    }

    IEnumerator PlayAudioAfterTime(float waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        RCP();
    }

    public UnityEvent OnRCP;

    public void NoRecomend()
    {
        PlayAudioSource(2);
        
        StartCoroutine(PlayAudioAfterTime(4f));
    }

    public float timeToExecuteDescarga;
    public void Recommend()
    {

        StartCoroutine(CoroutineDescarga());
        PlayAudioSource(3);
    }

    IEnumerator CoroutineDescarga()
    {
        yield return new WaitForSecondsRealtime(timeToExecuteDescarga);
        Descarga();
    }

    public UnityEvent OnDescargaElectrica;
    public UnityEvent OnDescargafinish;
    public void Descarga()
    {
        PlayAudioSource(4);
        audioList[6].PlayDelayed(0.5f);
        ChangeState(states.descarga);
        OnDescargaElectrica.Invoke();
        Invoke("OnFinish", 2.5f);
    }

    void OnFinish()
    {
        StopAllAudios();
        OnDescargafinish.Invoke();
       
    }
    public void RCP()
    {
        grabCollection.gameObject.SetActive(false);
        PlayAudioSource(5);
        ChangeState(states.rcp);
        OnRCP.Invoke();
    }


    public void ChangeState(states nextState)
    {
        CheckState();
        currentState = nextState;
    }
    public void PlayAudioSource(int indexAudio)
    {
        StopAllAudios();
        audioList[indexAudio].Play();
    }

    public void StopAllAudios()
    {
        for (int i = 0; i < audioList.Count; i++)
        {
            audioList[i].Stop();
        }
    }


    //Debug

    void CheckState()
    {
        switch (currentState)
        {
            case states.start:
                ChangeSphereColorToGreen();
                break;
            case states.descarga:
                ChangeSphereColorToRed();
                break;
            default:
                break;
        }
    }

    public void ChangeSphereColorToGreen()
    {
        sphereView.GetComponent<MeshRenderer>().material = greenMat;
    }

    public void ChangeSphereColorToRed()
    {
        sphereView.GetComponent<MeshRenderer>().material = redMat;
    }
}
