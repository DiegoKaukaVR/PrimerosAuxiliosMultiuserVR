using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigConfiguration : MonoBehaviour
{
    public static RigConfiguration
        instance;

    [Tooltip("Meter aquí todos los transforms que nos interese registrar")]
    public Transform[] transformArray;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    /// <summary>
    /// Returns Transform based on ID
    /// </summary>
    public Transform TransformByID(int ID)
    {
        if (ID> transformArray.Length)
        {
            Debug.LogError("Out of Collection");
            return null;
        }

        return transformArray[ID];
    }
}
