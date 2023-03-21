using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlesQuimicos : MonoBehaviour
{
    public bool color;
    public bool texture;
    public Color colorSecundario;
    public Material materialSecundario;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CambiarEstado()
    {
        if (color)
        {
            GetComponent<MeshRenderer>().materials[0].color = colorSecundario;
        }
        if (texture)
        {
            GetComponent<MeshRenderer>().materials[0] = materialSecundario;
        }
    }
}
