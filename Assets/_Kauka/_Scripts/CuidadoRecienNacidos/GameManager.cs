using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using Unity.XR.CoreUtils;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool testing;
    public float timeScale = 1;

    public Transform offset;

    public string UserName = "Default";

    [Header("Module Configuration")]
    public Modulo currentModulo;
    public enum Modulo
    {
        formativo,
        evaluativo
    }

    public enum Capsula
    {
        PrimerosAuxilios,
        Comunicacion
    }

    public Capsula CapsulaType;

    /// <summary>
    /// module ID
    /// </summary>
    public int moduleIndex = 0;

    public void StartTutorial()
    {

    }
    public bool CheckEvaluative()
    {
        if (currentModulo == Modulo.evaluativo)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public void ResetSceneInSeconds(float time)
    {
        Invoke("ResetScene", time);
    }

    public GameObject fade;
    public void ResetScene()
    {
        
        fade.GetComponent<Animator>().SetBool("fade", false);
        Invoke("Reset", 1f);
    }
    public void Fade()
    {
        fade.GetComponent<Animator>().SetBool("fade", false);
    }

    private void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    [Header("XR Configuration")]
    public XROrigin xrRig;
    [HideInInspector] public XRController leftController;
    [HideInInspector] public XRController rightController;
    [HideInInspector] public ControllerVelocity controllerVelocity;
    public HapticController hapticController;

    [Header("AudioManager")]
    public AudioManager audioManager;

    [Header("Process")]
    public Process currentProcess;

    DeviceBasedSnapTurnProvider turnProvider;
    TeleportationProvider teleportProvider;
    LocomotionController locomotionController;

    public SeleccionModuloComunicacion moduleSelectionComunicacion;
    public SeleccionModuloPrimerosAuxilios moduleSelectionPrimerosAuxilios;
    public void SetProcess(Process newProcess)
    {
        currentProcess = newProcess;
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        Initialize();
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        currentProcess.FinishStepEvent();
    //    }
    //}

    void Initialize()
    {
        Time.timeScale = timeScale;

        if (xrRig == null)
        {
            xrRig = FindObjectOfType<XROrigin>();
        }

        controllerVelocity = xrRig.GetComponent<ControllerVelocity>();
        audioManager = GetComponentInChildren<AudioManager>();
        //turnProvider = xrRig.GetComponent<DeviceBasedSnapTurnProvider>();
        teleportProvider = xrRig.GetComponent<TeleportationProvider>();
        locomotionController= xrRig.GetComponent<LocomotionController>();
        hapticController = xrRig.GetComponent<HapticController>();

        if (GetComponent<SeleccionModuloComunicacion>())
        {
            moduleSelectionComunicacion = GetComponent<SeleccionModuloComunicacion>();
        }
        if (GetComponent<SeleccionModuloPrimerosAuxilios>())
        {
            moduleSelectionPrimerosAuxilios = GetComponent<SeleccionModuloPrimerosAuxilios>();
        }
      
    }
    
    public void SelectModule(int index)
    {
        switch (CapsulaType)
        {
            case Capsula.PrimerosAuxilios:
                moduleSelectionPrimerosAuxilios.SetModule(index);
                break;
            case Capsula.Comunicacion:
                moduleSelectionComunicacion.SetModule(index);
                break;
            default:
                break;
        }
    }
  


    //UI TEXT
    //BACK TO LOBBY
    //NEXT MODULE
    //GO TO OTHER MODULES



    public void FinishExperience()
    {

        Debug.Log("FinishExperience");



    }

    public Transform SocketTransformSimulation;



    public void EnableTurn(bool value)
    {
        //turnProvider.enabled = value;
    }

    public void EnableMovementTeleport(bool value)
    {
        teleportProvider.enabled = value;
        locomotionController.enabled = value;
    }

    GameObject flechaPrevia;
    public GameObject flechaIndicador;

    public void InstanciarFlechaInidicador(Transform target)
    {
        if (flechaPrevia) { Destroy(flechaPrevia); }
        GameObject nuevaFlecha = Instantiate(flechaIndicador);
        nuevaFlecha.GetComponent<IndicadorObjetivo>().objetoDestino = target.gameObject;
        flechaPrevia = nuevaFlecha;

    }

    public void DestroyFlechaIndicador()
    {
        if (flechaPrevia) { Destroy(flechaPrevia); }
    }

    public GameObject DummyPlayer;
    public void SetDummyPlayer(bool value)
    {
        if (DummyPlayer != null)
        {
            DummyPlayer.SetActive(value);
        }
      
    }

}
