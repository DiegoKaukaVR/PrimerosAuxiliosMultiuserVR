using TMPro;
using UnityEngine;
using UnityEngine.UI;
[ExecuteAlways]
public class Translatable : MonoBehaviour
{
    //public Text targetText;
    public TextMeshProUGUI targetText;
    public string key;
    private void Awake()
    {
        targetText = GetComponentInChildren<TextMeshProUGUI>();
        if (targetText == null)
        {
            targetText = GetComponent<TextMeshProUGUI>();
        }
    }
    public void GetText()
    {
        targetText = GetComponentInChildren<TextMeshProUGUI>();
        if (targetText == null)
        {
            targetText = GetComponent<TextMeshProUGUI>();
        }
    }
}

