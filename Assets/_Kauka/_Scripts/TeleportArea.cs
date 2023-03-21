using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportArea : MonoBehaviour
{
    public GameObject[] areaArray;

    private void Awake()
    {

        SetDown();
        
        if (GameManager.instance.testing)
        {
          
            Invoke("SetTeleportArea", 0.2f);
        }
        else
        {

        }
     
    }

    public void SetDown()
    {
        for (int i = 0; i < areaArray.Length; i++)
        {
            areaArray[i].SetActive(false);  
        }
    }

    public void SetTeleportArea()
    {
        areaArray[GameManager.instance.moduleIndex].SetActive(true);
    }


}
