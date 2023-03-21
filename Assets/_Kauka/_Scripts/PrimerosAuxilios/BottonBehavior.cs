using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools;

public class BottonBehavior : OnTriggerEvent
{
    HapticController hapticController;
    
    [Header("Botton Configuration")]

    public bool avaibleBotton;
    [SerializeField] bool canBeUsedAgain, hasBeingPressed;
    [SerializeField] float resetTime = .5f;


    private void Start()
    {
        hapticController = GameManager.instance.xrRig.GetComponent<HapticController>();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (hasBeingPressed || !avaibleBotton)
        {
            return;
        }

        if (DetectLayer.LayerInLayerMask(other.gameObject.layer, targetLayer))
        {
            hapticController.SendHaptics(0.2f, 0.2f);
            onEnterTrigger.Invoke();
            BottonOn();
        }
    }

    public void ActivateBotton()
    {
        avaibleBotton = true;
    }

    public void BottonOn()
    {
        hasBeingPressed = true;

        if (canBeUsedAgain)
        {
            StartCoroutine(BottonResetUse());
        }
        else
        {
            avaibleBotton = false;
        }
    }

    IEnumerator BottonResetUse()
    {
        yield return new WaitForSecondsRealtime(resetTime);
        hasBeingPressed = false;
    }



}
