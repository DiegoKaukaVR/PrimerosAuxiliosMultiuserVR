using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class SeleccionModuloComunicacion : MonoBehaviour
{
    public GameObject[] moduleArray;
    public UnityEvent OnModuleSelected;
    public UIManagerComunicacion uiManager;
    PlayerStartPosition playerStartPosition;

    int valueModule;

    private void Start()
    {
        uiManager = GetComponent<UIManagerComunicacion>();
        playerStartPosition = GetComponent<PlayerStartPosition>();
    }
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
