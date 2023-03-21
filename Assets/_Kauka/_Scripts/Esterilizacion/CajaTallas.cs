using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CajaTallas : MonoBehaviour
{
    int tallasCerradas = 0;
    public bool cajaSellada;
    public Image img;
    public Transform posAutoclave1;
    public Transform posAutoclave2;
    public Transform posFueraAutoclave;
    public GameObject tapa;
    public GameObject[] tallas;
    // Start is called before the first frame update
    void Start()
    {
        img.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        if (tapa.GetComponent<ImanObjeto>().volviendoAPosicion)
        {
            GameControllerEsterilizacion.Instance.tallaSellada = true;
            GameControllerEsterilizacion.Instance.filtroEnTalla = true;
            GameControllerEsterilizacion.Instance.CheckConditions();
            tapa.transform.parent = this.transform;
        }
    }
    public void ComprobarTallas()
    {
        tallasCerradas++;
        if (tallasCerradas == 4)
        {
            cajaSellada = true;
            img.color = Color.green;
            print("tallas puestas");
            GameControllerEsterilizacion.Instance.tallaCerrada = true;
            GameControllerEsterilizacion.Instance.CheckConditions();
        }
    }

    public void PasarAAutoclave()
    {
        foreach(GameObject go in tallas)
        {
            go.transform.parent = this.transform;
            
        }
        StartCoroutine(IntroducirEnAutoclave());
        GameControllerEsterilizacion.Instance.bandejaEnAutoclave = true;
        GameControllerEsterilizacion.Instance.CheckConditions();
    }
    IEnumerator IntroducirEnAutoclave()
    {
        float tiempo = 0;
        while(tiempo < 2)
        {
            tiempo += Time.deltaTime;
            transform.position = Vector3.Lerp(posAutoclave1.position, posAutoclave2.position, tiempo / 2);
            yield return null;
        }
    }
    public void SacarAutoclave()
    {

        StartCoroutine(SacarContenedor());
        GameControllerEsterilizacion.Instance.fueraAutoclave = true;
        GameControllerEsterilizacion.Instance.CheckConditions();
    }
    public IEnumerator SacarContenedor()
    {
        float tiempo = 0;
        Vector3 posOrig = transform.position;
        while (tiempo < 2)
        {
            tiempo += Time.deltaTime;
            transform.position = Vector3.Lerp(posOrig, posFueraAutoclave.position, tiempo / 2);
            yield return null;
        }
    }
}
