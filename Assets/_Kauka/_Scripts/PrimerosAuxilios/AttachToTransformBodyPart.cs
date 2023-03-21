using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachToTransformBodyPart : AttachToTransform
{

    public BodyPart bodyPart;




    public enum BodyPart
    {
        Head, 
        Neck,
        RightShoulder,
        LeftShoulder,
        LeftArm,
        RightArm,
        LeftElbow,
        RightElbow,
        LeftHand,
        RightHand,
        Spine1,
        Spine2,
        Spine3,
        LeftUpperLeg,
        RightUpperLeg,
        LeftMiddleLeg,
        RightMiddleLeg,
        LeftFoot,
        RightFoot,
    }
}
