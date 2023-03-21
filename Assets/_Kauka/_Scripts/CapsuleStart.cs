using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleStart : MonoBehaviour
{
    public GameObject[] modulesGO;

    private void Start()
    {
        StartCapsule();
    }

    void StartCapsule()
    {
        for (int i = 0; i < modulesGO.Length; i++)
        {
            modulesGO[i].SetActive(false);
        }
    }
}
