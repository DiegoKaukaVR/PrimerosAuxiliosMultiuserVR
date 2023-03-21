using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;

public class Autoclave : MonoBehaviour
{
    public TMP_Text textoModo;
    public TMP_Text textoTemperatura;
    public TMP_Text textoPresion;
    [Space(10)]
    public string[] modosDisponibles;
    public int modoCorrecto;
    [Space(10)]
    public bool puedeIniciar;
    public float segundosError = 2;
    [Space(10)]
    public GameObject tapa;
    public float rotMaximaTapa;
    public float rotMinimaTapa;
    int modo = 0;
    bool cogiendoTapa;
    public GameObject controladorQuimico;
    // Start is called before the first frame update
    void Start()
    {
        //modo = modosDisponibles.Length;
        textoModo.text = "Programa: \n " + modosDisponibles[modo];
        textoTemperatura.text = "Ta: 60 grados";
        textoPresion.text = "Presión: 36 bar";
    }
    private void Update()
    {
        if (cogiendoTapa)
        {
            ComprobarTapa();
        }
    }

    public void CambiarModo()
    {
        if(modo < modosDisponibles.Length)
        {
            
            textoModo.text = "Programa: \n " + modosDisponibles[modo];
            modo++;
        }
        else
        {
            modo = 0;
            textoModo.text = "Programa: \n " + modosDisponibles[modo];
        }
    }
    public void ActivarAutoclave()
    {
        if (GameControllerEsterilizacion.Instance.modoFormativo)
        {
            if (puedeIniciar)
            {
                if (modo == modoCorrecto)
                {
                    textoModo.text = "Iniciando programa: \n " + modosDisponibles[modo];
                    print("Modo correcto");
                    controladorQuimico.GetComponent<ControlesQuimicos>().CambiarEstado();
                    GameControllerEsterilizacion.Instance.programaCorrectoAutoclave = true;
                    GameControllerEsterilizacion.Instance.CheckConditions();
                    
                }
                else
                {
                    textoModo.text = "Iniciando programa incorrecto: \n " + modosDisponibles[modo];
                    print("Modo incorrecto");
                }
            }
            else
            {
                StartCoroutine(MensajeError());
            }
        }
        if (GameControllerEsterilizacion.Instance.modoEvaluativo)
        {
            if (modo == modoCorrecto)
            {
                GameControllerEsterilizacion.Instance.programaCorrectoAutoclave = true;
            }
            else
            {
                GameControllerEsterilizacion.Instance.programaCorrectoAutoclave = false;
            }
            GameControllerEsterilizacion.Instance.CheckConditions();
            textoModo.text = "Iniciando programa: \n " + modosDisponibles[modo];
            controladorQuimico.GetComponent<ControlesQuimicos>().CambiarEstado();
            StartCoroutine(ProgramaFinalizado());
        }
    }

    IEnumerator MensajeError()
    {
        string textoInicial = textoModo.text;
        textoModo.color = Color.red;
        textoModo.text = "Cierre la tapa";
        yield return new WaitForSeconds(segundosError);
        textoModo.text = textoInicial;
        textoModo.color = Color.green;
    }

    public void ComprobarTapa()
    {
        cogiendoTapa = true;
        
        if (Mathf.Abs(Mathf.Abs(tapa.transform.eulerAngles.x) - Mathf.Abs(rotMaximaTapa)) < 20)
        {
            tapa.transform.eulerAngles = new Vector3(rotMaximaTapa,tapa.transform.eulerAngles.y, tapa.transform.eulerAngles.z);
            tapa.GetComponent<XRGrabInteractable>().enabled = false;
            tapa.GetComponent<Rigidbody>().useGravity = false;
            puedeIniciar = true;
        }
    }
    public void SoltarTapa()
    {
        cogiendoTapa = false;
    }
    IEnumerator ProgramaFinalizado()
    {
        yield return new WaitForSeconds(2);
        float t = 0;
        Vector3 eulerInicial = tapa.transform.eulerAngles;
        while (t < 1)
        {
            t += Time.deltaTime;
            tapa.transform.eulerAngles = Vector3.Lerp(eulerInicial, new Vector3(rotMinimaTapa, tapa.transform.eulerAngles.y, tapa.transform.eulerAngles.z), t/1);
            yield return null;
        }
        GameControllerEsterilizacion.Instance.programaFinalizado = true;
        GameControllerEsterilizacion.Instance.CheckConditions();
    }
   
}
