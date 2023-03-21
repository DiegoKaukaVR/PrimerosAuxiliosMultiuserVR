using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerComunicacion : MonoBehaviour
{

    public GameObject[] conversacionesGO;

  

    private void Start()
    {
        if (GameManager.instance.testing)
        {
            ManualStart();
        }
      
    }

    void ManualStart()
    {
        for (int i = 0; i < conversacionesGO.Length; i++)
        {
            conversacionesGO[i].SetActive(false);
        }

        conversacionesGO[GameManager.instance.moduleIndex].SetActive(true);
    }

    public void SetCanvasModule()
    {
        for (int i = 0; i < conversacionesGO.Length; i++)
        {
            conversacionesGO[i].SetActive(false);
        }

        SetCanvasModuleTrue(GameManager.instance.moduleIndex);
    }

    public void SetCanvasModuleTrue(int value)
    {
        conversacionesGO[value].SetActive(true);
    }
}
