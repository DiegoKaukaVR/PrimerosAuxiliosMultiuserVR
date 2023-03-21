using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParteSuciaHerramienta : MonoBehaviour
{
    public float valorSuciedad;
    public float valorLimpiezaAgua;
    public float valorSecado;
    float suciedadActual;
    float aguaNecesariaActual;
    float secadoActual;


    public string tagHerramientaDeLimpieza;
    public string tagHerramientaSecado;
    public bool triggerContinuo;
    public bool limpiezaManual;
    public GameObject vfx;
    public GameObject vfxLimpio;

    public bool limpio;
    public bool enjuagado;
    public bool secado;
    // Start is called before the first frame update
    void Start()
    {
        suciedadActual = valorSuciedad;
        aguaNecesariaActual = valorLimpiezaAgua;
        secadoActual = valorSecado;
    }


    private void OnTriggerStay(Collider other)
    {

        if (other.tag == tagHerramientaDeLimpieza && triggerContinuo && !limpio)
        {
            suciedadActual -= 1f;
            Instantiate(vfx, other.transform.position, Quaternion.identity, null);
            if (suciedadActual <= 0)
            {
                Instantiate(vfxLimpio, other.transform.position, Quaternion.identity, null);
                GetComponentInParent<HerramientaBandeja>().ComprobarSuciedadActual();
                limpio = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == tagHerramientaDeLimpieza && !limpio && !limpiezaManual)
        {
            print("limpiando " + tagHerramientaDeLimpieza);
            suciedadActual -= 1f;
            Instantiate(vfx, other.transform.position, Quaternion.identity, null);
            if (suciedadActual <= 0)
            {
                Instantiate(vfxLimpio, other.transform.position, Quaternion.identity, null);
                GetComponentInParent<HerramientaBandeja>().ComprobarSuciedadActual();
                limpio = true;
                GameControllerEsterilizacion.Instance.herramientaLimpia = true;
                GameControllerEsterilizacion.Instance.CheckConditions();
            }
        }

        if (other.tag == tagHerramientaSecado && !secado && limpio && enjuagado && GameControllerEsterilizacion.Instance.modoFormativo)
        {
            print("secando " + tagHerramientaSecado);
            Secar(other.ClosestPoint(gameObject.transform.position));
        }
        if (other.tag == tagHerramientaSecado && GameControllerEsterilizacion.Instance.modoEvaluativo)
        {
            print("secando " + tagHerramientaSecado);
            Secar(other.ClosestPoint(gameObject.transform.position));
        }
    }

    public void LimpiezaManual(Vector3 posPart)
    {
        print("limpiando " + tagHerramientaDeLimpieza + "manual");
        suciedadActual -= 1f;
        Instantiate(vfx, posPart, Quaternion.identity, null);
        if (suciedadActual <= 0)
        {
            Instantiate(vfxLimpio, posPart, Quaternion.identity, null);
            GetComponentInParent<HerramientaBandeja>().ComprobarSuciedadActual();
            limpio = true;
        }
    }
    public void Enjuagar(Vector3 posPart)
    {
        aguaNecesariaActual -= 1f;
        if (aguaNecesariaActual <= 0)
        {
            GetComponentInParent<HerramientaBandeja>().ComprobarEnjuagado();
            Instantiate(vfxLimpio, posPart, Quaternion.identity, null);
            enjuagado = true;
        }
    }

    public void Secar(Vector3 posPart)
    {
        secadoActual -= 1f;
        if (secadoActual <= 0)
        {
            GetComponentInParent<HerramientaBandeja>().ComprobarSecado();
            Instantiate(vfxLimpio, posPart, Quaternion.identity, null);
            secado = true;
        }
    }
}
