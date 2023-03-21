using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using UnityEngine.SpatialTracking;
using UnityEngine.Events;


/// <summary>
/// This is used in Heimlich:
/// Player must be next to intial position, then second camera is activated and should presh both triggers around the invisible interactable to perform Heimlich maneuver. Both animations should reproduce in sync
/// </summary>
[CanSelectMultiple(true)]
public class OnTwoGrabAnimationHeimlich : XRGrabInteractable
{
    [Header("Heimlich Configuration")]
    [Space(10)]

    [SerializeField] GameObject secondCameraProjection;
    [SerializeField] Animator animator;


    [Header("2 Hands Configuration")]
    [Space(10)]

    [SerializeField] Transform LeftHandTransform;
    [SerializeField] Transform RightHandTransform;

    [SerializeField] float maxDistBetweenHands = 0.4f;
    [SerializeField] float thresholdStartPushingAcc;
    [SerializeField] float minDistValidTarget;
    [HideInInspector] public bool pushing;

    [SerializeField] int maxSuccess = 4;
    [SerializeField] int actualSuccess;


    [Tooltip("(0 - 1) being 0 started position and 1 pathway completed")]
    float progress;
    protected float actualDist;
    protected float distTarget;

    bool started;
    bool grabbed;
    bool completed;

    [Header("Player configuration")]
    [Space(10)]

    [SerializeField] GameObject XRRig;
    TrackedPoseDriver trackedPoseDriver;
    ControllerVelocity controllerVelocity;
    [SerializeField] Transform XRRigtransform;
    [SerializeField] TrackCameraPosition trackCamera;
    JoystickMovementController joystickMovementController;
    LocomotionController locomotionController;
    HapticController hapticController;
    bool originalTP;
    bool originalJoystick;



    [Tooltip("Position that player must be in order to perform Heimlich")]
    [SerializeField] Transform initialPlayerPos;
    [SerializeField] Vector3 offSetSnapPos;

    [Header("Interaction Path")]
    [Space(10)]

    [SerializeField] Transform bodyTransformAttached;
    [SerializeField] Vector3 offSetBodyAttached;
    Vector3 originalPos;
    Transform originalTransformParent;

    Vector3 posA, posB;
    [SerializeField] Vector3 offSetA, offSetB;

    [SerializeField] UnityEvent OnComplete;

    public PannelCounter pannelCounter;



    private void Start()
    {
        XRBaseInteractor interactor = selectingInteractor;
        IXRSelectInteractor newInteractor = firstInteractorSelecting;
        List<IXRSelectInteractor> moreInteractors = interactorsSelecting;
        trackedPoseDriver = XRRig.GetComponentInChildren<TrackedPoseDriver>();
        controllerVelocity = XRRig.GetComponent<ControllerVelocity>();

        joystickMovementController = XRRig.GetComponent<JoystickMovementController>();
        locomotionController = XRRig.GetComponent<LocomotionController>();
        hapticController = XRRig.GetComponent<HapticController>();
        SetSecondCamera(false);

        trackPosition = false;
        originalTransformParent = transform.parent;
        transform.parent = bodyTransformAttached;


        posA = bodyTransformAttached.position + offSetA;
        posB = bodyTransformAttached.position + offSetB;
        distTarget = Vector3.Distance(posA, posB);


       

        originalJoystick =  joystickMovementController.enableMovementJoy;
        originalTP = locomotionController.enableTP;

    }

    #region Start
    public void StartLogic()
    {
        Debug.Log("Start Logic");
        SnapPlayerPos();
        PlayerMovement(false);
        SetSecondCamera(true);
    }

    public float lerpTime = 1;
    public Transform fixedRot;
    void SnapPlayerPos()
    {
        // lerp Position
        //StartCoroutine(LerpPosition(initialPlayerPos.position + offSetSnapPos, lerpTime));
        //StartCoroutine(LerpRotation(initialPlayerPos.rotation, lerpTime));

    }

    /// <summary>
    /// Condicion de estar mirando en la dirección del cuerpo!!!
    /// </summary>

