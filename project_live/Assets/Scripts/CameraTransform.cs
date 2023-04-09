using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransform : MonoBehaviour
{
    public Transform target;
    public Vector3 offSet;

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + offSet;    
    }
}
