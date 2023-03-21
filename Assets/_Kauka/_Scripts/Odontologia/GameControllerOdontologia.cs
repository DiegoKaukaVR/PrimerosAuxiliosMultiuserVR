using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;
using UnityEngine.UI;
using TMPro;

public class GameControllerOdontologia : MonoBehaviour
{
    public static GameControllerOdontologia _instance;
    public scr_StepController stepControllerFormativoModulo1;
    public scr_StepController stepControllerFormativoModulo2;
    public GameObject contenedorScriptsDeshabilitado1;
    public GameObject contenedorScriptsDeshabilitado2;
    public GameObject contenedorScriptsHabilitado;
    public GameObject canvas1;
    public GameObject flechaIndicador;
    GameObject flechaPrevia;

    [Header("Seleccion")]
    public bool modoFormativo;
    public bool modoEvaluativo;
    public bool modulo1;
    public bool modulo2;
    public GameObject[] objetosModulo1;
    public GameObject[] objetosModulo2;

    List<GameObject> objetosActivosActual = new List<GameObject>();

    [Space(20)]
    [Header("Booleanos control")]
    public bool manosLavadas1;
    public bool manosEnjuagadas1;
    public bool agendaConsultada;
    public bool llegarGabinete;
    public bool llamarPaciente1;
    public bool sugerirPacienteGabinete;
    public bool llegarSillon;
    public bool sillonSupino;
    public bool servilletaDentalColocada;
    public bool manosLavadas2;
    public bool manosEnjuagadas2;
    public bool episColocados;
    public bool instrumentalEnBandeja;
    public bool bandejaEnSillon;
    public bool luzColocada;
    public bool sondaAcercada;
    public bool espejoAcercado;
    public bool canulaAspiracionSujeta;
    public int cantidadCorrectaIntercambioMaterial;
    //public bool usandoManoIzquierda;
    public bool sugerirEnjuague;
    public bool pinzasQuitadas;
    public bool servilletaDentalQuitada;
    public bool cobroHonorarios;
    public bool materialUtilizadoRecogido;
    public bool escupideraLimpia;
    public bool guantesQuitados;
    public bool manosLavadas3;
    public bool manosEnjuagadas3;
    [Space(20)]
    public bool placaDigitalAcercada;
    public bool radiografiaRealizada;
    public bool conversacion1;
    public bool conversacionBool;
    public int stepActual;
    

