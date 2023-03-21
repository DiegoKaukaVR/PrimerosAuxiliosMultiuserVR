using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntercambioDeMaterial : MonoBehaviour
{

    public GameObject manoDerechaDentista;
    public GameObject manoIzquierdaDentista;

    public List<GameObject> herramientasParaRecoger = new List<GameObject>();
    public List<string> herramientasParaAcercar = new List<string>();
    public List<int> tiemposEspera = new List<int>();

    int pasoActual = 0;
    public int pasosTotales;
    public GameObject bandejaAux;
    // Start is called before the first frame update
    void Start()
    {
        pasoActual = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CheckNextStep()
    {
        if (pasoActual < pasosTotales) { CurrentAction(); }
        
    }

    void CurrentAction()
    {
        if(pasoActual == 0)
        {
            manoIzquierdaDentista.SetActive(true);
            manoDerechaDentista.SetActive(true);
        }
        else
        {
            manoDerechaDentista.SetActive(true);
            manoDerechaDentista.GetComponent<TriggerHerramientaEspecificaDentista>().herramientaActual = herramientasParaAcercar[pasoActual];
            if (herramientasParaRecoger[pasoActual] != null)
            {
                GameObject herramientaInstanciada = Instantiate(herramientasParaRecoger[pasoActual], manoDerechaDentista.GetComponent<TriggerHerramientaEspecificaDentista>().posicionInstanciar);
                herramientaInstanciada.transform.parent = null;
                herramientaInstanciada.GetComponent<ImanObjeto>().posIman = bandejaAux.transform;
            }
            
            manoDerechaDentista.GetComponent<TriggerHerramientaEspecificaDentista>().colliderDisponible = true;
            //herramientaInstanciada.AddComponent<HerramientaARecoger>();
            //herramientaInstanciada.GetComponent<HerramientaARecoger>().manoActual = manoDerechaDentista;
            //herramientaInstanciada.GetComponent
        }
    }
    public void DetectarAcercarHerramienta()
    {
        manoIzquierdaDentista.SetActive(false);
        manoDerechaDentista.SetActive(false);
        StartCoroutine(EsperarAccion());
    }
    IEnumerator EsperarAccion()
    {
        yield return new WaitForSeconds(tiemposEspera[pasoActual]);
        pasoActual++;
        CheckNextStep();
    }

    public void AcercarMaterialEspecifico(int i)
    {
        manoIzquierdaDentista.SetActive(true);
        manoIzquierdaDentista.GetComponent<TriggerHerramientaEspecificaDentista>().indiceActual = i;
    }
}
