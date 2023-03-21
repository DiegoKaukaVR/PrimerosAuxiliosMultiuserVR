using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;
using UnityEngine.Animations.Rigging;
using UnityEngine.SceneManagement;

public class GameControllerMovilidad : MonoBehaviour
{
    public static GameControllerMovilidad _instance;
    public int pasoActual;
    public int puntoActual;
    public float distanciaCorrectaAgarre;
    public Collider colliderGeneral;

    public GameObject flechaIndicador;
    List<GameObject> flechaPrevia = new List<GameObject>();
    public GameObject punteroLinea;


    [Header("Main bools")]
    public bool modoFormativo;
    public bool modoEvaluativo;
    public bool modulo1;
    public bool modulo2;
    public bool modulo3;
    public bool modulo4;
    public bool modulo5;

    public scr_StepController stepControllerFormativoModulo1;
    public scr_StepController stepControllerFormativoModulo2;
    public scr_StepController stepControllerFormativoModulo3;
    public scr_StepController stepControllerFormativoModulo4;
    public scr_StepController stepControllerFormativoModulo5;

    public GameObject canvas1;
    public GameObject canvas2;
    public GameObject canvas3;
    public GameObject canvas4;
    public GameObject canvas5;

    public GameObject cuerpoModulo1;
    public GameObject cuerpoModulo2;
    public GameObject cuerpoModulo3;
    public GameObject cuerpoModulo4;
    public GameObject cuerpoModulo5;
    public GameObject cuerpoActual;

    public List<GameObject> puntosCorrectos = new List<GameObject>();

    public List<GameObject> puntosModulo1 = new List<GameObject>();
    public List<GameObject> puntosModulo2 = new List<GameObject>();
    public List<GameObject> puntosModulo3 = new List<GameObject>();
    public List<GameObject> puntosModulo4 = new List<GameObject>();
    public List<GameObject> puntosModulo5 = new List<GameObject>();

    public GridPerso gridCC;

    public GameObject puntoCorrectoActual;

    public List<GameObject> agarresMovibles = new List<GameObject>();
    public List<GameObject> agarres = new List<GameObject>();
    public List<GameObject> agarreEspecial = new List<GameObject>();

    public GameObject panelFinalIzq;
    public GameObject panelFinalDer;
    [Header("Booleanos control")]
    public bool primeraCondicionActual;
    public bool segundaCondicionActual;
    public bool terceraCondicionActual;
    public int barandillasSubidas = 0;

    public bool munecaSujeta;
    public bool entrepiernaSujeta;
    public bool piernaSujeta;
    public bool mandosSubiendo;
    public bool mandosBajando;

    public GameObject cuerpoGirableModulo3;

    public int numeroManoDerecha;
    public int numeroManoIzquierda;

    public Animator animCuerpo5;
    public Animator animCama;

    public GameObject[] moviblesCama;
    public GameObject[] constraintsCama;
    int tickSubirCama;

    bool moviendoCama;
    bool subirCama;

