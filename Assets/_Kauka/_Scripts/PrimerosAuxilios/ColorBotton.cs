using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorBotton : MonoBehaviour
{
    public Image Background;
    public Image mark;
    public Image remark;
    public Image remarkHover;

    public PreguntasRespuestas preguntasRespuetas;

    public Color originalColor;
    public Color originalMarkColor;
    public Color originalRemarkColor;

    Color correctColorBackGround = Color.green;
    Color correctColorMark = Color.green;
    Color correctColorReMark = Color.green;

    StaticUI staticUI;
    public int indexButton;

    private void Awake()
    {
        originalColor = Background.color;
        originalMarkColor = mark.color;

    }

    private void Start()
    {
        staticUI = StaticUI.instance;

        correctColorBackGround = staticUI.correctColorBackGround;
        correctColorMark = staticUI.correctColorMark;
        correctColorReMark = staticUI.correctColorReMark;
    }
    public void CheckCorrectButtonHover()
    {
        if (preguntasRespuetas.CheckCorrectButton(indexButton))
        {
            SetCompleteColor();
        }
       
    }

    public void SetCompleteColor()
    {
        Background.color = correctColorBackGround;
        mark.color = correctColorMark;
        remark.color = correctColorReMark;
        remarkHover.color = correctColorReMark;
    }
    public void SetOriginalColor()
    {
        Background.color = originalColor;
        mark.color = originalMarkColor;
        remark.color = originalRemarkColor;
        remarkHover.color = originalRemarkColor;
    }

    //private void OnValidate()
    //{
    //    if (Background == null || mark  == null || remark == null || remarkHover)
    //    {
    //        foreach (Transform trans in GetComponentsInChildren<Transform>())
    //        {
    //            if (trans.name == "bg")
    //            {
    //                Background = trans.GetComponent<Image>();
    //            }
    //            if (trans.name == "marco")
    //            {
    //                mark = trans.GetComponent<Image>();
    //            }
    //            if (trans.name == "esquinas")
    //            {
    //                remark = trans.GetComponent<Image>();
    //            }
    //            if (trans.name == "marcoHover")
    //            {
    //                remarkHover = trans.GetComponent<Image>();
    //            }
    //        }
    //    }
    //    originalColor = Background.color;
    //    originalMarkColor = mark.color;
    //    originalRemarkColor = remark.color;
    //}

}
