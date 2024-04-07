using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The object to follow
    public Vector3 offset;   // The offset from the target's position

    void LateUpdate()
    {
        if (target != null)
        {
            // Update the camera's position to follow the target with the specified offset
            transform.position = target.position + offset;
        }
    }
}