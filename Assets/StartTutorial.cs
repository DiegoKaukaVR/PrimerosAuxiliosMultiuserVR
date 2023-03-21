using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTutorial : MonoBehaviour
{
    public GameObject tutorial;
    public GameObject seleccionPannel;

    private void Awake()
    {
        if (DatosImportantes.saltarTutorial)
        {
            tutorial.SetActive(false);
            seleccionPannel.SetActive(true);
        }
        else
        {
            tutorial.SetActive(true);
            seleccionPannel.SetActive(false);
        }
    }
}
