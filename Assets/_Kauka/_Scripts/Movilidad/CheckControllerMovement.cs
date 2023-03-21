using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;
using UnityEngine.Events;

public class CheckControllerMovement : MonoBehaviour
{
    XRController grab;
    Vector3 posAnterior;
    Vector3 posNueva;
    public bool subiendo;
    public bool bajando;

    bool toggle;
    // Start is called before the first frame update
    void Start()
    {
        grab = GetComponent<XRController>();
        posAnterior = transform.position;
        posNueva = transform.position;
        //toggle = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (toggle)
        {
            if (Vector3.Distance(transform.position, posNueva) > 0.03f)
            {
                posNueva = transform.position;
                if (posNueva.y > posAnterior.y)
                {
                    subiendo = true;
                    bajando = false;
                    GameControllerMovilidad.Instance.mandosSubiendo = true; 
                    GameControllerMovilidad.Instance.mandosBajando = false;


                }
                if (posNueva.y < posAnterior.y)
                {
                    subiendo = false;
                    bajando = true;
                    GameControllerMovilidad.Instance.mandosSubiendo = false;
                    GameControllerMovilidad.Instance.mandosBajando = true;
                }

               posAnterior = transform.position;
                StartCoroutine(Reset());
            }
        }
    }
    IEnumerator Reset()
    {
        yield return new WaitForSeconds(0.2f);
        subiendo = false;
        bajando = false;
        GameControllerMovilidad.Instance.mandosSubiendo = false;
        GameControllerMovilidad.Instance.mandosBajando = false;
    }
}
