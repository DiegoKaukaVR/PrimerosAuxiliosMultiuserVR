using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionBase : MonoBehaviour
{
    public string _name;
    public bool _value;

    public void ChangeToFalse()
    {
        _value = false;
    }

    public void ChangeToTrue()
    {
        _value = true;
    }

    public void ChangeValue(bool value)
    {
        _value = value;
    }

}
