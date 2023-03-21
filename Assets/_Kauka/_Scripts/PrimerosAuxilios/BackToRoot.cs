using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToRoot : MonoBehaviour
{
    Transform originalTransform;
    void Start()
    {
        originalTransform = transform.parent;
    }

    private void OnDisable()
    {
        BackToRootMethod();
    }
    public void BackToRootMethod()
    {
        transform.position = originalTransform.position;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

    }
}
