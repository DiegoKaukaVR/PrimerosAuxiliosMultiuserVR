using System.Collections;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.SpatialTracking;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;


/// <summary>
/// This is used in Heimlich:
/// Player must be next to intial position, then second camera is activated and should presh both triggers around the invisible interactable to perform Heimlich maneuver. Both animations should reproduce in sync
/// </summary>
[CanSelectMultiple(true)]
public class OnTwoGrabAnimation : XRGrabInteractable
{


    protected SphereCollider sphereCollider;
    [SerializeField] protected Animator animator;

    [Header("2 Hands Configuration")]
    [Space(10)]

    [SerializeField] protected Transform LeftHandTransform;
    [SerializeField] protected Transform RightHandTransform;

    [SerializeField] protected float maxDistBetweenHands = 0.4f;
    [Tooltip("Aceleración a la que empieza a considerar que es un pusheo")]
    [SerializeField] protected float thresholdStartPushingAcc;
    [Tooltip("Distancia mínima para detectar que ha llegado al final del pusheo (a la posicion de destino)")]
    [SerializeField] protected float minDistValidTarget;
    protected bool pushing;

    [SerializeField] protected int maxSuccess = 30;
    [SerializeField] protected int actualSuccess;

    protected bool grabbed;
    protected bool started;
    protected bool completed;


    [Header("Player configuration")]
    [Space(10)]
    [SerializeField] protected XROrigin _XRRig;
    [SerializeField] protected TrackCameraPosition trackCamera;

    protected ControllerVelocity controllerVelocity;
    protected Transform XRRigtransform;
    protected JoystickMovementController joystickMovementController;
    protected LocomotionController locomotionController;
    protected HapticController hapticController;
    protected TrackedPoseDriver trackedPoseDriver;

    protected bool originalTP;
    protected bool originalJoystick;

    [Header("Interaction Path")]
    [Space(10)]

    [Tooltip("(0 - 1) being 0 started position and 1 pathway completed")]
    protected float progress;
    protected float actualDist;
    protected float distTarget;

    [SerializeField] protected Transform bodyTransformAttached;
    [SerializeField] protected Vector3 offSetBodyAttached;
    protected Vector3 originalPos;
    protected Transform originalTransformParent;


    protected Vector3 posA, posB;
    [SerializeField] protected Vector3 offSetA, offSetB;

    [SerializeField] UnityEvent onCompleteEvent;


    public bool onlyOneHand;


    protected virtual void Start()
    {
        _XRRig = GameManager.instance.xrRig;

        trackedPoseDriver = _XRRig.GetComponentInChildren<TrackedPoseDriver>();
        controllerVelocity = _XRRig.GetComponent<ControllerVelocity>();
        joystickMovementController = _XRRig.GetComponent<JoystickMovementController>();
        locomotionController = _XRRig.GetComponent<LocomotionController>();
        hapticController = _XRRig.GetComponent<HapticController>();
        trackCamera = _XRRig.GetComponentInChildren<TrackCameraPosition>();
        sphereCollider = GetComponent<SphereCollider>();

        trackPosition = false;
        originalTransformParent = transform.parent;
        transform.parent = bodyTransformAttached;
        transform.localPosition = Vector3.zero;

        posA = bodyTransformAttached.position + offSetA;
        posB = bodyTransformAttached.position + offSetB;
        distTarget = Vector3.Distance(posA, posB);
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        avaiblePush = true;

        originalPos = transform.position;
        posA = bodyTransformAttached.position + offSetA;
        posB = bodyTransformAttached.position + offSetB;
    }

    #region Start

    /// <summary>
    /// Disable or enable Head track position and readjust camera
    /// </summary>
    protected void TrackPose(bool value)
    {
        if (!value)
        {
            trackCamera.RegisterCameraPos();
            trackedPoseDriver.trackingType = TrackedPoseDriver.TrackingType.RotationOnly;
            trackCamera.SetCameraOffSet();
        }
        else
        {
            trackCamera.ResetCameraOffSet();
            trackedPoseDriver.trackingType = TrackedPoseDriver.TrackingType.RotationAndPosition;
        }
    }

    /// <summary>
    /// Disable or enable player movement
    /// </summary>
    protected void PlayerMovement(bool value)
    {
        if (value)
        {
            joystickMovementController.enableMovementJoy = originalJoystick;
            locomotionController.enableTP = originalTP;
        }
        else
        {
            joystickMovementController.enableMovementJoy = value;
            locomotionController.enableTP = value;
        }

    }

    #endregion

    #region End/Completed/Failed

    /// <summary>
    /// Override to create funtionality when the task is finished
    /// </summary>
    public virtual void EndLogic()
    {
        Debug.Log("End Logic");
    }
    protected virtual void CompletedTask()
    {
        onCompleteEvent.Invoke();
        EndLogic();
        Debug.Log("Task has being completed.");
    }
    protected virtual void FailedTask()
    {
        EndLogic();
        Debug.Log("Task has being failed.");
    }


    #endregion

