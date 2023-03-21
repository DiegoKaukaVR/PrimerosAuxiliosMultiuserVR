using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugZone : MonoBehaviour
{

    MeshRenderer mesh;

    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    public void SetScale(float value)
    {
        transform.localScale = Vector3.one * value;
    }
    public void SetMeshDisplay(bool value)
    {
        mesh.enabled = value;
    }


}
