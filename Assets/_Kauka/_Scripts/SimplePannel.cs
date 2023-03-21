using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SimplePannel : MonoBehaviour
{
    protected Animator animator;
    [SerializeField] protected bool openOnAwake = true;
    [SerializeField] protected bool openOnEnable = true;

    protected bool done;
    [SerializeField] protected bool onlyOnce;
    [SerializeField] protected bool instantShutDown = false;
    protected void Awake()
    {
        animator = GetComponent<Animator>();
        if (openOnAwake)
        {
            animator.SetBool("open", true);
        }
      
    }

    protected void OnEnable()
    {
        if (openOnEnable)
        {
            animator.SetBool("open", true);
        }
        if (onlyOnce && done)
        {
            gameObject.SetActive(false);
        }
    }

    public UnityEvent OnDisbaleEvent;

    public void OpenPannel()
    {
        GetComponent<Animator>().SetBool("open", true);
    }
    public void ClosePannel()
    {
       
        GetComponent<Animator>().SetBool("open", false);
        done = true;
        OnDisbaleEvent.Invoke();

        if (instantShutDown)
        {
            gameObject.SetActive(false);
        }
        else
        {
            Invoke("DisablePannel", 0.2f);
        }
        
    }
    protected void DisablePannel()
    {
        gameObject.SetActive(false);
    }
}
