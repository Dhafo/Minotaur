using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public void FollowTarget(Transform target)
    {
        gameObject.transform.SetParent(target);
        gameObject.transform.localPosition = new Vector3(0,0,-10);
    }
}