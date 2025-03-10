using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class quatFacade : MonoBehaviour
{
    public Quaternion smoothRotate(Vector3 camRot, Quaternion rotation, float time){
        Quaternion targetRot = Quaternion.LookRotation(camRot, Vector3.up);
        return Quaternion.Slerp(rotation, targetRot, time);
    }
}