    public LocomotionController lc;
    #region Singleton
    public static GameControllerMovilidad Instance
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
        //foreach(GameObject go in puntosCorrectos)
        //{
        //    go.SetActive(false);
        //}
        //SetValidPoint();
        modoFormativo = true;
        ActivarMovimiento(false);
    }
    private void Update()
    {
        if (moviendoCama)
        {
            SubirCamaBoton(subirCama);
        }
    }
    public void Mensaje(string msj)
    {
        print(msj);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
    public void ActivarMovimiento(bool activo)
    {
        lc.enableTP = activo;
    }
    public void EmpezarCapsula(GameObject panel)
    {
        if (modoFormativo)
        {
            if (modulo1)
            {
                foreach(GameObject go in puntosModulo1)
                {
                    puntosCorrectos.Add(go);
                }
                panel.SetActive(false);
                stepControllerFormativoModulo1.gameObject.SetActive(true);
                cuerpoActual = cuerpoModulo1;
                cuerpoModulo1.SetActive(true);
                AddHandles();
                stepControllerFormativoModulo1.InitializeSteps();
                ActivarMovimiento(true);
            }
            if (modulo2)
            {
                foreach (GameObject go in puntosModulo2)
                {
                    puntosCorrectos.Add(go);
                }
                panel.SetActive(false);
                stepControllerFormativoModulo2.gameObject.SetActive(true);
                cuerpoActual = cuerpoModulo2;
                cuerpoModulo2.SetActive(true);
                AddHandles();
                stepControllerFormativoModulo2.InitializeSteps(); ActivarMovimiento(true);
            }
            if (modulo3)
            {
                foreach (GameObject go in puntosModulo3)
                {
                    puntosCorrectos.Add(go);
                }
                panel.SetActive(false);
                
                stepControllerFormativoModulo3.gameObject.SetActive(true);
                cuerpoActual = cuerpoModulo3;
                cuerpoModulo3.SetActive(true);
                AddHandles();
                stepControllerFormativoModulo3.InitializeSteps(); ActivarMovimiento(true);
            }
            if (modulo4)
            {
                foreach (GameObject go in puntosModulo4)
                {
                    puntosCorrectos.Add(go);
                }
                panel.SetActive(false);
                stepControllerFormativoModulo4.gameObject.SetActive(true);
                cuerpoActual = cuerpoModulo4;
                cuerpoModulo4.SetActive(true);
                AddHandles();
                stepControllerFormativoModulo4.InitializeSteps(); ActivarMovimiento(true);
            }
            if (modulo5)
            {
                foreach (GameObject go in puntosModulo5)
                {
                    puntosCorrectos.Add(go);
                }
                panel.SetActive(false);
                stepControllerFormativoModulo5.gameObject.SetActive(true);
                cuerpoActual = cuerpoModulo5;
                cuerpoModulo5.SetActive(true);
                AddHandles();
                stepControllerFormativoModulo5.InitializeSteps(); ActivarMovimiento(true);


            }
        }
        if (modoEvaluativo)
        {
            if (modulo1)
            {

            }
            if (modulo2)
            {

            }
            if (modulo3)
            {

            }
            if (modulo4)
            {

            }
            if (modulo5)
            {

            }
        }
        

    }

    public void AddHandles()
    {
        AgarreMovilidad[] agarresActuales = cuerpoActual.GetComponentsInChildren<AgarreMovilidad>();
        AgarreApoyo[] agarreApoyoActuales = cuerpoActual.GetComponentsInChildren<AgarreApoyo>();

        foreach(AgarreMovilidad am in agarresActuales)
        {
            agarres.Add(am.gameObject);
            //if (!am.gameObject.GetComponent<AgarreMovilidad>().soloRotacion)
            //{
            //    agarresMovibles.Add(am.gameObject);
            //}
        }
        foreach(AgarreApoyo am in agarreApoyoActuales)
        {
            agarres.Add(am.gameObject);
        }

        foreach(AgarreMovilidad am in agarresActuales)
        {
            agarres[am.numeroAgarre] = am.gameObject;
            //if (!am.gameObject.GetComponent<AgarreMovilidad>().soloRotacion)
            //{
            //    agarresMovibles[am.numeroAgarre] = am.gameObject;
            //}
        }

        foreach (AgarreApoyo am in agarreApoyoActuales)
        {
            agarres[am.indice] = am.gameObject;
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
            modulo3 = false;
            modulo4 = false;
            modulo5 = false;
        }
        if (num == 2)
        {
            modulo1 = false;
            modulo2 = true;
            modulo3 = false;
            modulo4 = false;
            modulo5 = false;
        }
        if (num == 3)
        {
            modulo1 = false;
            modulo2 = false;
            modulo3 = true;
            modulo4 = false;
            modulo5 = false;
        }
        if (num == 4)
        {
            modulo1 = false;
            modulo2 = false;
            modulo3 = false;
            modulo4 = true;
            modulo5 = false;
        }
        if (num == 5)
        {
            modulo1 = false;
            modulo2 = false;
            modulo3 = false;
            modulo4 = false;
            modulo5 = true;
        }
    }


    public void SetValidPoint()
    {
        DisablePoints();
        puntosCorrectos[puntoActual].SetActive(true);
        //gridCC.GetDesiredPoints(puntosCorrectos[pasoActual].transform);
        puntoCorrectoActual = puntosCorrectos[puntoActual];
        HideActualObjective();
        ActivarAgarre(puntoCorrectoActual.GetComponent<PuntoObjetivoMovilidad>().indiceAgarreObjetivo);
        puntoActual++;
        
    }
    public void DisablePoints()
    {
        foreach (GameObject go in puntosCorrectos)
        {
            go.SetActive(false);
        }
    }

    public void VisualizeActualObjective(int i)
    {
        if(i == puntoCorrectoActual.GetComponent<PuntoObjetivoMovilidad>().indiceAgarreObjetivo)
        {
            gridCC.GetDesiredPoints(puntoCorrectoActual.transform);
            puntoCorrectoActual.GetComponent<PuntoObjetivoMovilidad>().objetoVisual.SetActive(true);
        }
        else
        {
            gridCC.ClearDesiredPoints();
        }
    }
    public void HideActualObjective()
    {
        puntoCorrectoActual.GetComponent<PuntoObjetivoMovilidad>().objetoVisual.SetActive(false);
    }

    public void ConfirmarCondicionAuxiliar(int i)
    {
        if (i == pasoActual)
        {

            primeraCondicionActual = true;
            CheckConditions();
        }
    }

    public void ConfirmarSegundaCondicionAuxiliar(int i)
    {
        if (i == pasoActual)
        {
            if (segundaCondicionActual == false)
            {

                segundaCondicionActual = true;
                CheckConditions();
            }
            else
            {
                segundaCondicionActual = false;
            }
        }
    }

    public void SujetarAuxiliar(int i)
    {
        if (i == 0)
        {
            if (munecaSujeta)
            {
                munecaSujeta = false;
            }
            else
            {
                munecaSujeta = true;
            }
        }

        if (i == 1)
        {
            if (entrepiernaSujeta)
            {
                entrepiernaSujeta = false;
            }
            else
            {
                entrepiernaSujeta = true;
            }
        }

        if (i == 2)
        {
            if (piernaSujeta)
            {
                piernaSujeta = false;
            }
            else
            {
                piernaSujeta = true;
            }
        }
    }



    public void SoltarAgarreApoyo(int i)
    {
        segundaCondicionActual = false;
    }

    public void InstanciarFlechaInidicador(GameObject target)
    {
        //if (flechaPrevia) { Destroy(flechaPrevia); }

        GameObject nuevaFlecha = Instantiate(flechaIndicador);
        nuevaFlecha.GetComponent<IndicadorObjetivo>().objetoDestino = target;
        flechaPrevia.Add(nuevaFlecha);
    }



    public void DestruirFlechaIndicador()
    {
        //if (flechaPrevia) { Destroy(flechaPrevia); }
        foreach(GameObject go in flechaPrevia)
        {
            Destroy(go);
        }
    }


    public void DesactivarMovibles()
    {
        foreach(GameObject go in agarresMovibles)
        {
            go.GetComponent<XRGrabInteractable>().trackPosition = false;
        }
    }
    public void ActivarMovibles()
    {
        foreach (GameObject go in agarresMovibles)
        {
            go.GetComponent<XRGrabInteractable>().trackPosition = true;
        }
    }

    public void DesactivarAgarresVisual()
    {
        foreach(GameObject go in agarres)
        {
            if (go.GetComponent<XRGrabInteractable>())
            {

                foreach (Collider col in go.GetComponent<XRGrabInteractable>().colliders)
                {
                    col.gameObject.SetActive(false);
                }
                go.GetComponent<XRGrabInteractable>().enabled = false;
            }
            if (go.GetComponent<XRSimpleInteractable>())
            {
                foreach (Collider col in go.GetComponent<XRSimpleInteractable>().colliders)
                {
                    col.gameObject.SetActive(false);
                }
                go.GetComponent<XRSimpleInteractable>().enabled = false;
            }
        }
        DisablePoints();
    }
    public void ActivarAgarre(int i)
    {
        //foreach (Collider col in agarres[i].GetComponent<XRGrabInteractable>().colliders)
        //{
        //    col.gameObject.SetActive(true);
        //}
        //agarres[i].GetComponent<XRGrabInteractable>().enabled = true;


        if (agarres[i].GetComponent<XRGrabInteractable>())
        {

            foreach (Collider col in agarres[i].GetComponent<XRGrabInteractable>().colliders)
            {
                col.gameObject.SetActive(true);
            }
            agarres[i].GetComponent<XRGrabInteractable>().enabled = true;
        }
        if (agarres[i].GetComponent<XRSimpleInteractable>())
        {
            foreach (Collider col in agarres[i].GetComponent<XRSimpleInteractable>().colliders)
            {
                col.gameObject.SetActive(true);
            }
            agarres[i].GetComponent<XRSimpleInteractable>().enabled = true;
        }
    }
    public void BloquearManosModulo4()
    {
        GetComponent<Modulo3Aux>().enabled = true;
        GetComponent<Modulo3Aux>().CogerPosiciones();
    }
    public void ElevarCamaModulo5()
    {
        cuerpoActual.GetComponent<Animator>().SetTrigger("Elevate");
        animCama.SetTrigger("Elevate");
        
    }

    public void SubirCamaBoton(bool activado)
    {
        if (activado)
        {
            foreach(GameObject go in moviblesCama)
            {
                go.transform.position += -new Vector3(0, .001f, 0);
            }
            tickSubirCama++;
            if (tickSubirCama >= 200)
            {
                moviendoCama = false;
                ConfirmarCondicionAuxiliar(5);
                CheckConditions();
            }
        }
        else
        {
            foreach (GameObject go in moviblesCama)
            {
                go.transform.position += new Vector3(0, .001f, 0);
            }
            tickSubirCama--;
        }
    }

    public void ActivarCama(bool activo)
    {
        foreach(GameObject go in constraintsCama)
        {
            go.GetComponent<MultiParentConstraint>().weight = 0;
        }
        
        if (activo)
        {
            subirCama = true;
            moviendoCama = true;
        }
        else
        {
            moviendoCama = false;
        }
    }
    public void ActivarLineaPuntero()
    {
        punteroLinea.SetActive(true);
        punteroLinea.GetComponent<PunteroLinea>().SetPositions(agarres[puntoCorrectoActual.GetComponent<PuntoObjetivoMovilidad>().indiceAgarreObjetivo].transform.position, puntoCorrectoActual.transform.position);
    }
    public void DesactivarLineaPuntero()
    {
        punteroLinea.GetComponent<PunteroLinea>().Disable();

    }

    public void CheckConditions()
    {
        if(modulo1 && modoFormativo)
        {
            if (pasoActual == 0 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                DesactivarAgarresVisual();
                stepControllerFormativoModulo1.NextStep();
            }
            if (pasoActual == 1 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                DesactivarAgarresVisual();
                stepControllerFormativoModulo1.NextStep();
            }
            if (pasoActual == 2 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                DesactivarAgarresVisual();
                stepControllerFormativoModulo1.NextStep();
            }
            if (pasoActual == 3 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                DesactivarAgarresVisual();
                stepControllerFormativoModulo1.NextStep();
            }
            if (pasoActual == 4 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                DesactivarAgarresVisual();
                stepControllerFormativoModulo1.NextStep();
            }
            if (pasoActual == 5 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                DesactivarAgarresVisual();
                stepControllerFormativoModulo1.NextStep();
            }
            if (pasoActual == 6 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                DesactivarAgarresVisual();
                stepControllerFormativoModulo1.NextStep();
            }
            if (pasoActual == 7 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                DesactivarAgarresVisual();
                stepControllerFormativoModulo1.NextStep();
            }
            if (pasoActual == 8 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                DesactivarAgarresVisual();
                stepControllerFormativoModulo1.NextStep();
            }
            if (pasoActual == 9 && primeraCondicionActual )
            {
                primeraCondicionActual = false;
                pasoActual++;
                DesactivarAgarresVisual();
                stepControllerFormativoModulo1.NextStep();
            }
            if (pasoActual == 10 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                segundaCondicionActual = false;
                pasoActual++;
                DesactivarAgarresVisual();
                stepControllerFormativoModulo1.NextStep();
            }
            if (pasoActual == 11 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                segundaCondicionActual = false;
                pasoActual++;
                DesactivarAgarresVisual();
                stepControllerFormativoModulo1.NextStep();
            }
            if (pasoActual == 12 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                segundaCondicionActual = false;
                pasoActual++;
                DesactivarAgarresVisual();
                stepControllerFormativoModulo1.NextStep();
            }
            if (pasoActual == 13 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                DesactivarAgarresVisual();
                stepControllerFormativoModulo1.NextStep();
            }
            if (pasoActual == 14 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                DesactivarAgarresVisual();
                stepControllerFormativoModulo1.NextStep();
            }
            if (pasoActual == 15 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                DesactivarAgarresVisual();
                stepControllerFormativoModulo1.NextStep();
            }
            if (pasoActual == 16 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                DesactivarAgarresVisual();
                stepControllerFormativoModulo1.NextStep();
            }
            if (pasoActual == 17 && primeraCondicionActual)
            {
                stepControllerFormativoModulo1.gameObject.SetActive(false);
                DesactivarAgarresVisual();
                Finalizar();
            }
        }

       
        if (modulo2 && modoFormativo)
        {
            if (pasoActual == 0 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                DesactivarAgarresVisual();
                stepControllerFormativoModulo2.NextStep();
            }
            if (pasoActual == 1 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                DesactivarAgarresVisual();
                stepControllerFormativoModulo2.NextStep();
            }
            if (pasoActual == 2 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                stepControllerFormativoModulo2.NextStep();
                DesactivarAgarresVisual();
            }
            if (pasoActual == 3 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                stepControllerFormativoModulo2.NextStep();
                DesactivarAgarresVisual();
            }
            if (pasoActual == 4 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                stepControllerFormativoModulo2.NextStep();
                DesactivarAgarresVisual();
            }
            if (pasoActual == 5 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                stepControllerFormativoModulo2.NextStep();
                DesactivarAgarresVisual();
            }
            if (pasoActual == 6 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                stepControllerFormativoModulo2.NextStep();
                DesactivarAgarresVisual();
            }
            if (pasoActual == 7 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                stepControllerFormativoModulo2.NextStep();
                DesactivarAgarresVisual();
            }
            if (pasoActual == 8 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                stepControllerFormativoModulo2.NextStep();
                DesactivarAgarresVisual();
            }
            if (pasoActual == 9 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                stepControllerFormativoModulo2.NextStep();
                DesactivarAgarresVisual();
            }
            if (pasoActual == 10 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                segundaCondicionActual = false;
                pasoActual++;
                stepControllerFormativoModulo2.NextStep();
                DesactivarAgarresVisual();
            }
            if (pasoActual == 11 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                segundaCondicionActual = false;
                pasoActual++;
                stepControllerFormativoModulo2.NextStep();
                DesactivarAgarresVisual();
            }
            if (pasoActual == 12 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                segundaCondicionActual = false;
                pasoActual++;
                stepControllerFormativoModulo2.NextStep();
                DesactivarAgarresVisual();
            }
            if (pasoActual == 13 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                stepControllerFormativoModulo2.NextStep();
                DesactivarAgarresVisual();
            }
            if (pasoActual == 14 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                stepControllerFormativoModulo2.NextStep();
                DesactivarAgarresVisual();
            }
            if (pasoActual == 15 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                stepControllerFormativoModulo2.NextStep();
                DesactivarAgarresVisual();
            }
            if (pasoActual == 16 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                stepControllerFormativoModulo2.NextStep();
                DesactivarAgarresVisual();
            }
            if (pasoActual == 17 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                stepControllerFormativoModulo2.NextStep();
                DesactivarAgarresVisual();
            }
            if (pasoActual == 18 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                stepControllerFormativoModulo2.NextStep();
                DesactivarAgarresVisual();
            }
            if (pasoActual == 19 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                stepControllerFormativoModulo2.NextStep();
                DesactivarAgarresVisual();
            }
            if (pasoActual == 20 && primeraCondicionActual)
            {
                stepControllerFormativoModulo2.gameObject.SetActive(false);
                DesactivarAgarresVisual();
                Finalizar();
            }

        }

        if (modulo3 && modoFormativo)
        {
            if (pasoActual == 0 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                stepControllerFormativoModulo3.NextStep();
                DesactivarAgarresVisual();
            }
            if (pasoActual == 1 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                stepControllerFormativoModulo3.NextStep(); DesactivarAgarresVisual();
            }
            if (pasoActual == 2 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                stepControllerFormativoModulo3.NextStep(); DesactivarAgarresVisual();
            }
            if (pasoActual == 3 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                stepControllerFormativoModulo3.NextStep(); DesactivarAgarresVisual();
            }
            if (pasoActual == 4 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                stepControllerFormativoModulo3.NextStep(); DesactivarAgarresVisual();
            }
            if (pasoActual == 5 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                stepControllerFormativoModulo3.NextStep(); DesactivarAgarresVisual();
            }
            if (pasoActual == 6 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                stepControllerFormativoModulo3.NextStep(); DesactivarAgarresVisual();
            }
            if (pasoActual == 7 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                stepControllerFormativoModulo3.NextStep(); DesactivarAgarresVisual();
            }
            if (pasoActual == 8 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                stepControllerFormativoModulo3.NextStep(); DesactivarAgarresVisual();
            }
            if (pasoActual == 9 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                stepControllerFormativoModulo3.NextStep(); DesactivarAgarresVisual();
            }
            if (pasoActual == 10 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                stepControllerFormativoModulo3.NextStep(); DesactivarAgarresVisual();
            }
            if (pasoActual == 11 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                stepControllerFormativoModulo3.NextStep(); DesactivarAgarresVisual();
            }
            if (pasoActual == 12 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                stepControllerFormativoModulo3.NextStep(); DesactivarAgarresVisual();
            }
            if (pasoActual == 13 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                stepControllerFormativoModulo3.NextStep(); DesactivarAgarresVisual();
            }
            if (pasoActual == 14 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                stepControllerFormativoModulo3.NextStep(); DesactivarAgarresVisual();
            }
            if (pasoActual == 15 && primeraCondicionActual)
            {
                stepControllerFormativoModulo3.gameObject.SetActive(false);
                DesactivarAgarresVisual();
                Finalizar();
            }

        }
        if (modulo4 && modoFormativo)
        {
            if (pasoActual == 0 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                stepControllerFormativoModulo4.NextStep(); DesactivarAgarresVisual();
            }
            if (pasoActual == 1 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                stepControllerFormativoModulo4.NextStep(); DesactivarAgarresVisual();
            }
            if (pasoActual == 2 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                stepControllerFormativoModulo4.NextStep(); DesactivarAgarresVisual();
            }
            if (pasoActual == 3 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                stepControllerFormativoModulo4.NextStep(); DesactivarAgarresVisual();
            }
            if (pasoActual == 4 && primeraCondicionActual )
            {
                primeraCondicionActual = false;
                pasoActual++;
                stepControllerFormativoModulo4.NextStep(); DesactivarAgarresVisual();
            }
            if (pasoActual == 5 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                stepControllerFormativoModulo4.NextStep(); DesactivarAgarresVisual();
            }
            if (pasoActual == 6 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                stepControllerFormativoModulo4.NextStep(); DesactivarAgarresVisual();
            }
            if (pasoActual == 7 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                stepControllerFormativoModulo4.NextStep(); DesactivarAgarresVisual();
            }
            if (pasoActual == 8 && primeraCondicionActual)
            {
                stepControllerFormativoModulo4.gameObject.SetActive(false);
                DesactivarAgarresVisual();
                Finalizar();
            }
        }

        if (modulo5 && modoFormativo)
        {
            if (pasoActual == 0 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                stepControllerFormativoModulo5.NextStep(); DesactivarAgarresVisual();
            }
            if (pasoActual == 1 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                stepControllerFormativoModulo5.NextStep(); DesactivarAgarresVisual();
            }
            if (pasoActual == 2 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                stepControllerFormativoModulo5.NextStep(); DesactivarAgarresVisual();
            }
            if (pasoActual == 3 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                stepControllerFormativoModulo5.NextStep(); DesactivarAgarresVisual();
            }
            if (pasoActual == 4 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                stepControllerFormativoModulo5.NextStep(); DesactivarAgarresVisual();
            }
            if (pasoActual == 5 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                stepControllerFormativoModulo5.NextStep(); DesactivarAgarresVisual();
            }
            if (pasoActual == 6 && primeraCondicionActual)
            {
                primeraCondicionActual = false;
                pasoActual++;
                stepControllerFormativoModulo5.NextStep(); DesactivarAgarresVisual();
            }
            if (pasoActual == 7 && primeraCondicionActual)
            {
                stepControllerFormativoModulo5.gameObject.SetActive(false);
                DesactivarAgarresVisual();
                Finalizar();
            }
        }
    }
    public void Finalizar()
    {

        if (modulo1)
        {
            canvas1.SetActive(false);
            panelFinalIzq.SetActive(true);
        }
        if (modulo2)
        {
            canvas2.SetActive(false);
            panelFinalIzq.SetActive(true);
        }
        if (modulo3)
        {
            canvas3.SetActive(false);
            panelFinalIzq.SetActive(true);
        }
        if (modulo4)
        {
            canvas4.SetActive(false);
            panelFinalIzq.SetActive(true);
        }
        if (modulo5)
        {
            canvas5.SetActive(false);
            panelFinalIzq.SetActive(true);
        }
        ActivarMovimiento(false);
    }
}
