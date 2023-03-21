using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioConversation : MonoBehaviour
{
    public AudioSource[] audios;

    public void PlayAudioSource(int index)
    {
        for (int i = 0; i < audios.Length; i++)
        {
            audios[i].Stop();
        }

        audios[index].Play();
    }
}
