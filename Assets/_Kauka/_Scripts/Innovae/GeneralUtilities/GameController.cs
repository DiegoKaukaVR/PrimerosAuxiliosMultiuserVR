using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
public class GameController : Singleton<GameController>
{
    #region VARIABLES
    #region Inteface Variables
    [Space(10)]
    [Tooltip("write here the name of the .csv located in /Assets/_Innovae/Resources that u want to use for language")]
    [Header("Variables for the control of the UI")]
    [Space(10)]
    public string textsFileName = "Textos_industria";
    public Idioma selectedLanguage = Idioma.ESP;
    #endregion Inteface Variables
    #region Player Variables
    //We save this variables in case we need them along the application.
    [SerializeField]
    private ContinuousMoveProviderBase player;
    private Camera playerCamera;
    #endregion Player Variables
    #region Game Variables
    [Space(10)]
    [Header("Variables for the control of the Game")]
    [Tooltip("Select the starting GameMode for stablishing the scene")]
    [Space(10)]
    [SerializeField]
    private GameMode gameMode = GameMode.Other;
    [Space(10)]
    [Header("Teleport Points for the player")]
    [Space(10)]
    [SerializeField]
    private Transform startTeleport;
    private Coroutine teleportCoroutine;
    #endregion Game Variables
    #endregion VARIABLES
#if UNITY_EDITOR
    [MenuItem("Innovae/Cargar Textos")]
    static void CargarTextos()
    {
        LanguageController.LoadDictionary();
    }
    [MenuItem("Innovae/Cargar Keys")]
    static void CargarKeys()
    {
        LanguageController.GetKeys();
    }
    [MenuItem("Innovae/Crear CSV")]
    static void CrearCSV()
    {
        LanguageController.CrearCSV();
    }
    [MenuItem("Innovae/Agregar translatables")]
    static void CrearTranslatable()
    {
        foreach (TextMeshProUGUI textmesh in Resources.FindObjectsOfTypeAll(typeof(TextMeshProUGUI)))
        {
            if (textmesh.gameObject.GetComponent<Translatable>() == null)
            {
                Translatable tr = textmesh.gameObject.AddComponent<Translatable>();
                tr.GetText();
            }
        }
    }
#endif
    #region MonoBehaviour Methods
    /// <summary>
    /// We initialize the different components we have on the scene. we search for them and in case of finding them
    /// we initialize them and save in variables too.
    /// </summary>
    private void Awake()
    {
        playerCamera = Camera.main;
        InitializeDictionary();
    }
    /// <summary>
    /// Here we make the comprobation if we are starting a Tutorial or the application mode.
    /// We initialize in base the mode we have choosen
    /// </summary>
    private void Start()
    {
        if (gameMode == GameMode.Other)
        {
            if (PlayerPrefs.GetInt("Mode") == 0)
            {
                gameMode = GameMode.Application;
            }
            else if (PlayerPrefs.GetInt("Mode") == 1)
            {
                gameMode = GameMode.Tutorial;
            }
        }
        teleportPlayerStart();
    }
    #endregion MonoBehaviour Methods
    //Functions for initializing all the components that come with the scene start
    #region Initialization methods
    /// <summary>
    /// We Initialize here our language manager, sending the .csv archive name and the language that has been selected.
    /// </summary>
    public void InitializeDictionary()
    {
        LanguageController.LoadDictionary();
    }
    #endregion Initialization methods
    //Getters and Setters for nonpublic and important variables
    #region GETSET
    public Camera GetPlayerCamera()
    {
        return playerCamera;
    }
    #endregion GETSET
    
    #region Player teleport
    public void teleportPlayerStart()
    {
        // Start the teleport movement coroutine to change the players position and rotation depending on what game mode it is playing
        if (teleportCoroutine != null)
        {
            StopCoroutine(teleportCoroutine);
        }
        teleportCoroutine = StartCoroutine(TeleportMovementEmpezar(startTeleport));
    }
    public void teleportPlayer(Transform transform)
    {
        if (teleportCoroutine != null)
        {
            StopCoroutine(teleportCoroutine);
        }
        teleportCoroutine = StartCoroutine(TeleportMovement(transform));
    }
    private IEnumerator TeleportMovementEmpezar(Transform destiny)
    {
        // Wait some time and then change both rotation and position of the camera to set it to the were the designer wants the player to be and what to be looking at
        player.enabled = false;
        yield return new WaitForSecondsRealtime(0.5f);
        // Rotation
        // Set the new rotation to the cameraRig
        player.transform.rotation = destiny.rotation;
        // Calculate rotational offset between CameraRig and Camera
        float offsetAngleY = playerCamera.transform.rotation.eulerAngles.y - player.transform.rotation.eulerAngles.y;
        // Now rotate CameraRig in opposite direction to compensate
        player.transform.Rotate(0f, -offsetAngleY, 0f);
        // Position
        // Calculate postional offset between CameraRig and Camera
        Vector3 offsetPos = playerCamera.transform.position - player.transform.position;
        // Reposition CameraRig to desired position minus offset
        player.transform.position = (destiny.position - offsetPos);
        //player.ResetOrientation();
        yield return new WaitForSecondsRealtime(0.5f);
        player.enabled = true;
        yield return new WaitForSecondsRealtime(1.5f);
        stopTeleport();
    }
    private IEnumerator TeleportMovement(Transform destiny)
    {
        // Wait some time and then change both rotation and position of the camera to set it to the were the designer wants the player to be and what to be looking at
        player.enabled = false;
        player.GetComponent<CharacterController>().enabled = false;
        yield return new WaitForSecondsRealtime(2);
        // Rotation
        // Set the new rotation to the cameraRig
        player.transform.rotation = destiny.rotation;
        // Calculate rotational offset between CameraRig and Camera
        float offsetAngleY = playerCamera.transform.rotation.eulerAngles.y - player.transform.rotation.eulerAngles.y;
        // Now rotate CameraRig in opposite direction to compensate
        player.transform.Rotate(0f, -offsetAngleY, 0f);
        // Reposition CameraRig to desired position minus offset
        player.transform.position = destiny.position + new Vector3(0, 2.5f, 0);
        player.enabled = true;
        player.GetComponent<CharacterController>().enabled = true;
        yield return new WaitForSecondsRealtime(1);
        player.transform.position = new Vector3(destiny.position.x, player.transform.position.y, destiny.position.z);
        yield return new WaitForSecondsRealtime(1.5f);
        stopTeleport();
    }
    private void stopTeleport()
    {
        if (teleportCoroutine != null)
        {
            StopCoroutine(teleportCoroutine);
        }
    }
    #endregion Player teleport
}