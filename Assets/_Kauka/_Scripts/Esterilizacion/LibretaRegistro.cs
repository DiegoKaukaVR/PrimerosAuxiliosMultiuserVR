using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LibretaRegistro : MonoBehaviour
{
    List <string> herramientasDisponibles = new List<string>();
    List<string> herramientasGuardadas = new List<string>();
    List<string> herramientasFalladas = new List<string>();
    List<int> controladoresAcertados = new List<int>();
    List<int> controladoresFallados = new List<int>();
    int errores;
    int aciertos;
    public int aciertosDisponibles;
    // Start is called before the first frame update
    void Start()
    {
        herramientasDisponibles.Add("Tijeras");
        aciertosDisponibles = herramientasDisponibles.Count;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AlternarIcono(GameObject but)
    {
       
        if ( but.transform.GetChild(0).gameObject.activeSelf)
        {
            but.transform.GetChild(0).gameObject.SetActive(false);
        }
        else if (!but.transform.GetChild(0).gameObject.activeSelf)
        {
            but.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void CheckHerramienta(string herramienta)
    {
        if (herramientasDisponibles.Contains(herramienta))
        {
            aciertos++;
            herramientasDisponibles.Remove(herramienta);
            herramientasGuardadas.Add(herramienta);
        }
        else if (herramientasGuardadas.Contains(herramienta))
        {
            aciertos--;
            herramientasGuardadas.Remove(herramienta);
            herramientasDisponibles.Add(herramienta);
        }
        else if (herramientasFalladas.Contains(herramienta))
        {
            errores--;
            herramientasFalladas.Remove(herramienta);
        }
        else if(!herramientasDisponibles.Contains(herramienta) && !herramientasGuardadas.Contains(herramienta) && !herramientasFalladas.Contains(herramienta))
        {
            errores++;
            herramientasFalladas.Add(herramienta);
        }
        if (aciertos >= herramientasDisponibles.Count && errores == 0)
        {
            GameControllerEsterilizacion.Instance.materialRegistrado = true;
            GameControllerEsterilizacion.Instance.materialRecibido = true;
            GameControllerEsterilizacion.Instance.CheckConditions();
        }
    }

    public void SumarControladoresQuimicos(int num)
    {
        if(GameControllerEsterilizacion.Instance.controladoresQuimicosCorrectos.Contains(num) && !controladoresAcertados.Contains(num))
        {
            aciertos++;
            controladoresAcertados.Add(num);
        }
        if (GameControllerEsterilizacion.Instance.controladoresQuimicosCorrectos.Contains(num) && controladoresAcertados.Contains(num))
        {
            aciertos--;
            controladoresAcertados.Remove(num);
        }
        if (!GameControllerEsterilizacion.Instance.controladoresQuimicosCorrectos.Contains(num) && controladoresFallados.Contains(num))
        {
            errores--;
            controladoresFallados.Remove(num);
        }
        if (!GameControllerEsterilizacion.Instance.controladoresQuimicosCorrectos.Contains(num) && !controladoresFallados.Contains(num))
        {
            errores++;
            controladoresFallados.Add(num);
        }
        if (aciertos >= GameControllerEsterilizacion.Instance.controladoresQuimicosCorrectos.Count && errores == 0)
        {
            GameControllerEsterilizacion.Instance.comprobarControlesQuimicos = true;
            GameControllerEsterilizacion.Instance.CheckConditions();
        }
    }

}
