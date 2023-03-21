using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIManagerPrimerosAuxilios : MonoBehaviour
{
    public UnityEvent OnStart;

    public GameObject[] llamadasGO;

    public void SetCanvasModule()
    {
        if (GameManager.instance.moduleIndex>5)
        {
            return;
        }
        SetCanvasModuleTrue(GameManager.instance.moduleIndex);
    }

    public void SetCanvasModuleTrue(int value)
    {
        llamadasGO[value].SetActive(true);
    }
    public void SetCanvasModuleFalse(int value)
    {
        llamadasGO[value].SetActive(false);
    }
    private void Start()
    {
        if (GameManager.instance.testing)
        {
            Invoke("LateStart", 0.2f);
        }
      
    }
    private void LateStart()
    {
        OnStart.Invoke();
    }

}
