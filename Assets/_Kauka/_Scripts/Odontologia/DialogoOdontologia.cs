using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogoOdontologia : MonoBehaviour
{
    public TMP_Text texto;
    public GameObject posOdontologo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = posOdontologo.transform.position;
    }

    public void PlayTexto(int i)
    {
        if (GameControllerOdontologia.Instance.modulo1)
        {
            if (i == 0)
            {

            }
        }
        if (GameControllerOdontologia.Instance.modulo2)
        {
            if (i == 0)
            {

            }
            if (i == 1)
            {

            }
            if (i == 2)
            {

            }
            if (i == 3)
            {

            }
        }
    }

    public void TextoActual(string textoEntrante)
    {
        texto.text = null;
        texto.text = textoEntrante;
        texto.gameObject.transform.position = posOdontologo.transform.position;
    }

}