    IEnumerator LerpPosition(Vector3 targetPos, float duration)
    {
        float time = 0;
        Vector3 startPosition = XRRigtransform.position;

        while (time < duration)
        {
            XRRigtransform.position = Vector3.Lerp(startPosition, targetPos - trackCamera.GetDirToPlayerFromRig().normalized * trackCamera.GetDistancePlayerToRig(), time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        XRRigtransform.position = targetPos - trackCamera.GetDirToPlayerFromRig().normalized * trackCamera.GetDistancePlayerToRig();
        TrackPose(false);
    }

    IEnumerator LerpRotation(Quaternion targetRotation, float duration)
    {
        float timeRot = 0;
        Quaternion startRot = XRRigtransform.rotation;

        while (timeRot < duration)
        {
            XRRigtransform.rotation = Quaternion.Lerp(startRot, targetRotation, timeRot / duration);
            timeRot += Time.deltaTime;
            yield return null;
        }

    }

    void SetSecondCamera(bool value)
    {
        secondCameraProjection.SetActive(value);
    }
    void TrackPose(bool value)
    {
        //if (!value)
        //{
        //    trackCamera.RegisterCameraPos();
        //    trackedPoseDriver.trackingType = TrackedPoseDriver.TrackingType.RotationOnly;
        //    trackCamera.SetCameraOffSet();
        //}
        //else
        //{
        //    trackCamera.ResetCameraOffSet();
        //    trackedPoseDriver.trackingType = TrackedPoseDriver.TrackingType.RotationAndPosition;
        //}
    }
    void PlayerMovement(bool value) 
    {
        //if (value)
        //{
        //    GameManager.instance.EnableTurn(value);
        //    joystickMovementController.enableMovementJoy = originalJoystick;
        //    locomotionController.enableTP = originalTP;  
        //}
        //else
        //{
        //    GameManager.instance.EnableTurn(value);
        //    joystickMovementController.enableMovementJoy = value;
        //    locomotionController.enableTP = value;
        //}

    }

    #endregion

    #region End/Completed/Failed
    public void EndLogic()
    {
        Debug.Log("End Logic");
        PlayerMovement(true);
        SetSecondCamera(false);
        TrackPose(true);
    }

    void CompletedTask()
    {
        OnComplete.Invoke();
        EndLogic();
    }
    void FailedTask()
    {

    }
    #endregion

    #region ResetLogic
    void ResetGrabLogic()
    {
        ResetPosition();
        ResetProgress();
        ResetTransform();

        grabbed = false;
        completed = false;
        started = false;
        animator.SetBool("Activated", false);
    }
    void ResetPosition()
    {
        transform.position = originalPos;
    }

    void ResetLocalPosition()
    {
        ResetTransform();
        transform.localPosition = offSetBodyAttached;
    }
    void ResetProgress()
    {
        progress = 0;
        animator.SetFloat("Progress", progress);
    }
    void ResetTransform()
    {
        transform.parent = bodyTransformAttached;
    }

    #endregion

    #region Interaction

    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        base.OnSelectEntered(interactor);
        OnEnterSelected();
    }

    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        base.OnSelectExited(interactor);
        OnExitSelected();

    }

    public UnityEvent On2HandsEnter;


    void OnEnterSelected()
    {
        Debug.Log("Interactors Selecting: " + interactorsSelecting.Count);

        if (HasMultipleInteractors() && CheckHandsConditions())
        {
            On2HandsEnter.Invoke();
            Debug.Log("Grabbed");
            trackPosition = true;
            grabbed = true;
            animator.SetBool("Activated", true);
            StopCoroutine(CoroutinePushCheck());
            StartCoroutine(CoroutinePushCheck());
        }
    }

    void OnExitSelected()
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
        dirToObjectiveLeft = new Vector3(Mathf.Clamp(dirToObjectiveLeft.x, 0, 10f), Mathf.Clamp(dirToObjectiveLeft.y, 0, 10f), Mathf.Clamp(dirToObjectiveLeft.z, 0, 10f));

        dirToObjectiveRight = transform.position - RightHandTransform.position;
        dirToObjectiveRight.Normalize();
        dirToObjectiveRight = new Vector3(Mathf.Clamp(dirToObjectiveRight.x, 0, 10f), Mathf.Clamp(dirToObjectiveRight.y, 0, 10f), Mathf.Clamp(dirToObjectiveRight.z, 0, 10f));

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

