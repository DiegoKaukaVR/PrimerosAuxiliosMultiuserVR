using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

public class AnimatorConfiguration : MonoBehaviour
{
    public Animator animator;


    void Awake()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }
    public void SetBoolTrue(string name)
    {
        animator.SetBool(name, true);
    }
    public void SetBoolFalse(string name)
    {
        animator.SetBool(name, false);
    }

    public void SetTrigger(string name)
    {
        animator.SetTrigger(name);
    }

    public void Resetfloat(string name)
    {
        animator.SetFloat(name, 0);
    }

}
