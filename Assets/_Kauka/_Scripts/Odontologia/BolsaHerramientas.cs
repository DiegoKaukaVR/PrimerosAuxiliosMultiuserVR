using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolsaHerramientas : MonoBehaviour
{
    public GameObject[] herramientas;
    public Transform[] slotsObjetivo;
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject go in herramientas)
        {
            go.GetComponent<Collider>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ColocarHerramientas()
    {
        if(GetComponent<ImanObjeto>() && GetComponent<ImanObjeto>().volviendoAPosicion)
        {
            for (int i = 0; i < herramientas.Length; i++)
            {
                herramientas[i].transform.parent = slotsObjetivo[i].transform;
                herramientas[i].transform.position = slotsObjetivo[i].transform.position;
                herramientas[i].transform.rotation = slotsObjetivo[i].transform.rotation;
                
            }
        }
    }
}
