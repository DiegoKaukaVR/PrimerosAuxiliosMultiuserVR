using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceptorPegatina : MonoBehaviour
{
    public GameObject autoclave;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Pistola" && other.GetComponentInParent<PistolaPresion>())
        {
            if (other.GetComponentInParent<PistolaPresion>().esPegatina && other.GetComponentInParent<PistolaPresion>().puedePegatina && other.GetComponentInParent<PistolaPresion>().triggerM)
            {
                GameObject pegatina =  Instantiate(other.GetComponentInParent<PistolaPresion>().pegatina, other.transform.position, Quaternion.identity, transform.root);
                pegatina.transform.parent = this.gameObject.transform;
                other.GetComponentInParent<PistolaPresion>().puedePegatina = false;
                GameControllerEsterilizacion.Instance.controlQuimicoExteriorColocado = true;
                GameControllerEsterilizacion.Instance.CheckConditions();
                autoclave.GetComponent<Autoclave>().controladorQuimico = pegatina;
            }
        }
    }
}
