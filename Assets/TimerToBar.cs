using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerToBar : MonoBehaviour
{
    float currentTimer;
    float maxTimer = 3f;

    [SerializeField] TextMeshProUGUI _text;

    public Image UIRespiracion;
    public void AddTimer()
    {
        currentTimer += Time.deltaTime;
        _text.text = string.Format("{0:0.0}", currentTimer);

        UIRespiracion.fillAmount = currentTimer / maxTimer;
    }

    public void ResetTimer()
    {
        currentTimer = 0;
    }
}
