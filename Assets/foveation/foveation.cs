using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foveation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Unity.XR.Oculus.Utils.EnableDynamicFFR(true);
        Unity.XR.Oculus.Utils.SetFoveationLevel(4);
    }
}
