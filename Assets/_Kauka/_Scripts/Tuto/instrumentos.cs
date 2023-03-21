using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instrumentos : MonoBehaviour
{
    public scr_StepController stp_Ctrl;
    public materialResalte[] materialResalte;
    bool step;

    public void cogerInstrumentoStep()
    {
        if (!step)
        {
            stp_Ctrl.NextStep();
            step = true;

            for (int i = 0; i < materialResalte.Length; i++)
            {
                materialResalte[i].pararMaterialResalte();
            }
        }
    }
    public void iniciarResalteEnStepStart()
    {
        for (int i = 0; i < materialResalte.Length; i++)
        {
            materialResalte[i].comenzarResalteMat();
        }
    }
}