    float acc;
    float dist;

    public float timeReset = 0.4f;
    bool resetPos = false;

    bool needToReset;

    bool hardRESET;
    protected IEnumerator CoroutinePushCheck()
    {
        while (grabbed)
        {
            if (needToReset)
            {
                Debug.Log("transform.position.x: " + transform.position.x + " posA:" + posA.x);
                
                    if (RightHandTransform.position.x >= (posA.x-0.25))
                    {
                        hardRESET = false;
                        needToReset = false;
                        //resetPos = true;
                    }
                    else
                    {
                        hardRESET = true;
                        Debug.Log("OUT");
                        yield return null;
                    }
            }

            
            if (hardRESET)
            {
                yield return null;
            }
            else
            {
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

    }

    float SuccessTime = 0.4f;
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
            //resetPos = true;

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


        //private void CalculateProgress()
        //{
        //    //Dist B
        //    actualDist = Vector3.Distance(posB, transform.position);

        //    // Calculate progress
        //    progress = (distTarget - actualDist) / distTarget;

        //    if (progress < 0) progress = 0;
        //    if (progress > 1) progress = 1;
        //    //if (debugProgress) Debug.Log("Calculating progress... " + progress);

        //    // Send Value to Anim
        //    animator.SetFloat("Progress", progress);
        //}


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
    void StartPush()
    {
        needToReset = true;
        pushing = true;
        animator.SetBool("Push", false);
        animator.SetBool("FailedPush", false);
        Debug.Log("Start Push");
    }




    ///// <summary>
    ///// When hands have finished their push wether is completed or failed
    ///// </summary>
    //void EndPush()
    //{
    //    pushing = false;

    //    if (!avaiblePush || finished)
    //    {
    //        return;
    //    }
    //    avaiblePush = false;
    //    Invoke("ResetSuccesAvaible", 0.4f);

    //    if (acc > thresholdStartPushingAcc) 
    //    {
    //        SuccessPush(); Debug.Log("End Push: Success");
            
    //    }
    //    else 
    //    { 
    //        FailedPush(); Debug.Log("End Push: Failure"); 
    //    }  
    //}

    bool finished;
 
    void SuccessPush()
    {
        GameManager.instance.audioManager.PlayComplete();
        animator.SetTrigger("hit");
        actualSuccess++;
        Debug.Log("SuccessPush");
        pannelCounter.SetTxtInsuflacciones(actualSuccess);
        pannelCounter.SetPorcentageImage(actualSuccess, maxSuccess);
        hapticController.SendTotalHaptics();

        if (actualSuccess >= maxSuccess)
        {
            finished = true;
            StopCoroutine(CoroutinePushCheck());
            actualSuccess = 0;
            animator.SetTrigger("Completed");
            CompletedTask();
            Debug.Log("Completed");
            this.enabled = false;
            return;
        }        
    }

    void FailedPush()
    {
        //GameManager.instance.audioManager.PlayFail();
        //animator.SetBool("FailedPush", true);
        Debug.Log("FailedPush");
    }



    #endregion

    #region Debug
    //private void OnDrawGizmosSelected()
    //{
    //    //InitialPos
    //    Gizmos.color = new Color(0, 0, 1, .5f);
    //    Gizmos.DrawCube(initialPlayerPos.position + offSetSnapPos, .1f * Vector3.one);
    //}

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = new Color(0, 1, 0, .5f);

    //    if (bodyTransformAttached == null) { Debug.LogWarning("Add originalTransformObject"); return; }
    //    else
    //        offSetA = transform.position - bodyTransformAttached.position;

    //    posA = bodyTransformAttached.position + offSetA;
    //    posB = bodyTransformAttached.position + offSetB;

    //    Gizmos.DrawRay(initialPlayerPos.position, initialPlayerPos.forward);

    //    Gizmos.DrawCube(posA, Vector3.one * .1f);
    //    Gizmos.DrawCube(posB, Vector3.one * .1f);
    //    Gizmos.DrawLine(posA, posB);

    //    Gizmos.color = new Color(1, 0, 0, .5f);
    //    Gizmos.DrawCube(transform.position, Vector3.one * 0.05f);
    //}
    #endregion
}
