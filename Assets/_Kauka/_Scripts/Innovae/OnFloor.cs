using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnFloor : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Start()
    {
        initialPosition = this.transform.position;
        initialRotation = this.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            StartCoroutine("TouchingFloor");
        }
    }

    IEnumerator TouchingFloor()
    {
        yield return new WaitForSeconds(3);
        this.transform.position = initialPosition;
        this.transform.rotation = initialRotation;
    }
}
