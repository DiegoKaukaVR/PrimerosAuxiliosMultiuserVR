using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreparacionCIV : MonoBehaviour
{
    public GameObject polvoCemento;
    public GameObject gotaLiquidoParaCemento;

    public Transform lugarCemento;
    public Transform lugarGota;

    public GameObject triggerParaEspatula;

    bool polvoEchado;
    bool liquidoEchado;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstanciarPolvoCemento()
    {

        if (liquidoEchado)
        {
            triggerParaEspatula.SetActive(true);
        }

        polvoEchado = true;
    }

    public void InstanciarGotaLiquido()
    {

        if (polvoEchado)
        {
            triggerParaEspatula.SetActive(true);
        }

        liquidoEchado = true;
    }
}
