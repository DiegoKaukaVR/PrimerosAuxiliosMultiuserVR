using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class apoyoborrar : MonoBehaviour
{
    public GameObject[] objetos;
    bool cogiendo;
    Vector3 eulerInicial;
    // Start is called before the first frame update
    void Start()
    {
        eulerInicial = transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            transform.localEulerAngles = eulerInicial + new Vector3(0,10,0);
        }
    }

    //public void Coger()
    //{
    //    cogiendo = true;
    //    foreach(GameObject go in objetos)
    //    {
            
    //        go.transform.position = this.transform.position;
    //    }
    //}
    //public void Soltar()
    //{
    //    cogiendo = false;
    //}
}
