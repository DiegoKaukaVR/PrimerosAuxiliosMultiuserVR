using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;




public class ActuacionPass : Process
{
    [Header("ActuacionPass Configuration")]
    [SerializeField] Animator maniquiAnimator;

    public XRGrabInteractable xrRama, xrValla;
    [SerializeField] AudioSource electrocutionAudio;


    /// <summary>
    /// When the human is electrocuted by streetlight. It is triggered when the module starts
    /// </summary>
    public void Electrocute()
    {
        electrocutionAudio.Play();
    }



    /// <summary>
    /// When player pushes the victim with a stick
    /// </summary>
    public void FallDownAgent()
    {
        maniquiAnimator.SetTrigger("Fall");
        Debug.Log("Falling...");
    
    }
    


 


   

}
