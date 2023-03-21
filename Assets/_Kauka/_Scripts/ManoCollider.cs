using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManoCollider : MonoBehaviour
{
    MeshCollider meshCollider;
    private void Awake()
    {
        meshCollider = GetComponentInChildren<MeshCollider>();
        meshCollider.enabled = false;
    }
}
