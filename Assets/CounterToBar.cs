using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CounterToBar : MonoBehaviour
{
    float currentCounter;
    float maxCounter = 5;

    public Image UIRespiracion;

    [SerializeField] TextMeshProUGUI textCounter;
    public void AddCounter()
    {
        currentCounter ++;
        UIRespiracion.fillAmount = currentCounter / maxCounter;
        textCounter.text = currentCounter.ToString();
    }

    public void ResetTimer()
    {
        currentCounter = 0;
        UIRespiracion.fillAmount = 0;
        textCounter.text = "0";
    }

    private void OnEnable()
    {
        ResetTimer();
    }
    private void OnDisable()
    {
        ResetTimer();
    }
}
