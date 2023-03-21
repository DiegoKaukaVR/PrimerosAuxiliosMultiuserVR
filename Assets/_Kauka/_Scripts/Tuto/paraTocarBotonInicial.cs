using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class paraTocarBotonInicial : MonoBehaviour
{
    public GameObject panelElegirCasos;
    public void goTuto(bool b)
    {
        if (b) 
        { SceneManager.LoadScene(1); } 
        else 
        { 
          gameObject.SetActive(false); 
          panelElegirCasos.SetActive(true); 
          panelElegirCasos.GetComponent<Animator>().SetBool("open", true); 
          DatosImportantes.saltarTutorial = true; 
        }
    }
  
}