    #region ResetLogic
    protected void ResetGrabLogic()
    {
        ResetPosition();
        ResetProgress();
        ResetTransform();

        grabbed = false;
        completed = false;
        started = false;
        animator.SetBool("Activated", false);
    }
    protected void ResetPosition()
    {
        transform.position = originalPos;
    }
    protected void ResetLocalPosition()
    {
        ResetTransform();
        transform.localPosition = offSetBodyAttached;
    }
    protected void ResetProgress()
    {
        progress = 0;
        animator.SetFloat("Progress", progress);
    }
    protected void ResetTransform()
    {
        transform.parent = bodyTransformAttached;
    }

    #endregion

    #region Interaction

    public bool CanBeGrabbed = true;

    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        base.OnSelectEntered(interactor);
        if (!CanBeGrabbed)
        {
            return;
        }
        OnEnterSelected();
    }
    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        base.OnSelectExited(interactor);
        OnExitSelected();

    }

    public UnityEvent On2HandsEvent;
    public UnityEvent On1HandEvent;
    protected virtual void OnEnterSelected()
    {
        Debug.Log("Interactors Selecting: " + interactorsSelecting.Count);

        if (!onlyOneHand)
        {
            if (HasMultipleInteractors() && CheckHandsConditions())
            {
                On2HandsEvent.Invoke();
                Debug.Log("Grabbed");
                trackPosition = true;
                grabbed = true;
                animator.SetBool("Activated", true);
                StopCoroutine(CoroutinePushCheck());
                StartCoroutine(CoroutinePushCheck());
            }
        }
        else
        {
            On1HandEvent.Invoke();
            Debug.Log("Grabbed");
            trackPosition = true;
            grabbed = true;
            animator.SetBool("Activated", true);
            StopCoroutine(CoroutinePushCheck());
            StartCoroutine(CoroutinePushCheck());
        }
      
    }

    protected virtual void OnExitSelected()
    {
        Debug.Log("Interactors Selecting: " + interactorsSelecting.Count);
        ResetLocalPosition();
        animator.SetBool("Activated", false);
        pushing = false;
        grabbed = false;

       

        if (HasNoInteractors())
        {
            trackPosition = false;
        }
    }


    bool HasMultipleInteractors() { return interactorsSelecting.Count > 1; }
    bool HasNoInteractors() { return interactorsSelecting.Count == 0; }



    #endregion


    #region 2HandPushMechanic

    /// <summary>
    /// Checks hands distance to grabbable and also if hands are linked together
    /// </summary>
    bool CheckHandsConditions()
    {
        //if (Vector3.Distance(LeftHandTransform.position, transform.position) < minDistGrabBall
        //|| Vector3.Distance(RightHandTransform.position, transform.position) < minDistGrabBall)
        //    return false;

        if (Vector3.Distance(LeftHandTransform.position, RightHandTransform.position) > maxDistBetweenHands)
        {
            Debug.DrawLine(LeftHandTransform.position, RightHandTransform.position, Color.red);
            return false;
        }


        Debug.DrawLine(LeftHandTransform.position, RightHandTransform.position, Color.green);
        return true;
    }


    Vector3 dirBetweenHands;
    float distBetweenHands;

    Vector3 leftHandAcc;
    Vector3 righthandAcc;
    Vector3 finalHandAccLeft;
    Vector3 finalHandAccRight;
    Vector3 dirToObjectiveLeft;
    Vector3 dirToObjectiveRight;
    float totalAcceleration;

    float GetAccelerationToObjetive()
    {
        leftHandAcc = controllerVelocity.GetLeftHandAcceleration();
        righthandAcc = controllerVelocity.GetRightHandAcceleration();

        dirToObjectiveLeft = transform.position - LeftHandTransform.position;
        dirToObjectiveLeft.Normalize();

        dirToObjectiveRight = transform.position - RightHandTransform.position;
        dirToObjectiveRight.Normalize();

        finalHandAccLeft = new Vector3(dirToObjectiveLeft.x * leftHandAcc.x, dirToObjectiveLeft.y * leftHandAcc.y, dirToObjectiveLeft.z * leftHandAcc.z);
        finalHandAccRight = new Vector3(dirToObjectiveRight.x * righthandAcc.x, dirToObjectiveRight.y * righthandAcc.y, dirToObjectiveRight.z * righthandAcc.z);

        if (finalHandAccLeft.magnitude > finalHandAccRight.magnitude)
        {
            return finalHandAccLeft.magnitude;
        }
        else
        {
            return finalHandAccRight.magnitude;
        }
    }

    float GetAccelerationToObjetiveOpposite()
    {
        leftHandAcc = controllerVelocity.GetLeftHandAcceleration();
        righthandAcc = controllerVelocity.GetRightHandAcceleration();

        dirToObjectiveLeft = transform.position - LeftHandTransform.position;
        dirToObjectiveLeft.Normalize();
        dirToObjectiveLeft = -dirToObjectiveLeft;

        dirToObjectiveRight = transform.position - RightHandTransform.position;
        dirToObjectiveRight = -dirToObjectiveRight;
        dirToObjectiveRight.Normalize();

        finalHandAccLeft = new Vector3(dirToObjectiveLeft.x * leftHandAcc.x, dirToObjectiveLeft.y * leftHandAcc.y, dirToObjectiveLeft.z * leftHandAcc.z);
        finalHandAccRight = new Vector3(dirToObjectiveRight.x * righthandAcc.x, dirToObjectiveRight.y * righthandAcc.y, dirToObjectiveRight.z * righthandAcc.z);

        if (finalHandAccLeft.magnitude > finalHandAccRight.magnitude)
        {
            return finalHandAccLeft.magnitude;
        }
        else
        {
            return finalHandAccRight.magnitude;
        }
    }

    public float timeReset = 0.3f;
    bool resetPos = false;

    bool needToReset;
    float acc;
    float dist;
    protected IEnumerator CoroutinePushCheck()
    {
        while (grabbed)
        {
            if (needToReset)
            {
                if (resetPos == false)
                {
                    if (transform.position.y >= (posA.y - 0.1f))
                    {
                        needToReset = false;
                        resetPos = true;
                    }
                    else
                    {
                        yield return null;
                    }
                }

            }

            acc = GetAccelerationToObjetive();
            //Debug.Log("Acc to objetive" + acc);

            if (acc > thresholdStartPushingAcc)
            {
                StartPush();
            }

            Vector3 newPoint = new Vector3(transform.position.x, posB.y, transform.position.z);
            dist = Vector3.Distance(transform.position, newPoint);

            if ((pushing && acc > thresholdStartPushingAcc) || (pushing && dist < minDistValidTarget))
            {
                EndPush();
            }

            //CalculateProgress();

            yield return null;
        }

     
    }


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
        animator.SetFloat("Progress", progress);
    }


    /// <summary>
    /// Returns Middle position between hands
    /// </summary>
    /// <returns></returns>
    Vector3 GetMiddlePointHands()
    {
        dirBetweenHands = LeftHandTransform.position - RightHandTransform.position;
        distBetweenHands = dirBetweenHands.magnitude;

        return LeftHandTransform.position + dirBetweenHands.normalized * distBetweenHands;
    }

    /// <summary>
    /// When hands are prepared to make the push
    /// </summary>
    protected virtual void StartPush()
    {
        pushing = true;
       
        animator.SetBool("Push", false);
        animator.SetBool("FailedPush", false);
        Debug.Log("Start Push");
    }

   
    float SuccessTime = 0.25f;
    bool avaiblePush = true;

    void ResetSuccesAvaible()
    {
        avaiblePush = true;
    }
    /// <summary>
    /// When hands have finished their push wether is completed or failed
    /// </summary>
    void EndPush()
    {
        pushing = false;

        if (acc > thresholdStartPushingAcc) 
        {
            if (!avaiblePush)
            {
                return;
            }
            avaiblePush = false;
            SuccessPush(); 
            Debug.Log("End Push: Success"); 
            needToReset = true; 
           
            Invoke("ResetSuccesAvaible", SuccessTime);
        }
        else 
        { 
            //FailedPush(); 
            Debug.Log("End Push: Failure");
            if (!avaiblePush)
            {
                return;
            }
        }

    }

    /// <summary>
    /// Sobreescribir desde el hijo y comprobar la frecuencia 
    /// </summary>
    protected virtual void SuccessPush()
    {
        animator.SetBool("Push", true);
        actualSuccess++;
        Debug.Log("SuccessPush");
        GameManager.instance.audioManager.PlayComplete();
        hapticController.SendHaptics();

        if (actualSuccess >= maxSuccess)
        {
            StopCoroutine(CoroutinePushCheck());
            actualSuccess = 0;
            animator.SetTrigger("Completed");
            CompletedTask();
           
            Debug.Log("Completed");
        }
       
    }

    void FailedPush()
    {
        GameManager.instance.audioManager.PlayFail();
        animator.SetBool("FailedPush", true);
        Debug.Log("FailedPush");
    }



    #endregion




    private void OnValidate()
    {
        //if (bodyTransformAttached == null) { Debug.LogWarning("Add originalTransformObject"); return; }
        //else
        //{
        //    transform.position = bodyTransformAttached.position + offSetA;
        //    offSetA = transform.position - bodyTransformAttached.position;

        //}
        
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = new Color(0, 1, 0, .5f);

        //if (bodyTransformAttached == null) { Debug.LogWarning("Add originalTransformObject"); return; }
        //else
        //    offSetA = transform.position - bodyTransformAttached.position;

        //posA = bodyTransformAttached.position + offSetA;
        //posB = bodyTransformAttached.position + offSetB;

        //Gizmos.DrawCube(posA, Vector3.one * .1f);
        //Gizmos.DrawCube(posB, Vector3.one * .1f);
        //Gizmos.DrawLine(posA, posB);

        //Gizmos.color = new Color(1, 0, 0, .5f);
        //Gizmos.DrawCube(transform.position, Vector3.one * 0.05f);
    }

}