using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;
using UnityEngine.UI;
using TMPro;

public class GameControllerEsterilizacion : MonoBehaviour
{
    public static GameControllerEsterilizacion _instance;
    public scr_StepController stepControllerFormativoModulo1;
    public scr_StepController stepControllerFormativoModulo2;
    public GameObject contenedorScriptsDeshabilitado;
    public GameObject contenedorScriptsHabilitado;
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
    [Header("Booleanos Control")]
    public bool manosLavadas;
    public bool manosEnjuagadas;
    public bool prendaUnicaPuesta;
    public bool prendasPuestas;
    public bool materialRecibido;
    public bool materialRegistrado;
    public bool materialAMesaTrabajo;
    //public bool materialColocadoMesa1;
    public bool materialInteractuado;
    public bool materialInspeccionado;
    public bool materialDeshechado;
    public bool materialEnFregadero;
    public bool temperaturaCorrectaAgua;
    public bool detergenteVertido;
    public bool herramientaLimpia;
    public bool materialLimpio;
    public bool materialEnjuagado;
    public bool materialSecado;
    public bool utilizandoPistolaPresionSecado;
    public bool proteccionAuditivaSecado;
    public bool materialAMesaTrabajo2;
    public bool materialClasificado;
    public bool materialEnTalla;
    public bool tallaColocada;
    public bool tallaCerrada;
    public bool tallaSellada;
    public bool controlQuimicoInteriorColocado;
    public bool controlQuimicoExteriorColocado;
    public bool fechaCaducidadColocada;
    public bool filtroEnTalla;
    public bool tapaEnTalla;
    public bool bandejaEnAutoclave;
    public bool programaCorrectoAutoclave;
    public bool programaFinalizado;
    public bool fueraAutoclave;
    public bool controlBowieDick;
    public bool controlesAmpollas;
    public bool comprobarControlesQuimicos;
    public bool abrirTalla;
    public bool materialRepartido;
    public bool finalizarEsterilizacion;

    [Header("Condiciones adicionales evaluativo")]
    public int prendasColocadas;
    int stepActual;
    public List<int> controladoresQuimicosCorrectos = new List<int>();

    [Header("Resultados")]
    public GameObject panelResultados;
    public GameObject textoResultados;

    [Header("Otros Objetos")]
    public GameObject slotsBandeja1;
    public GameObject slotsBandeja2;
    IndiceObjetos[] iobjs;
    
