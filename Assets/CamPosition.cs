using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPosition : MonoBehaviour
{
    public Transform target;
    public Vector3 offsetPosition;
    //public Vector3 offsetRotation;

    public void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + offsetPosition;
            transform.rotation = Quaternion.Euler(0f, target.eulerAngles.y,0f);
        }
    }
}
