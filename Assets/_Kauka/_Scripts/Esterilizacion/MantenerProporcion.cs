using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pa : MonoBehaviour
{
    Vector3 scaleOrig;
    // Start is called before the first frame update
    void Start()
    {
        scaleOrig = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = scaleOrig;   
    }
    public void CogerTalla()
    {
        //GetComponent<HingeJoint>().
    }
}
