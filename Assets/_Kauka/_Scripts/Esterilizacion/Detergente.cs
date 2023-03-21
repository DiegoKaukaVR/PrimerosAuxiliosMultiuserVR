using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detergente : MonoBehaviour
{
    public int pourThreshold = 45;
    public Transform origin;
    public GameObject liquidoDetergente;

    Vector3 rotInicial;
    private bool isPouring = false;
    bool cogiendo;
    private void Start()
    {
        liquidoDetergente.GetComponent<ParticleSystem>().Stop();
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
        liquidoDetergente.GetComponent<ParticleSystem>().Stop();
        cogiendo = false;
    }

    void StartPour()
    {
        liquidoDetergente.GetComponent<ParticleSystem>().Play();
    }

    void EndPour()
    {
        liquidoDetergente.GetComponent<ParticleSystem>().Stop();
    }

    float CalculatePourAngle()
    {
        return Vector3.Angle(transform.up, Vector3.up); 
    }


}
