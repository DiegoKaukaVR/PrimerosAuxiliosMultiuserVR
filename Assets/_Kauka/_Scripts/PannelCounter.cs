using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PannelCounter : MonoBehaviour
{
    public Image porcentageImage;
    public TextMeshProUGUI textCounter;


    public void SetTxtInsuflacciones(int value)
    {
        textCounter.text = value.ToString();
    }
    public void SetPorcentageImage(int currentPorcentage, int maxPorcentage)
    {
        switch (currentPorcentage)
        {
            case 0:
                porcentageImage.fillAmount = 0;
                break;

            case 1:
                porcentageImage.fillAmount = 0.2f;
                break;

            case 2:
                porcentageImage.fillAmount = 0.4f;
                break;

            case 3:
                porcentageImage.fillAmount = 0.6f;
                break;

            case 4:
                porcentageImage.fillAmount = 0.8f;
                break;
            case 5:
                porcentageImage.fillAmount = 1f;
                break;
            default:
                break;
        }
       
    }

    public void ResetCounter()
    {
        porcentageImage.fillAmount = 0;
    }


}
