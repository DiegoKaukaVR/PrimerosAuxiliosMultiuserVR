using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class ControllerStepSimulation : MonoBehaviour
{
    [SerializeField] Process process;

    private void Update()
    {
        Keyboard kb = InputSystem.GetDevice<Keyboard>();
        if (kb.spaceKey.wasPressedThisFrame)
        {
            process.FinishStepEvent();
            Debug.Log("Step simulated");
        }
    }
}
