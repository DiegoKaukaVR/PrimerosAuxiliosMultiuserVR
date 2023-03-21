using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IluminarIconos : MonoBehaviour
{
    public Color colorApagado;
    public Color colorEncendido;
    public bool activoAlPrincipio;
    public bool noDesactivable = false;
    public GameObject objetoSecundario;
    bool activo = false;
    // Start is called before the first frame update
    void Start()
    {
        if (activoAlPrincipio)
        {
            GetComponentInChildren<Image>().color = colorEncendido;
            activo = true;
        }
        else
        {
            GetComponentInChildren<Image>().color = colorApagado;
            activo = false;
        }
    }

    public void ColorIcono()
    {
        if(activo == false) 
        { 
            GetComponentInChildren<Image>().color = colorEncendido; activo = true;
            if (objetoSecundario)
            {
                objetoSecundario.SetActive(true);
            }
        }
        else if(!noDesactivable)
        { 
            GetComponentInChildren<Image>().color = colorApagado; activo = false;
            if (objetoSecundario)
            {
                objetoSecundario.SetActive(false);
            }
        }
    }
    public void ForzarDesactivado()
    {
        GetComponentInChildren<Image>().color = colorApagado; activo = false;
        if (objetoSecundario)
        {
            objetoSecundario.SetActive(false);
        }
    }
    public void ForzarActivado()
    {
        GetComponentInChildren<Image>().color = colorEncendido; activo = true;
        if (objetoSecundario)
        {
            objetoSecundario.SetActive(true);
        }
    }
    public void HoverIcono(bool hover)
    {
        if (hover)
        {
            GetComponentInChildren<Image>().color = colorEncendido;
        }
        else if(!activo)
        {
            GetComponentInChildren<Image>().color = colorApagado;
        }
    }
}
