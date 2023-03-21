using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotTransformCanvas : MonoBehaviour
{
    
    [SerializeField] Transform pivotMenu;
    public float yOffset = 1.25f;

    public float distanceSpawn = 2.5f;

    private void Awake()
    {
        pivotMenu = GameManager.instance.xrRig.GetComponentInChildren<PivotUsuario>().transform;
        MakeTransformation();
    }

    public void MakeTransformation()
    {
        //transform.position = GameManager.instance.xrRig.transform.position + transform.forward * distanceSpawn;
        transform.position = new Vector3(pivotMenu.position.x, yOffset, pivotMenu.position.z);
        transform.rotation = new Quaternion(0f, pivotMenu.rotation.y, 0f, pivotMenu.rotation.w);
    }

    

}