    #region Singleton
    public static GameControllerEsterilizacion Instance
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
        tallaColocada = true;
        stepControllerFormativoModulo1.gameObject.SetActive(false);
        stepControllerFormativoModulo2.gameObject.SetActive(false);
        iobjs = FindObjectsOfType<IndiceObjetos>();
        controladoresQuimicosCorrectos.Add(1);
        controladoresQuimicosCorrectos.Add(3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EmpezarCapsula()
    {
        if (modoFormativo)
        {
            DesactivarScripts();
            if (modulo1)
            {
                foreach(IndiceObjetos io in iobjs)
                {
                    if(io.moduloPerteneciente != 1)
                    {
                        foreach (Transform hijo in io.gameObject.transform)
                        {
                            hijo.gameObject.SetActive(false);
                        }
                    }
                }
                slotsBandeja1.GetComponent<SlotsBandeja>().ColocarHerramientas();
                stepControllerFormativoModulo1.gameObject.SetActive(true);
                stepControllerFormativoModulo1.InitializeSteps();
            }
            if (modulo2)
            {
                foreach (IndiceObjetos io in iobjs)
                {
                    if (io.moduloPerteneciente != 2)
                    {
                        foreach (Transform hijo in io.gameObject.transform)
                        {
                            hijo.gameObject.SetActive(false);
                        }
                    }
                }
                slotsBandeja2.GetComponent<SlotsBandeja>().ColocarHerramientas();
                stepControllerFormativoModulo2.gameObject.SetActive(true);
                stepControllerFormativoModulo2.InitializeSteps();
            }
        }
        if (modoEvaluativo)
        {
            
            if (modulo1)
            {
                foreach (IndiceObjetos io in iobjs)
                {
                    if (io.moduloPerteneciente != 1)
                    {

                        foreach(Transform hijo in io.gameObject.transform)
                        {
                            hijo.gameObject.SetActive(false);
                        }
                    }
                }
                slotsBandeja1.GetComponent<SlotsBandeja>().ColocarHerramientas();
            }
            if (modulo2)
            {
                foreach (IndiceObjetos io in iobjs)
                {
                    if (io.moduloPerteneciente != 2)
                    {
                        foreach (Transform hijo in io.gameObject.transform)
                        {
                            hijo.gameObject.SetActive(false);
                        }
                    }
                }
                slotsBandeja2.GetComponent<SlotsBandeja>().ColocarHerramientas();
            }
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
            if (hijo.GetComponent<XRGrabInteractable>()) { hijo.GetComponent<XRGrabInteractable>().enabled = true; }
            if (hijo.gameObject.GetComponent<Canvas>()) { hijo.gameObject.GetComponent<Canvas>().enabled = true; }
        }
    }
    public void DesactivarObjetosActuales()
    {
        foreach(GameObject go in objetosActivosActual)
        {
            go.transform.parent = contenedorScriptsDeshabilitado.transform;
            
        }
        DesactivarScripts();
    }
    public void DesactivarScripts()
    {
        foreach(var hijo in contenedorScriptsDeshabilitado.GetComponentsInChildren<Collider>())
        {
            hijo.enabled = false;
            if (hijo.GetComponent<XRGrabInteractable>()) { hijo.GetComponent<XRGrabInteractable>().enabled = false; }
            if (hijo.gameObject.GetComponent<Canvas>()) { hijo.gameObject.GetComponent<Canvas>().enabled = false; }
        }
    }
    public void ActivarTodo()
    {
        foreach (var hijo in contenedorScriptsDeshabilitado.GetComponentsInChildren<Collider>())
        {
            hijo.enabled = true;
            if (hijo.GetComponent<XRGrabInteractable>()) { hijo.GetComponent<XRGrabInteractable>().enabled = true; }
        }
    }

    public void CheckConditions()
    {
        if (modoFormativo)
        {
            if (modulo1)
            {
                if (manosLavadas && stepActual == 0)
                {
                    stepControllerFormativoModulo1.NextStep();
                    stepActual++;
                }
                if (stepActual == 1 && manosEnjuagadas)
                {
                    stepControllerFormativoModulo1.NextStep();
                    stepActual++;
                }
                if (stepActual == 2 && prendasPuestas)
                {
                    stepControllerFormativoModulo1.NextStep();
                    stepActual++;
                }
                if (stepActual == 3 && materialRecibido && materialRegistrado)
                {
                    stepControllerFormativoModulo1.NextStep();
                    stepActual++;
                }
                if (stepActual == 4 && materialAMesaTrabajo)
                {
                    stepControllerFormativoModulo1.NextStep();
                    stepActual++;
                }
                if (stepActual == 5 && materialDeshechado && materialInspeccionado)
                {
                    stepControllerFormativoModulo1.NextStep();
                    stepActual++;
                }
                if (stepActual == 6 && materialEnFregadero)
                {
                    stepControllerFormativoModulo1.NextStep();
                    stepActual++;
                }
                if (stepActual == 7 && temperaturaCorrectaAgua)
                {
                    stepControllerFormativoModulo1.NextStep();
                    stepActual++;
                }
                if (stepActual == 8 && detergenteVertido)
                {
                    stepControllerFormativoModulo1.NextStep();
                    stepActual++;
                }
                if (stepActual == 9 && materialAMesaTrabajo2 && materialSecado)
                {
                    stepControllerFormativoModulo1.NextStep();
                    stepActual++;
                    TerminarModulo(1);
                }  
            }
            if (modulo2)
            {
                if (stepActual == 1 && materialEnTalla)
                {
                    stepControllerFormativoModulo2.NextStep();
                    stepActual++;
                }
                if (stepActual == 2 && tallaCerrada && tallaSellada && filtroEnTalla && controlQuimicoExteriorColocado)
                {
                    stepControllerFormativoModulo2.NextStep();
                    stepActual++;
                }
                if (stepActual == 3 && bandejaEnAutoclave)
                {
                    stepControllerFormativoModulo2.NextStep();
                    stepActual++;
                }
                if (stepActual == 4 && programaCorrectoAutoclave)
                {
                    stepControllerFormativoModulo2.NextStep();
                    stepActual++;
                }
                if (stepActual == 5 && programaFinalizado)
                {
                    stepControllerFormativoModulo2.NextStep();
                    stepActual++;
                }
                if (stepActual == 6 && fueraAutoclave)
                {
                    stepControllerFormativoModulo2.NextStep();
                    stepActual++;
                }
                if (stepActual == 7 && controlBowieDick && comprobarControlesQuimicos)
                {
                    stepControllerFormativoModulo2.NextStep();
                    stepActual++;
                    TerminarModulo(2);
                }
            }
        }
        if (modoEvaluativo)
        {
            if (modulo1)
            {
                if (prendaUnicaPuesta && (!manosLavadas || !manosEnjuagadas))
                {
                    CrearTextoErrores("No se lava las manos.");
                    manosLavadas = true; manosEnjuagadas = true;
                }
                if (materialRecibido)
                {
                    if (!prendasPuestas)
                    {
                        CrearTextoErrores("No lleva el vestuario de trabajo.");
                        prendasPuestas = true;
                        if ((!manosLavadas || !manosEnjuagadas) && !prendaUnicaPuesta)
                        {
                            CrearTextoErrores("No se lava las manos."); 
                            manosLavadas = true; manosEnjuagadas = true;
                        }
                    }
                }
                if (materialAMesaTrabajo)
                {
                    if (!materialRegistrado)
                    {
                        CrearTextoErrores("No comprueba el inventario del instrumental recibido / No introduce los datos del material recibido");
                        materialRegistrado = true;
                        if (!prendasPuestas)
                        {
                            CrearTextoErrores("No lleva el vestuario de trabajo.");
                            prendasPuestas = true;
                            if (!manosLavadas || !manosEnjuagadas)
                            {
                                CrearTextoErrores("No se lava las manos.");
                                manosLavadas = true; manosEnjuagadas = true;
                            }
                        }
                    }
                }
                if (materialEnFregadero)
                {
                    if (!materialDeshechado)
                    {
                        CrearTextoErrores("No desecha el instrumental deteriorado en el contenedor adecuado.");
                        materialDeshechado = true;
                    }
                    if (!materialInspeccionado)
                    {
                        CrearTextoErrores("No desecha el instrumental deteriorado.");
                        materialInspeccionado = true;
                    }
                    if (!materialInteractuado)
                    {
                        CrearTextoErrores("No abre los instrumentos articulados.");
                        materialInteractuado = true;
                    }
                }
                if (herramientaLimpia)
                {
                    if (!temperaturaCorrectaAgua)
                    {
                        CrearTextoErrores("No regula la temperatura del agua correctamente.");
                        temperaturaCorrectaAgua = true;
                    }
                    if (!detergenteVertido)
                    {
                        CrearTextoErrores("No utiliza el detergente adecuado.");
                        detergenteVertido = true;
                    }
                }
                if (utilizandoPistolaPresionSecado)
                {
                    if (!proteccionAuditivaSecado)
                    {
                        CrearTextoErrores("No se pone la proteccion auditiva si se seca con pistola a presión.");
                    }
                }
                if (materialAMesaTrabajo2)
                {
                    if (!materialLimpio)
                    {
                        CrearTextoErrores("No frota/limpia suficientemente cada instrumento.");
                    }
                    if (!materialEnjuagado)
                    {
                        CrearTextoErrores("No enjuaga cada instrumento.");
                    }
                    if (!materialSecado)
                    {
                        CrearTextoErrores("No seca cada instrumento de forma adecuada.");
                    }
                    TerminarModulo(1);
                }
                if(materialSecado && !materialAMesaTrabajo2)
                {
                    CrearTextoErrores("No efectúa el traspaso del instrumetal a el área limpia.");
                }
            }
            if (modulo2)
            {
                if (materialEnTalla)
                {
                    if (!tallaColocada)
                    {
                        CrearTextoErrores("No coloca la talla dentro del contenedor rígido.");
                    }
                    if (!materialClasificado)
                    {
                        CrearTextoErrores("No clasifica el material recibido.");
                    }
                }
                if (tallaCerrada)
                {
                    if (!materialEnTalla)
                    {
                        CrearTextoErrores("No coloca los intrumentos dentro del contenedor.");
                    }
                    if (!controlQuimicoInteriorColocado)
                    {
                        CrearTextoErrores("No introduce el control químico interno.");
                    }
                }
                if (filtroEnTalla)
                {
                    if (!tallaCerrada)
                    {
                        CrearTextoErrores("No dobla la talla correctamente.");

                        if (!materialEnTalla)
                        {
                            CrearTextoErrores("No coloca los intrumentos dentro del contenedor.");
                        }
                        if (!controlQuimicoInteriorColocado)
                        {
                            CrearTextoErrores("No introduce el control químico interno.");
                        }
                    }
                }
                if (tapaEnTalla)
                {
                    if (!filtroEnTalla)
                    {
                        CrearTextoErrores("No coloca el filtro en la talla.");
                        if (!tallaCerrada)
                        {
                            CrearTextoErrores("No dobla la talla correctamente.");

                            if (!materialEnTalla)
                            {
                                CrearTextoErrores("No coloca los intrumentos dentro del contenedor.");
                            }
                            if (!controlQuimicoInteriorColocado)
                            {
                                CrearTextoErrores("No introduce el control químico interno.");
                            }
                        }
                    }
                }
                if (bandejaEnAutoclave)
                {
                    if (!controlQuimicoExteriorColocado)
                    {
                        CrearTextoErrores("No coloca los controles químicos externos.");
                    }
                    if (!fechaCaducidadColocada)
                    {
                        CrearTextoErrores("No coloca la fecha de caducidad.");
                    }
                }
                if (programaFinalizado)
                {
                    if (!programaCorrectoAutoclave)
                    {
                        CrearTextoErrores("No programa bien el autoclave.");
                    }
                    if (!controlBowieDick)
                    {
                        CrearTextoErrores("No se hicieron los controles de Bowie-Dick antes de la primera carga del día.");
                    }
                    if (!controlesAmpollas)
                    {
                        CrearTextoErrores("No se coocaron controles con ampollas de esporas a la primera carga");
                    }
                }
                if (abrirTalla)
                {
                    if (!comprobarControlesQuimicos)
                    {
                        CrearTextoErrores("No se controlan los indicadores cuando termina la esterilización");
                    }
                }
                if (finalizarEsterilizacion)
                {
                    if (!materialRepartido)
                    {
                        CrearTextoErrores("No se reparte de manera correcta el material esterilizado.");
                    }
                    TerminarModulo(2);
                }
            }
        }
    }

    public void CrearTextoErrores(string error)
    {
        GameObject nuevoTexto = Instantiate(textoResultados, panelResultados.transform);
        nuevoTexto.GetComponent<TMP_Text>().text = error;
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
