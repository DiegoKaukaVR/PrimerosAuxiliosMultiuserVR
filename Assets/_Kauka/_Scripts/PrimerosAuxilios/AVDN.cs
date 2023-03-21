#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
// 1. Hablar a la persona
// 2. Tocar a la persona
// 2. Producir dolor
// 3. Comprobar que no responde
/// </summary>
/// 
public class AVDN : MonoBehaviour
{
    public GameObject tocarGO;
    public GameObject hurtGO;
    public bool talked;
    public bool hurt;

    AudioSource TocarSound;
    AudioSource MakeHurtSound;
    AudioSource TalkSound;

    public void Tocar()
    {
        GameManager.instance.audioManager.PlayComplete();
        //TocarSound.Play();
    }

    public void MakeHurt()
    {
        GameManager.instance.audioManager.PlayComplete();
        //MakeHurtSound.Play();
    }

    public void NoResponse()
    {
        GameManager.instance.audioManager.PlayComplete();
    }

    public void SetTouch(bool value)
    {
        tocarGO.SetActive(value);
    }

    public void SetHurt(bool value)
    {
        hurtGO.SetActive(value);
    }

}
#endif
