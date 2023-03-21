using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Translatable_TextPro : MonoBehaviour
{
    public Text targetText;
    public string key;

    public TextMesh texto;  

    private void Awake()
    {
        targetText = GetComponent<Text>();
    }
}
