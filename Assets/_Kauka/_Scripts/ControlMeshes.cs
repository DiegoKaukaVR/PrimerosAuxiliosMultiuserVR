using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMeshes : MonoBehaviour
{
    public List<MeshRenderer> meshRendererList;

    private void Start()
    {
        foreach (MeshRenderer mesh in GetComponentsInChildren<MeshRenderer>())
        {
            if (meshRendererList.Contains(mesh))
            {
                continue;
            }
            meshRendererList.Add(mesh);
        }
    }
    public void SetOff()
    {
        for (int i = 0; i < meshRendererList.Count; i++)
        {
            meshRendererList[i].enabled = false;
        }
    }

    public void SetOn()
    {
        for (int i = 0; i < meshRendererList.Count; i++)
        {
            meshRendererList[i].enabled = true;
        }
    }
}
