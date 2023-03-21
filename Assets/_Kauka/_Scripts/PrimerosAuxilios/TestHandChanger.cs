using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHandChanger : MonoBehaviour
{
  
    void Start()
    {
        Invoke("ChangeHand", 0.2f);
        
    }

    void ChangeHand()
    {
        ChangeHands.instance.changeRightHand.ChangeHandsControllerToAlternative(1);
        //ChangeHands.instance.changeLeftHand.ChangeHandsControllerToAlternative(1);

    }

 
}
