using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ChangeRightHandsController : MonoBehaviour
{
    public List<GameObject> prefabList;
    public List<bool> hasAnimatorParamether;

    public HandsControllerPersonalizado handsControllerPersonalizado;
    

    private void Awake()
    {
        ChangeHands.instance.changeRightHand = this;
        handsControllerPersonalizado = GetComponent<HandsControllerPersonalizado>();
    }
    public void ChangeHandsControllerToDefault()
    {
        handsControllerPersonalizado.handModelPrefab = prefabList[0];
        handsControllerPersonalizado.ChangeHands();
    }

    public void ChangeHandsControllerToAlternative(int value)
    {
        handsControllerPersonalizado.handModelPrefab = prefabList[value];
        handsControllerPersonalizado.hasAnimator = hasAnimatorParamether[value];
        handsControllerPersonalizado.ChangeHands();
    }

}
