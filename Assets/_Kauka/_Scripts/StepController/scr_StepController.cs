using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_StepController : MonoBehaviour
{
    [SerializeField]
    private List<scr_Step> steps;

    public int stepIndex = 0;

    private void Awake()
    {
        steps = new List<scr_Step>();

        foreach (var item in GetComponentsInChildren<scr_Step>())
        {
            steps.Add(item);
        }

        InitializeSteps();
    }

    public void InitializeSteps()
    {
        steps[stepIndex].StartStep();
    }

    public void NextStep()
    {
        //if (DatosImportantes.modoForma)
        //{
            steps[stepIndex].EndStep();
            StartCoroutine(TempCambioStep());
        //}

        //stepIndex++;

        //steps[stepIndex].StartStep();


    }

    private IEnumerator TempCambioStep()
    {
        //steps[stepIndex].EndStep();

        yield return new WaitForSecondsRealtime(1.0f);
        stepIndex++;

        steps[stepIndex].StartStep();
    }
}