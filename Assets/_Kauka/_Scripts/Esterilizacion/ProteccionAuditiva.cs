using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProteccionAuditiva : MonoBehaviour
{
    ImanObjeto io;
    // Start is called before the first frame update
    void Start()
    {
        io = GetComponent<ImanObjeto>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ComprobarProteccionAuditiva()
    {
        if (io.volviendoAPosicion)
        {
            GameControllerEsterilizacion.Instance.proteccionAuditivaSecado = true;

        }
    }
}
