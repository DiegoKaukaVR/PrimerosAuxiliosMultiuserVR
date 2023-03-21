using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VallaControl : MonoBehaviour
{
    public Transform VallaTransform;
    public GameObject base1;


    Collider coll;
    Rigidbody rb;
    [SerializeField] float hardcodedHeight;
    [SerializeField] float minHeightToReposition;


    private void Start()
    {
        coll = base1.GetComponent<Collider>();
        rb = base1.GetComponent<Rigidbody>();

        pos = base1.transform.localPosition;
        rot = base1.transform.localEulerAngles;
    }
    public void CalculateReposition()
    {
        if (VallaTransform.transform.position.y < minHeightToReposition)
        {
            Debug.Log("Reposition");
            VallaTransform.transform.position = new Vector3(VallaTransform.transform.position.x, hardcodedHeight, VallaTransform.transform.position.z);
            VallaTransform.transform.eulerAngles = new Vector3(0, VallaTransform.eulerAngles.y, 0);
        }
     
    }

    Vector3 pos;
    Vector3 rot;
    public void SetOff()
    {
        coll.enabled = false;
      
        rb.isKinematic = true;


       
    }
    public void SetOn()
    {

        CalculateReposition();
        base1.transform.localPosition = pos;
        base1.transform.localEulerAngles = rot;
        
        Invoke("Logic", 0.1f);
       
    }

    void Logic()
    {
        coll.enabled = true;
        rb.isKinematic = false;

    }
}
