using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SeleccionModuloPrimerosAuxilios : MonoBehaviour
{
    public GameObject[] moduleArray;
    public UnityEvent OnModuleSelected;

    UIManagerPrimerosAuxilios uiManager;
    PlayerStartPosition playerStartPosition;

    public GameObject pannelSeleccion;

    private void Awake()
    {
        uiManager = GetComponent<UIManagerPrimerosAuxilios>();
        playerStartPosition = GetComponent<PlayerStartPosition>();

       
    }

    private void Start()
    {
        if (GameManager.instance.testing)
        {
            pannelSeleccion.SetActive(false);
        }
    }

    int valueModule;
    public void SetModule(int value)
    {
        valueModule = value;
        GameManager.instance.moduleIndex = valueModule;
        OnModuleSelected.Invoke();
        uiManager.SetCanvasModule();
        Invoke("LateCallback", 1f);
    
    }

    void LateCallback()
    {
        moduleArray[valueModule].SetActive(true);
        playerStartPosition.TeleportPlayer(GameManager.instance.moduleIndex);
    }
}
