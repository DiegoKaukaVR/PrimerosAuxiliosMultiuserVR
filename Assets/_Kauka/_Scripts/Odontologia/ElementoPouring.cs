using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementoPouring : MonoBehaviour
{
    public int pourThreshold = 45;
    public MonoBehaviour scriptActivable;
    Vector3 rotInicial;
    private bool isPouring = false;
    bool cogiendo;
    private void Start()
    {
        scriptActivable.enabled = false;
        rotInicial = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if (cogiendo)
        {
            bool pourCheck = CalculatePourAngle() > pourThreshold;
            if (isPouring != pourCheck)
            {
                isPouring = pourCheck;
                if (isPouring)
                {
                    StartPour();
                }
                else
                {
                    EndPour();
                }
            }
        }
    }

    public void CogerBotella()
    {
        cogiendo = true;
    }

    public void SoltarBotella()
    {
        
        cogiendo = false;
    }

    void StartPour()
    {
        scriptActivable.enabled = true;
    }

    void EndPour()
    {
        scriptActivable.enabled = false;
    }

    float CalculatePourAngle()
    {
        return Vector3.Angle(transform.up, Vector3.up);
    }
}
