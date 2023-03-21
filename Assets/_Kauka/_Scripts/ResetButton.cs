using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButton : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    bool cycle;
    public void AvaibleButton()
    {
        if (!cycle)
        {
            animator.SetBool("open", true);
        }
        else
        {
            animator.SetBool("open", false);
        }

        cycle = !cycle;
        
    }
}