    #region Singleton
    public static GameControllerOdontologia Instance
    {
        get
        {
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        stepControllerFormativoModulo1.gameObject.SetActive(false);
        stepControllerFormativoModulo2.gameObject.SetActive(false);
        contenedorScriptsDeshabilitado1.SetActive(false);
        contenedorScriptsDeshabilitado2.SetActive(false);

        canvas1.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckCanulaAspiracion()
    {

    }

    public void EmpezarCapsula(GameObject panel)
    {
        if (modoFormativo)
        {
            if (modulo1)
            {
                contenedorScriptsDeshabilitado1.SetActive(true);
                DesactivarScripts();
                stepControllerFormativoModulo1.gameObject.SetActive(true);
                stepControllerFormativoModulo1.InitializeSteps();
                
                panel.SetActive(false);
                canvas1.SetActive(true);
            }
            if (modulo2)
            {
                contenedorScriptsDeshabilitado2.SetActive(true);
                DesactivarScripts();
                stepControllerFormativoModulo2.gameObject.SetActive(true);
                stepControllerFormativoModulo2.InitializeSteps();

                panel.SetActive(false);
                canvas1.SetActive(true);
            }
        }
        if (modoEvaluativo)
        {

           
        }
        


    }
    public void ElegirModo(int num)
    {
        if (num == 1)
        {
            modoFormativo = true;
            modoEvaluativo = false;
        }
        if (num == 2)
        {
            modoEvaluativo = true;
            modoFormativo = false;
        }
    }
    public void ElegirModulo(int num)
    {

        if (num == 1)
        {
            modulo1 = true;
            modulo2 = false;
        }
        if (num == 2)
        {
            modulo2 = true;
            modulo1 = false;
        }

    }
    public void InstanciarFlechaInidicador(GameObject target)
    {
        if (flechaPrevia) { Destroy(flechaPrevia); }

        GameObject nuevaFlecha = Instantiate(flechaIndicador);
        nuevaFlecha.GetComponent<IndicadorObjetivo>().objetoDestino = target;
        flechaPrevia = nuevaFlecha;
    }

    public void ActivarObjetosActuales(GameObject objetoActivable)
    {
        objetoActivable.transform.parent = contenedorScriptsHabilitado.transform;
        objetosActivosActual.Add(objetoActivable);
        foreach (var hijo in contenedorScriptsHabilitado.GetComponentsInChildren<Collider>())
        {
            hijo.enabled = true;
            if (hijo.GetComponent<XRSimpleInteractable>()) { hijo.GetComponent<XRSimpleInteractable>().enabled = true; }
            else if (hijo.GetComponent<XRGrabInteractable>()) { hijo.GetComponent<XRGrabInteractable>().enabled = true; }
            else if (hijo.GetComponent<Collider>()) { hijo.GetComponent<Collider>().enabled = true; }
            if (hijo.gameObject.GetComponent<Canvas>()) { hijo.gameObject.GetComponent<Canvas>().enabled = true; }
        }
    }
    public void DesactivarObjetosActuales()
    {
        foreach (GameObject go in objetosActivosActual)
        {
            if (modulo1)
            {
                go.transform.parent = contenedorScriptsDeshabilitado1.transform;
            }
            if (modulo2)
            {
                go.transform.parent = contenedorScriptsDeshabilitado2.transform;
            }

        }
        DesactivarScripts();
    }
    public void DesactivarScripts()
    {
        if (modulo1)
        {

            foreach (var hijo in contenedorScriptsDeshabilitado1.GetComponentsInChildren<Collider>())
            {
                //hijo.enabled = false;
                if (hijo.GetComponent<XRSimpleInteractable>()) { hijo.GetComponent<XRSimpleInteractable>().enabled = false; }
                else if (hijo.GetComponent<XRGrabInteractable>()) { hijo.GetComponent<XRGrabInteractable>().enabled = false; }
                else if (hijo.GetComponent<Collider>()) { hijo.GetComponent<Collider>().enabled = false; }
                if (hijo.gameObject.GetComponent<Canvas>()) { hijo.gameObject.GetComponent<Canvas>().enabled = false; }
                if (hijo.gameObject.GetComponent<Outline>()) { hijo.gameObject.GetComponent<Outline>().enabled = false; }

            }
        }
        if (modulo2)
        {

            foreach (var hijo in contenedorScriptsDeshabilitado2.GetComponentsInChildren<Collider>())
            {
                //hijo.enabled = false;
                if (hijo.GetComponent<XRSimpleInteractable>()) { hijo.GetComponent<XRSimpleInteractable>().enabled = false; }
                else if (hijo.GetComponent<XRGrabInteractable>()) { hijo.GetComponent<XRGrabInteractable>().enabled = false; }
                else if (hijo.GetComponent<Collider>()) { hijo.GetComponent<Collider>().enabled = false; }
                if (hijo.gameObject.GetComponent<Canvas>()) { hijo.gameObject.GetComponent<Canvas>().enabled = false; }
                if (hijo.gameObject.GetComponent<Outline>()) { hijo.gameObject.GetComponent<Outline>().enabled = false; }

            }
        }
    }
    public void BooleanAuxControl(int i)
    {
        if(i == 0)
        {
            llamarPaciente1 = true;
        }
        if (i == 1)
        {
            llegarGabinete = true;
            sugerirPacienteGabinete = true;
        }
        if (i == 2)
        {
            sugerirEnjuague = true;
        }
        if (i == 3)
        {
            agendaConsultada = true;
        }
        if (i == 4)
        {
            cobroHonorarios = true;
        }
        if (i == 5)
        {
            radiografiaRealizada = true;
        }
        if (i == 6)
        {
            conversacionBool = true;
        }
        CheckConditions();
    }

    public void CheckConditions()
    {
        if (modoFormativo)
        {
            if (modulo1)
            {
                if (manosLavadas1 && stepActual == 0)
                {
                    stepControllerFormativoModulo1.NextStep();
                    stepActual++;
                }
                if (manosEnjuagadas1 && stepActual == 1)
                {
                    stepControllerFormativoModulo1.NextStep();
                    stepActual++;
                }
                if (stepActual == 2 && agendaConsultada)
                {
                    stepControllerFormativoModulo1.NextStep();
                    stepActual++;
                }
                if (stepActual == 3 && llamarPaciente1)
                {
                    stepControllerFormativoModulo1.NextStep();
                    stepActual++;
                }
                if (stepActual == 4 && llegarGabinete && sugerirPacienteGabinete)
                {
                    stepControllerFormativoModulo1.NextStep();
                    stepActual++;
                }
                if (stepActual == 5 && sillonSupino)
                {
                    stepControllerFormativoModulo1.NextStep();
                    stepActual++;
                }
                if (stepActual == 6 && servilletaDentalColocada)
                {
                    stepControllerFormativoModulo1.NextStep();
                    stepActual++;
                }
                if (stepActual == 7 && manosLavadas2)
                {
                    stepControllerFormativoModulo1.NextStep();
                    stepActual++;
                }
                if (stepActual == 8 && manosEnjuagadas2)
                {
                    stepControllerFormativoModulo1.NextStep();
                    stepActual++;
                }
                if (stepActual == 9 && episColocados)
                {
                    stepControllerFormativoModulo1.NextStep();
                    stepActual++;
                }
                if (stepActual == 10 && instrumentalEnBandeja)
                {
                    stepControllerFormativoModulo1.NextStep();
                    stepActual++;
                }
                if (stepActual == 11 && bandejaEnSillon)
                {
                    stepControllerFormativoModulo1.NextStep();
                    stepActual++;
                }
                if (stepActual == 12 && luzColocada)
                {
                    stepControllerFormativoModulo1.NextStep();
                    stepActual++;
                }
                if (stepActual == 13 && sondaAcercada && espejoAcercado)
                {
                    stepControllerFormativoModulo1.NextStep();
                    stepActual++;
                }
                if(stepActual == 14 && canulaAspiracionSujeta)
                {
                    stepControllerFormativoModulo1.NextStep();
                    stepActual++;
                }
                if(stepActual == 15 && cantidadCorrectaIntercambioMaterial<=0)
                {
                    stepControllerFormativoModulo1.NextStep();
                    stepActual++;
                }
                if (stepActual == 16 && sugerirEnjuague)
                {
                    stepControllerFormativoModulo1.NextStep();
                    stepActual++;
                }
                if (stepActual == 17 && servilletaDentalQuitada)
                {
                    stepControllerFormativoModulo1.NextStep();
                    stepActual++;
                }
                if(stepActual == 18 && cobroHonorarios)
                {
                    stepControllerFormativoModulo1.NextStep();
                    stepActual++;
                }
                if (stepActual == 19 && materialUtilizadoRecogido)
                {
                    stepControllerFormativoModulo1.NextStep();
                    stepActual++;
                }
                if (stepActual == 20 && escupideraLimpia)
                {
                    stepControllerFormativoModulo1.NextStep();
                    stepActual++;
                }
                if (stepActual == 21 && guantesQuitados)
                {
                    stepControllerFormativoModulo1.NextStep();
                    stepActual++;
                }
                if (stepActual == 22 && manosLavadas3)
                {
                    stepControllerFormativoModulo1.NextStep();
                    stepActual++;
                }
                if (stepActual == 23 && manosEnjuagadas3)
                {
                    stepControllerFormativoModulo1.NextStep();
                    stepActual++;
                    TerminarModulo(1);
                }
            }
            if (modulo2)
            {
                if (manosLavadas1 && stepActual == 0)
                {
                    stepControllerFormativoModulo2.NextStep();
                    stepActual++;
                }
                if (manosEnjuagadas1 && stepActual == 1)
                {
                    stepControllerFormativoModulo2.NextStep();
                    stepActual++;
                }
                if (stepActual == 2 && agendaConsultada)
                {
                    stepControllerFormativoModulo2.NextStep();
                    stepActual++;
                }
                if (stepActual == 3 && llamarPaciente1)
                {
                    stepControllerFormativoModulo2.NextStep();
                    stepActual++;
                }
                if (stepActual == 4 && llegarGabinete && sugerirPacienteGabinete)
                {
                    stepControllerFormativoModulo2.NextStep();
                    stepActual++;
                }
                if (stepActual == 5 && sillonSupino)
                {
                    stepControllerFormativoModulo2.NextStep();
                    stepActual++;
                }
                if (stepActual == 6 && servilletaDentalColocada)
                {
                    stepControllerFormativoModulo2.NextStep();
                    stepActual++;
                }
                if (stepActual == 7 && manosLavadas2)
                {
                    stepControllerFormativoModulo2.NextStep();
                    stepActual++;
                }
                if (stepActual == 8 && manosEnjuagadas2)
                {
                    stepControllerFormativoModulo2.NextStep();
                    stepActual++;
                }
                if (stepActual == 9 && episColocados)
                {
                    stepControllerFormativoModulo2.NextStep();
                    stepActual++;
                }
                if (stepActual == 10 && conversacionBool)
                {
                    stepControllerFormativoModulo2.NextStep();
                    stepActual++;
                    conversacionBool = false;
                }
                if (stepActual == 11 && placaDigitalAcercada)
                {
                    stepControllerFormativoModulo2.NextStep();
                    stepActual++;
                }
                if(stepActual == 12 && radiografiaRealizada)
                {

                }
            }

        }
    }
    public void TerminarModulo(int i)
    {
        if (i == 1)
        {
            print("Modulo 1 terminado.");
        }
        if (i == 2)
        {
            print("Modulo 2 terminado.");
        }
    }
}
