using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorPrendas : MonoBehaviour
{
    List<ImanObjeto> prendas = new List<ImanObjeto>();
    int prendasColocadas = 0;
    // Start is called before the first frame update
    void Start()
    {
        foreach(ImanObjeto io in GetComponentsInChildren<ImanObjeto>())
        {
            prendas.Add(io);
        }  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CheckPrendas()
    {
        foreach (ImanObjeto go in prendas)
        {
            if (go.volviendoAPosicion)
            {
                prendasColocadas++;
                if (FindObjectOfType<GameControllerEsterilizacion>())
                {
                    GameControllerEsterilizacion.Instance.prendaUnicaPuesta = true;
                    GameControllerEsterilizacion.Instance.CheckConditions();
                }
            }
        }

        if (prendasColocadas >= prendas.Count)
        {
            if (FindObjectOfType<GameControllerEsterilizacion>())
            {
                GameControllerEsterilizacion.Instance.prendasPuestas = true;
                GameControllerEsterilizacion.Instance.CheckConditions();
            }
            if (FindObjectOfType<GameControllerOdontologia>())
            {
                GameControllerOdontologia.Instance.episColocados = true;
                GameControllerOdontologia.Instance.CheckConditions();
            }
            Destroy(this);
        }
        else
        {
            prendasColocadas = 0;
        }
    }
}
