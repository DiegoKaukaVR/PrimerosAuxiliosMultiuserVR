using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRotation : MonoBehaviour
{
    public Vector3 rotOffset;
    // Start is called before the first frame update
    void Awake()
    {
        transform.localEulerAngles = rotOffset;
        Invoke("LateStart", 0.1f);
    }

    void LateStart()
    {
        transform.localEulerAngles = Vector3.zero;
    }

  
}
