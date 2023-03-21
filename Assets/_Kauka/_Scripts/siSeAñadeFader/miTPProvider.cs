using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Assertions;

namespace UnityEngine.XR.Interaction.Toolkit
{
    /// <summary>
    /// The <see cref="TeleportationProvider"/> is responsible for moving the XR Rig
    /// to the desired location on the user's request.
    /// </summary>
    // [HelpURL(XRHelpURLConstants.k_TeleportationProvider)] -> no deberia estar comentado-mirar unav
    public class miTPProvider : TeleportationProvider
    {
        public Fader fade;

        public override bool QueueTeleportRequest(TeleportRequest teleportRequest)
        {
            currentRequest = teleportRequest;
            StartCoroutine(startFade());
            //
            return true;
        }

        IEnumerator startFade()
        {
            fade.FadeIn();
            yield return new WaitForSeconds(Fader.Instance.tiempoFadeNormal);
            validRequest = true;
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(endFade());
        }
        IEnumerator endFade()
        {
            yield return new WaitForEndOfFrame();
            fade.FadeOut();
        }
    }
    /*
        public Fader fade;
        /// <summary>
        /// The current teleportation request.
        /// </summary>
        protected TeleportRequest currentRequest { get; set; }

        /// <summary>
        /// Whether the current teleportation request is valid.
        /// </summary>
        protected bool validRequest { get; set; }

        /// <summary>
        /// This function will queue a teleportation request within the provider.
        /// </summary>
        /// <param name="teleportRequest">The teleportation request to queue.</param>
        /// <returns>Returns <see langword="true"/> if successfully queued. Otherwise, returns <see langword="false"/>.</returns>
        public virtual bool QueueTeleportRequest(TeleportRequest teleportRequest)
        {
            Debug.Log("ghfdfgh");

            currentRequest = teleportRequest;
            StartCoroutine(startFade());
            //
            return true;
        }
        
        /// <summary>
        /// See <see cref="MonoBehaviour"/>.
        /// </summary>
        protected virtual void Update()
        {
            if (!validRequest || !BeginLocomotion())
                return;

            var xrRig = system.xrRig;
            if (xrRig != null)
            {
                switch (currentRequest.matchOrientation)
                {
                    case MatchOrientation.WorldSpaceUp:
                        xrRig.MatchRigUp(Vector3.up);
                        break;
                    case MatchOrientation.TargetUp:
                        xrRig.MatchRigUp(currentRequest.destinationRotation * Vector3.up);
                        break;
                    case MatchOrientation.TargetUpAndForward:
                        xrRig.MatchRigUpCameraForward(currentRequest.destinationRotation * Vector3.up, currentRequest.destinationRotation * Vector3.forward);
                        break;
                    case MatchOrientation.None:
                        // Change nothing. Maintain current rig rotation.
                        break;
                    default:
                        Assert.IsTrue(false, $"Unhandled {nameof(MatchOrientation)}={currentRequest.matchOrientation}.");
                        break;
                }

                var heightAdjustment = xrRig.rig.transform.up * xrRig.cameraInRigSpaceHeight;

                var cameraDestination = currentRequest.destinationPosition + heightAdjustment;

                xrRig.MoveCameraToWorldLocation(cameraDestination);
            }

            EndLocomotion();
            validRequest = false;
        
        }

        IEnumerator startFade()
        {
            fade.FadeIn();
            yield return new WaitForSeconds(1f);
            StartCoroutine(startFade());
            validRequest = true;
            Debug.Log("asd");
        }
        IEnumerator endFade()
        {
            yield return new WaitForSeconds(1f);
            fade.FadeOut();
        }
    }*/

    
}

