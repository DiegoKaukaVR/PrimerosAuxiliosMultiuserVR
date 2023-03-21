using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelGolpes : MonoBehaviour
{
    public Image image;
    public void ResetFillAmount()
    {
        image.fillAmount = 0;
    }
    private void OnDisable()
    {
        ResetFillAmount();
    }
}
