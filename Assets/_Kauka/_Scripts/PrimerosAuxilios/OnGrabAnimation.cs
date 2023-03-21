using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

/// <summary>
/// This class allows the player to grab an invisible object and move the object between A and B while reproducing an animation in sync
/// </summary>
public class OnGrabAnimation : MyGrabInteractable
{
    //1 Send Anim Progress 
    [Header("Grab Animation Config")]
    [Space(10)]


    [SerializeField] Animator animator;
    [SerializeField] Transform XRRigTransform;
    [SerializeField] Transform ManiquiTransform;
    Transform originalParent;

    XRSimpleInteractable InteractableEvents;
    XRBaseInteractor xrInteractor;

    [Tooltip("What hand is used to perform the grab interaction")]
    [SerializeField] hand handInteraction;
    enum hand
    {
        Right = 0,
        Left,
        Both
    }

    [SerializeField] XRController rightHandController;
    [SerializeField] XRController leftHandController;

    [SerializeField] bool grabbed;

    [Header("Animation Logic")]
    [SerializeField] bool _activated;
    [SerializeField] bool completed;


 

    [Header("Distances Config")]

    [Tooltip("Distance to reset the animation process")]
    [SerializeField] float maxRangeInteraction = 1.5f;

    [SerializeField] float maxDistObject = 1.5f;

    Vector3 originalPos;
    Vector3 currentPos;

    public Transform[] points;
   

    [SerializeField]
    public Vector3 offSetPosA;
    public Vector3 posA;
    [SerializeField]
    public Vector3 offSetPosB;
    public Vector3 posB;

    Vector3 dirTarget;
    float distTarget;
    float actualDist;

    [Min(0)]
    float progress;

    [SerializeField] bool debugProgress = false;


    public UnityEvent onCompleteEvent;

    bool freeze;

    Transform point1;
    Transform point2;

    bool started = false;
    private void Start()
    {
        if (XRRigTransform == null)
            XRRigTransform = FindObjectOfType<CharacterController>().transform;

      
        point1 = GetComponentsInChildren<Transform>()[0];
        point2 = GetComponentsInChildren<Transform>()[1];

        transform.parent = ManiquiTransform;
        originalParent = transform.parent;
        originalPos = transform.position;
        posA = transform.localPosition + offSetPosA; 
        posB = transform.localPosition + offSetPosB;
        point1.localPosition = posA;
        point2.localPosition = posB;
        dirTarget = posB - posA;
        distTarget = dirTarget.magnitude;
       
        StartCoroutine(CoroutineHoldObject());

        animator.SetFloat(ProgressName, 0);
        Invoke("LazyStart", 0.15f);


    }

    void LazyStart()
    {
        OnEnable();
        FeedbackPosition();
        
      
    }




    #region Events Grab - EnterSelect / ExitSelect

    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
       
        xrInteractor = interactor;
        XRController xrController = interactor.GetComponent<XRController>();


        //Condición de mano

        if (handInteraction != hand.Both)
        {
            if (handInteraction == hand.Left && xrController != leftHandController)
                return;
            if (handInteraction == hand.Right && xrController != rightHandController)
                return;
        }

