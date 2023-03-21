using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerramientaARecoger : MonoBehaviour
{
    public GameObject manoActual;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RecogerHerramienta()
    {
        if (manoActual) { manoActual.GetComponent<TriggerHerramientaEspecificaDentista>().colliderDisponible = true; }
    }
}
