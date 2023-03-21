using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavarManos_PulsarGrifo : MonoBehaviour
{

    public GameObject[] triggersLavarManos;
    public Transform posGrifoInclinado;
    public scr_StepController stp_ctrl;
    bool step;

    private void Start()
    {
        foreach (var item in triggersLavarManos)
        {
            item.SetActive(false);
        }
    }
    public void activarLavarManos()
    {
        foreach (var item in triggersLavarManos)
        {
            item.SetActive(true);
            transform.rotation = posGrifoInclinado.rotation;
        }
        if (!step)
        {
            stp_ctrl.NextStep();
            step = true;
        }
    }
}