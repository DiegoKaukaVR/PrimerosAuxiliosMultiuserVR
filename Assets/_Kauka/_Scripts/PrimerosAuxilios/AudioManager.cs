using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Notas de Desarrollo: 
/// 1. Integrar con el AudioMixer de Unity
/// 2. Poder controlar los efectos de Audio en canales
/// 3. Mutear / Desactivar Sonido
/// </summary>
public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource audioComplete;
    [SerializeField] AudioSource audioFail;
    [SerializeField] AudioSource insuflaccion;
    [SerializeField] AudioSource hit;
    [SerializeField] AudioSource finshedExperience;
    [SerializeField] AudioSource selectedUISound;
    [SerializeField] AudioSource presshedUISound;


    [Header(" Audio Mixer")]
    public List<AudioChannel> audioChannelList;

    public float[] ArrayMixerChannel = new float[5];

    [Serializable]
    public struct AudioChannel
    {
        public string name;
        [Range(0, 100)]
        public float volumen;
    }

 
    [SerializeField, Range(0, 100)]
    float masterAudioLevel = 75;

    [SerializeField, Range(0, 100)]
    float musicAudioLevel = 75;

    [SerializeField, Range(0, 100)]
    float sfxAudioLevel = 75;

    private void Start()
    {
        if (audioChannelList.Count == 0)
        {
            SetDefaultChannels();
        }
        
    }

    private void OnValidate()
    {
        if (audioChannelList.Count <3 )
        {
            SetDefaultChannels();
        }
    }


    void SetDefaultChannels()
    {
        AudioChannel MasterChannel = new AudioChannel { name = "Master", volumen = masterAudioLevel };
        AddChannelToList(MasterChannel);
        ArrayMixerChannel[0] = masterAudioLevel;

        AudioChannel MusicChannel = new AudioChannel { name = "Music", volumen = musicAudioLevel };
        AddChannelToList(MusicChannel);
        ArrayMixerChannel[1] = musicAudioLevel;

        AudioChannel SFXChannel = new AudioChannel { name = "SFX", volumen = sfxAudioLevel };
        AddChannelToList(SFXChannel);
        ArrayMixerChannel[2] = sfxAudioLevel;
    }

    void AddChannelToList(AudioChannel newAudioChannel)
    {
        if (audioChannelList.Count != 0)
        {
            for (int i = 0; i < audioChannelList.Count; i++)
            {
                if (audioChannelList[i].name == newAudioChannel.name)
                {
                    Debug.LogWarning("AudioChannel " + newAudioChannel.name + " ha sido eliminado de la lista porque ya está en ella");
                    return;
                }
            }
        }
       

        audioChannelList.Add(newAudioChannel);
    }

 

    public void ChangeMasterLevel(float newVolumen)
    {
        masterAudioLevel = newVolumen;
    }

    public void ChangeMusicLevel(float newVolumen)
    {
        musicAudioLevel = newVolumen;
    }

    public void ChangeSFXLevel(float newVolumen)
    {
        sfxAudioLevel = newVolumen;
    }





    public void PlayComplete()
    {
        audioComplete.Play();
    }

    public void PlayFail()
    {
        audioFail.Play();
    }

    public void PlayInsuflaccion()
    {
        insuflaccion.Play();
    }

    public void PlayHit()
    {
        hit.Play();
    }
    public void PlayFinishExperience()
    {
        audioComplete.Stop();
        finshedExperience.Play();
    }

    public void PlaySelectedUI()
    {
        selectedUISound.Stop();
        selectedUISound.Play();
    }
    public void PlayPreshedUI()
    {
        presshedUISound.Play();
    }
}
