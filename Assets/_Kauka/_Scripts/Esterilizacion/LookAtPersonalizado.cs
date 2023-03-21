using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPersonalizado : MonoBehaviour
{
    //[Header("SOLO FUNCIONA ACTIVANDO EL EJE Y POR AHORA")]
    public bool EjeX;
    public bool EjeY;
    
    public GameObject objetoASeguir;

    public float followSpeed = 10;
    public Quaternion rotOffset;

    public bool seguirCamara;
    private void Start()
    {
        if (seguirCamara || objetoASeguir == null)
        {
            objetoASeguir = Camera.main.gameObject;
        }

        Vector3 lookPos = objetoASeguir.transform.position - transform.position;
        Quaternion rotac = Quaternion.LookRotation(lookPos);
        transform.rotation = rotac * Quaternion.Inverse(rotOffset);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(EjeX && EjeY)
        {
            Vector3 lookPos = objetoASeguir.transform.position - transform.position;
            Quaternion rotac = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotac * Quaternion.Inverse(rotOffset), Time.deltaTime * followSpeed);
            //transform.LookAt(objetoASeguir.transform.position);
        }
        if(!EjeX && EjeY)
        {
            Vector3 lookPos = objetoASeguir.transform.position - transform.position;
            lookPos.y = 0;
            Quaternion rotac = Quaternion.LookRotation(lookPos);
            //transform.LookAt(objetoASeguir.transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation,rotac * Quaternion.Inverse(rotOffset),Time.deltaTime * followSpeed);
        }
        //if(EjeX && EjeY)
        //{
        //    Vector3 lookPos = objetoASeguir.transform.position - transform.position;
        //    Quaternion rotac = Quaternion.LookRotation(lookPos);
        //    //transform.LookAt(objetoASeguir.transform.position);
        //    transform.rotation = Quaternion.Slerp(transform.rotation, rotac * Quaternion.Inverse(rotOffset), Time.deltaTime * followSpeed);
        //}
    }

    private void OnValidate()
    {
        if (objetoASeguir)
        {
            if (EjeX && EjeY)
            {
                Vector3 lookPos = objetoASeguir.transform.position - transform.position;
                Quaternion rotac = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotac * Quaternion.Inverse(rotOffset), Time.deltaTime * followSpeed);
                //transform.LookAt(objetoASeguir.transform.position);
            }
            if (!EjeX && EjeY)
            {
                Vector3 lookPos = objetoASeguir.transform.position - transform.position;
                lookPos.y = 0;
                Quaternion rotac = Quaternion.LookRotation(lookPos);
                //transform.LookAt(objetoASeguir.transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotac * Quaternion.Inverse(rotOffset), Time.deltaTime * followSpeed);
            }
        }

    }
}
