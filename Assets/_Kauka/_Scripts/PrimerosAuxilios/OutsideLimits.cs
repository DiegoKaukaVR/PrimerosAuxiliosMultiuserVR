using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideLimits : MonoBehaviour
{
    public float limitYWorld = .2f;
    public float threshHold = -0.3f;
    public float range = 10f;

    Vector3 initialPos;

    private void Start()
    {
        initialPos = transform.position;
    }
    public void FixedUpdate()
    {
        if (Vector3.Distance(transform.position,initialPos) > range)
        {
            transform.position = initialPos;
        }

        if (transform.position.y< threshHold)
        {
            transform.position = new Vector3(transform.position.x, limitYWorld, transform.position.z);
        }
    }


}
