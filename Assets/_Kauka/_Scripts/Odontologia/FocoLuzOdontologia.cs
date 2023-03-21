using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocoLuzOdontologia : MonoBehaviour
{
    public Transform puntoObjetivo;
    public GameObject foco;
    bool agarrandoFoco;
    public float distanciaMinima;
    public LayerMask lm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (agarrandoFoco)
        {
            CheckFoco();
        }
    }

    public void CheckFoco()
    {
        agarrandoFoco = true;
        //if (Vector3.Angle(foco.transform.up, puntoObjetivo.position) < 10)
        //{
        //    print("Apuntando foco correctamente");
        //}

        RaycastHit hitInfo;
        if (Physics.Raycast(foco.transform.position, foco.transform.up, out hitInfo, 5, lm))
        {
            if(hitInfo.transform.gameObject.tag == "Sillon")
            {
                if(Vector3.Distance(hitInfo.point, puntoObjetivo.position) < distanciaMinima)
                {
                    //print("Enfocando correctamente.");
                    GameControllerOdontologia.Instance.luzColocada = true;
                    GameControllerOdontologia.Instance.CheckConditions();
                }
            }
        }
    }
    public void SoltarFoco()
    {
        agarrandoFoco = false;
    }
}
