using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CopyHandPose
{
    #if UNITY_EDITOR
    [MenuItem("MyTools/MirrorObjectPose")]
    public static void mirrorObjectPose()
    {
        GrabHandPose handPose = Selection.activeGameObject.GetComponent<GrabHandPose>();
        mirrorHandPose(handPose.targetLeftHandPose, handPose.targetRightHandPose);
    }
    #endif

    public static void mirrorHandPose(HandData poseToMirror, HandData originPose)
    {
        for (int i=0; i< originPose.fingerBones.Length; i++)
        {
            poseToMirror.fingerBones[i].localRotation = originPose.fingerBones[i].localRotation;

            Vector3 fingerPosition = originPose.fingerBones[i].localPosition;
            fingerPosition.x *= -1;
            fingerPosition.y *= -1;
            fingerPosition.z *= -1;

            poseToMirror.fingerBones[i].localPosition = fingerPosition;
        }
    }
}
