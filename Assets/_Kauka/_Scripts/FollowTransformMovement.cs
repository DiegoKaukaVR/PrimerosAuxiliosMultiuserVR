using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransformMovement : MonoBehaviour
{
    [SerializeField] public Transform Target;

    private void FixedUpdate()
    {
        transform.position = Target.transform.position;
    }
}
