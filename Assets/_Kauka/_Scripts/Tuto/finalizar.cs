using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class finalizar : MonoBehaviour
{
    public Animator fadeAnim;
    public Material matManos;
    public Texture manosTexture;

    [Tooltip("--- Escena a abrirse en int ---")]
    public int openSceneInt = 0;

    private void Start()
    {
        matManos.mainTexture = manosTexture;
    }
    public void cerrateAplicacio()
    {
        fadeAnim.SetBool("fade", true);
        StartCoroutine(endTuto());
    }
    IEnumerator endTuto()
    {
        yield return new WaitForSeconds(1f);
        FinishTutorial();
        //Application.Quit();
        matManos.mainTexture = manosTexture;
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(openSceneInt);
    }

    public void FinishTutorial()
    {
        DatosImportantes.saltarTutorial = true;
    }
}
