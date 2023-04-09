using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform cameraArm; //마우스 회전시 카메라 따라옴
    public float Dpi; //마우스 민감도

    // Update is called once per frame
    void Update()
    {
        LookAround();
    }

    public void LookAround() //대충 카메라 회전 앵글
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X") * Dpi ,Input.GetAxis("Mouse Y") * Dpi);
        Vector3 camAngle = cameraArm.rotation.eulerAngles;

        cameraArm.rotation = Quaternion.Euler(camAngle.x - mouseDelta.y,camAngle.y+mouseDelta.x,camAngle.z);
    }
}