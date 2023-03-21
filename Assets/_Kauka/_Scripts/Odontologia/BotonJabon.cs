using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BotonJabon : MonoBehaviour
{
    Vector3 posInicial;
    public float distanciaActivado;
    public GameObject gotaJabon;
    public Slider slider;
    public TMP_Text text;
    public Transform puntoJabon;
    bool puedeGota;
    GameObject gota;
    // Start is called before the first frame update
    void Start()
    {
        posInicial = transform.position;
        puedeGota = true;
    }

    public void InteractuarJabon()
    {
        if(Mathf.Abs(transform.position.y - posInicial.y) > distanciaActivado && puedeGota)
        {
            if(gota != null)
            {
                Destroy(gota);
            }
            gota = Instantiate(gotaJabon, puntoJabon.position, Quaternion.identity, null);
            gota.GetComponentInChildren<MedidoresJabon>().sliderProgreso = slider;
            gota.GetComponentInChildren<MedidoresJabon>().textoProgreso = text;
            gota.GetComponentInChildren<MedidoresJabon>().gotaPadre = gota;
            puedeGota = false;
            print("Echando Jabon");
        } 

        if(puedeGota == false && transform.position == posInicial)
        {
            puedeGota = true;
        }
    }

    private void OnCollision(Collision collision)
    {
       
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Hand")
        {
            InteractuarJabon();
        }
    }

}
