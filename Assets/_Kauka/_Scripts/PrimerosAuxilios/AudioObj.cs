using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioObj : MonoBehaviour
{
    AudioManager audioManager;
    AudioManager.AudioChannel audioChannel;

    [SerializeField]
    int channel = 0;



    private void Start()
    {
        audioManager = GameManager.instance.audioManager;
        AssignMixerChannel();
    }


    public void AssignMixerChannel()
    {
        audioChannel = audioManager.audioChannelList[channel];
    }


}
