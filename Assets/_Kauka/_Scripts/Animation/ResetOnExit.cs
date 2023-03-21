using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetOnExit : StateMachineBehaviour
{
    [Tooltip("The bool you want to change when the animation ends")]
    public string targetBool;
    [Tooltip("Is the bool going to go to true, or false")]
    public bool targetStatus; 


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(targetBool, targetStatus);
    }

    
}
