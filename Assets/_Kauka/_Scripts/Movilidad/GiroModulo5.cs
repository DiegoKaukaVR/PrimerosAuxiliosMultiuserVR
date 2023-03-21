using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;

public class GiroModulo5 : MonoBehaviour
{
    public GameObject pies;
    public GameObject cintura;

    public Transform posPieIzq;
    public Transform posPieDer;
    public Transform posRodIzq;
    public Transform posRodDer;
    public Transform pieDer;
    public Transform pieIzq;
    public Transform rodDer;
    public Transform rodIzq;


    public GameObject agarreFinalDesactivable;
    Vector3 posInit;
    Vector3 posInicialCintura;
    Vector3 posInicialPies;

    Animator animCuerpo;
    public Animation animacionGiro;

    float t = 0;
    bool seleccionado;
    bool seleccionado2;
    float tiempoAnimActual = 0;
    GameObject objetoObj = null;
    public float value;
    // Start is called before the first frame update
    void Start()
    {
        animCuerpo = GameControllerMovilidad.Instance.animCuerpo5;

       
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.G))
        //{
        //    GameControllerMovilidad.Instance.ElevarCamaModulo5();
        //    //seleccionado = true;
        //    //posInicialPies = pies.transform.position;
        //    //posInicialCintura = cintura.transform.position;
        //    //eulerInicialCintura = cintura.transform.localEulerAngles;
        //    //t = 0;
        //}
        if (Input.GetKeyDown(KeyCode.G))
        {
            GiroPrueba();
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            Animprueba();

        }
        if (seleccionado == true)
        {
            GiroM5(objetoObj);
        }
        if (seleccionado2 == true)
        {
            Giro2(objetoObj);
        }
    }

    //public void GiroM5()
    //{


    //    //float dist = posInit.x - GetComponent<XRGrabInteractable>().interactorsSelecting[0].transform.position.x;

    //    //objetoAMover.transform.position = new Vector3(posInicial.x + curvaMovimientoM4X.Evaluate(dist / refMover), posInicial.y + curvaMovimientoM4Y.Evaluate(dist / refMover), posInicial.z);

    //    t += Time.deltaTime;

    //    pies.transform.position = new Vector3(posInicialPies.x + posicionXPies.Evaluate(t), posInicialPies.y + posicionYPies.Evaluate(t), posInicialPies.z + posicionZPies.Evaluate(t));
    //    //cintura.transform.position = new Vector3(posInicialCintura.x + posicionXPies.Evaluate(t / 2), posInicialCintura.y + posicionYPies.Evaluate(t / 2), posInicialCintura.z + posicionZPies.Evaluate(t / 2));
    //    cintura.transform.localEulerAngles = new Vector3(eulerInicialCintura.x + rotacionCinturaX.Evaluate(t / 2), eulerInicialCintura.y + rotacionCinturaY.Evaluate(t / 2), eulerInicialCintura.z + rotacionCinturaZ.Evaluate(t / 2));


    //    if (t == 1) { seleccionado = false; }
    //    //if (Vector3.Distance(objetoAMover.transform.position, new Vector3(curvaMovimientoM4X.Evaluate(1), curvaMovimientoM4Y.Evaluate(1), objetoAMover.transform.position.z)) < .02f)
    //    //if (dist / refMover > .9f)
    //    //{
    //    //    objetoAMover.transform.position = new Vector3(posInicial.x + curvaMovimientoM4X.Evaluate(1), posInicial.y + curvaMovimientoM4Y.Evaluate(1), posInicial.z);
    //    //    GameControllerMovilidad.Instance.ConfirmarCondicionAuxiliar(5);
    //    //    GameControllerMovilidad.Instance.CheckConditions();
    //    //    seleccionado = false;
    //    //}
    //} 
    public void GiroPrueba()
    {
        //animCuerpo.SetTrigger("Turn");
        animacionGiro["giroM5"].speed = 0;
        animacionGiro.Play();
        animacionGiro["giroM5"].time = value;
    }
    public void Animprueba()
    {
        animCuerpo.SetTrigger("LowArms");
    }
    public void GiroM5(GameObject go)
    {
        objetoObj = go;
        if (seleccionado == false)
        {

            posInit = go.GetComponent<XRSimpleInteractable>().interactorsSelecting[0].transform.position;
            tiempoAnimActual = animacionGiro["giroM5"].time;
            //animCuerpo.SetTrigger("Turn");
            animCuerpo.SetTrigger("Start");

            animacionGiro["giroM5"].speed = 0;
        }
        seleccionado = true;
        
        float distZ = posInit.z - go.GetComponent<XRSimpleInteractable>().interactorsSelecting[0].transform.position.z;
        float distX = posInit.x - go.GetComponent<XRSimpleInteractable>().interactorsSelecting[0].transform.position.x;
        //objetoAMover.transform.position = new Vector3(posInicial.x + curvaMovimientoM4X.Evaluate(dist / refMover), posInicial.y + curvaMovimientoM4Y.Evaluate(dist / refMover), posInicial.z);


        //animacionGiro.Play("giroM5");
        animacionGiro.Play();
        animacionGiro["giroM5"].time = tiempoAnimActual + Mathf.Clamp(-distZ*3, 0,1.5f) + Mathf.Clamp(-distX *3, 0, 1.5f);
        //animacionGiro["giroM5"].time = tiempoAnimActual + value;
        //animCuerpo.GetCurrentAnimatorStateInfo(0).normalizedTime = tiempoAnimActual + Mathf.Abs(dist) / 2;
        //animCuerpo.GetCurrentAnimatorClipInfo(0). = tiempoAnimActual + Mathf.Abs(dist) / 2;

        print(animacionGiro["giroM5"].time);


        //if (Vector3.Distance(objetoAMover.transform.position, new Vector3(curvaMovimientoM4X.Evaluate(1), curvaMovimientoM4Y.Evaluate(1), objetoAMover.transform.position.z)) < .02f)
        if (animacionGiro["giroM5"].time >= 2.9f)
        {
            GameControllerMovilidad.Instance.ConfirmarCondicionAuxiliar(4);
            GameControllerMovilidad.Instance.CheckConditions();
            animCuerpo.SetTrigger("LowArms");
        }
    }

    public void Giro2(GameObject go)
    {
        objetoObj = go;
        if (seleccionado2 == false)
        {
            posInit = go.GetComponent<XRSimpleInteractable>().interactorsSelecting[0].transform.position;
            tiempoAnimActual = animacionGiro["MoverASilla"].time;
            animCuerpo.SetTrigger("Start");

            animacionGiro["MoverASilla"].speed = 0;
        }
        seleccionado2 = true;

        float distZ = posInit.z - go.GetComponent<XRSimpleInteractable>().interactorsSelecting[0].transform.position.z;
        float distX = posInit.x - go.GetComponent<XRSimpleInteractable>().interactorsSelecting[0].transform.position.x;
        //objetoAMover.transform.position = new Vector3(posInicial.x + curvaMovimientoM4X.Evaluate(dist / refMover), posInicial.y + curvaMovimientoM4Y.Evaluate(dist / refMover), posInicial.z);
        


        animacionGiro.Play("MoverASilla");
        animacionGiro["MoverASilla"].time = tiempoAnimActual + Mathf.Clamp(-distZ *1.2f, 0, 1f) + Mathf.Clamp(distX*1.2f , 0, 1f);
        //animacionGiro["MoverASilla"].time = tiempoAnimActual + Mathf.Lerp(-distZ + distX *2, 0, 2f);
        //animacionGiro["giroM5"].time = tiempoAnimActual + value;
        //animCuerpo.GetCurrentAnimatorStateInfo(0).normalizedTime = tiempoAnimActual + Mathf.Abs(dist) / 2;
        //animCuerpo.GetCurrentAnimatorClipInfo(0). = tiempoAnimActual + Mathf.Abs(dist) / 2;



        print(animacionGiro["MoverASilla"].time);

        //if (Vector3.Distance(objetoAMover.transform.position, new Vector3(curvaMovimientoM4X.Evaluate(1), curvaMovimientoM4Y.Evaluate(1), objetoAMover.transform.position.z)) < .02f)
        if (animacionGiro["MoverASilla"].time >= 2f)
        {
            GameControllerMovilidad.Instance.ConfirmarCondicionAuxiliar(7);
            GameControllerMovilidad.Instance.CheckConditions();
            
            animCuerpo.SetTrigger("LowArms2");
            //animCuerpo.enabled = false;
            agarreFinalDesactivable.SetActive(false);
            StartCoroutine(ColocarPies());
        }
    }
    public void Soltar()
    {
        seleccionado = false;
        seleccionado2 = false;
        
    }

    public IEnumerator ColocarPies()
    {
        yield return new WaitForSeconds(.9f);
        pieDer.transform.position = posPieDer.position;
        pieIzq.transform.position = posPieIzq.position;
        rodDer.transform.position = posRodDer.position;
        rodIzq.transform.position = posRodIzq.position;
    }
}
