using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jabonManos : MonoBehaviour
{
    bool apretando;
    public void jabonOn()
    {
        GetComponent<Animator>().SetBool("uso", true);
        if (!apretando)
        {
            apretando = true;
            GetComponent<Animator>().SetBool("uso", false);
        }
    }
    public void jabonOff()
    {
        apretando = false;
        GetComponent<Animator>().SetBool("uso", false);
    }
}