        base.OnSelectEntered(interactor);
        OnEnterSelected();
    }
    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        base.OnSelectExited(interactor);


        //Condicion de mano
        OnExitSelected();
    }

    void OnEnterSelected()
    {
        freeze = true;
        _activated = true;
        animator.SetBool("Activated", true);
        grabbed = true;
        Debug.Log("Enter Select");

        StopCoroutine("CoroutineHoldObject");
        StartCoroutine(CoroutineHoldObject());
    }
    void OnExitSelected()
    {
        freeze = false;
        grabbed = false;

        if (!CheckComplete())
        {
            if (IsInRangeToInteract())
            {
                StopCoroutine("CoroutineResetObject");
                StartCoroutine(CoroutineResetObject());
            }   
        }
        else
            _activated = false;


        Debug.Log("Exit Select");
    }

    #endregion

    #region Funtions

    IEnumerator CoroutineHoldObject()
    {
        while (grabbed)
        {
            //Dist A
            actualDist = Vector3.Distance(posA, transform.position);

            if (actualDist > maxDistObject)
            {
                Debug.Log("Object too far from origin, object and animation is reset");
               
                ResetGrabLogic();
                yield break;
            }
            else
            {
                CalculateProgress();
                yield return null;
            }
        }
    }

    public string ProgressName = "Progress";

    private void CalculateProgress()
    {
        //Dist B
        actualDist = Vector3.Distance(posB, transform.position);

        // Calculate progress
       
        progress = (distTarget - actualDist) / distTarget;


        if (progress < 0) progress = 0;
        if (progress > 1) progress = 1;
        //if (debugProgress) Debug.Log("Calculating progress... " + progress);

        // Send Value to Anim
        animator.SetFloat(ProgressName, progress);
    }


    /// <summary>
    /// Checks if the player has walked away and if so reset logic
    /// </summary>
    bool IsInRangeToInteract()
    {
        if (Vector3.Distance(originalPos, XRRigTransform.position) > maxRangeInteraction)
        {
            Debug.Log("Player too far, object and animation is reset");
            animator.SetBool("Activated", false);
            ResetGrabLogic();
            return false;
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// Checks if the invisible object has reached final position and if so complete with sucess result
    /// </summary>
    bool CheckComplete()
    {
        if (Vector3.Distance(transform.position, posB) < 0.10f)
        {
            Debug.Log("Completed!!");
            completed = true;
            animator.SetTrigger("Completed");
            OnComplete();
            return true;
        }
        else
            return false;
        
    }

    void OnComplete()
    {
        GameManager.instance.audioManager.PlayComplete();
      
        onCompleteEvent.Invoke();
        this.gameObject.SetActive(false);
    }

    void ResetGrabLogic()
    {
        ResetPosition();
        ResetProgress();

        grabbed = false;
        completed = false;
        _activated = false;
        animator.SetBool("Activated", false);
        transform.parent = originalParent;
    }
    void ResetPosition()
    {
        transform.position = originalPos;
    }
    void ResetProgress()
    {
        progress = 0;
        animator.SetFloat("Progress", progress);
    }

    /// <summary>
    /// Checks if the player has gone too far and if so reset all logic
    /// </summary>
    IEnumerator CoroutineResetObject()
    {
        while (!completed)
        {
            if (Vector3.Distance(originalPos, XRRigTransform.position) > maxRangeInteraction)
            {
                Debug.Log("Player too far, object and animation is reset");
                animator.SetBool("Activated", false);
                ResetGrabLogic();
                yield break;
            }
            else
            {
                yield return null;
            }
        }      
    }




    #endregion

    [SerializeField] bool disable;
    protected override void OnDisable()
    {
        base.OnDisable();
        disable = true;
    }

    //private void OnValidate()
    //{
    //    originalPos = transform.position;
    //    posA = originalPos + offSetPosA;
    //    posB = originalPos + offSetPosB;

    //    if (efectoLinea)
    //    {
    //        efectoLinea.LazyStart();
    //    }
       
    //}

   

    public EfectoLineaPersonalizado efectoLinea;
    protected override void OnEnable()
    {
        base.OnEnable();

        originalPos = transform.position;
        posA = originalPos + offSetPosA;
        posB = originalPos + offSetPosB;
    }


    void FeedbackPosition()
    {
        if (efectoLinea != null)
        {
            efectoLinea.parentGO.SetActive(true);
            efectoLinea.LazyStart();
        }
    }


    #region Debug

    [SerializeField] bool debugMaxRange;

    private void OnDrawGizmos()
    {
        if (disable ==true)
        {
            return;
        }
        Gizmos.color = new Color(0, 1, 0, .5f);

        if (!freeze)
        {

            posA = originalPos + offSetPosA;
            posB = originalPos + offSetPosB;
        }
     



        Gizmos.DrawCube(posA, Vector3.one*.1f);
        Gizmos.DrawCube(posB, Vector3.one*.1f);
        Gizmos.DrawCube(transform.position, Vector3.one * 0.05f);

        Gizmos.DrawLine(posA, posB);

        if (debugMaxRange)
                Gizmos.DrawWireSphere(posA, maxRangeInteraction);
    }
    #endregion

  

}
