using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PannelInsuflacciones : MonoBehaviour
{

    public Image porcentageImage;
    public TextMeshProUGUI textInsuflacciones;


    public void SetTxtInsuflacciones(int value )
    {
        textInsuflacciones.text = value.ToString();
    }
    public void SetPorcentageImage(int currentPorcentage, int maxPorcentage)
    {
        if (currentPorcentage == 1)
        {
            porcentageImage.fillAmount = 0.5f;
        }
        else
        {
            porcentageImage.fillAmount = 0;
        }

        //porcentageImage.fillAmount = (float)(currentPorcentage / maxPorcentage);

    }

}
