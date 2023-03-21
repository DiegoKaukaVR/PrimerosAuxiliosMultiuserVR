using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class LibretaRecienNacidos : MonoBehaviour
{
    public bool[] matrizCompletado = new bool[9];
    public bool[] matrizDisponible = new bool[9];
    public float[] matrizAnotaciones = new float[9];

    public TextMeshProUGUI[] arraytxt;
    public Button[] buttonArray;

    private void Start()
    {
        AvaibleButton(8);

    }

    public void AvaibleButton(int index)
    {
        matrizDisponible[index] = true;
    }

    public void DisableButton(int index) 
    {
        matrizDisponible[index] = false;
    }

    public void CompletedAnotation(int index)
    {
        matrizCompletado[index] = true;
    }

    public void IntroduceValue(int index)
    {
        if (index > 9)
           return;

        if (!matrizDisponible[index])
            return;

        if (matrizCompletado[index])
            return;
     

        SetTxt(index);
        arraytxt[index].gameObject.SetActive(true);
        DisableButton(index);
        CompletedAnotation(index);
    }

    public void SetTxt(int i)
    {
        arraytxt[i].text = matrizAnotaciones[i].ToString();
    }

}
