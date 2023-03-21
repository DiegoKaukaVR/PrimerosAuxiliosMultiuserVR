using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticUI : MonoBehaviour
{
    public static StaticUI instance;



    [ColorUsage(true, true)] public Color correctColorBackGround = Color.green;
    [ColorUsage(true, true)] public Color correctColorMark = Color.green;
    [ColorUsage(true, true)] public Color correctColorReMark = Color.green;

    private void Awake()
    {
        if (instance ==null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
}
