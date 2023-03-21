using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAnimatorState : MonoBehaviour
{
    [SerializeField] Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();       
    }

    public void ChangeAnimatorStateTrue(string pharameterName)
    {
        animator.SetBool(pharameterName, true);
    }
    public void ChangeAnimatorStateFalse(string pharameterName)
    {
        animator.SetBool(pharameterName, false);
    }
}
