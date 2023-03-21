using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHands : MonoBehaviour
{
    protected GameObject defaultController;
    protected GameObject alternativeController;

    public static ChangeHands instance;

    public ChangeLeftHandsController changeLeftHand;
    public ChangeRightHandsController changeRightHand;


    public bool showHands;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }


    public void ChangeLeftHand(int value) { changeLeftHand.ChangeHandsControllerToAlternative(value); }
    public void ChangeRightHand(int value) { changeRightHand.ChangeHandsControllerToAlternative(value); }

    public void ShowHands(bool value)
    {
        changeLeftHand.handsControllerPersonalizado.notShowControllers = !value;
        changeRightHand.handsControllerPersonalizado.notShowControllers = !value;
    }

    //public void SetPellizcarAvaible(bool value)
    //{
    //    DatosImportantes.pellizcarAvaible = value;
    //}
}
