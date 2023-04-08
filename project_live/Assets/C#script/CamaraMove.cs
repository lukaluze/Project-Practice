using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraMove : MonoBehaviour
{
    // Start is called before the first frame update
    //플레이어
    
    public Transform target;
    public Vector3 offset;
   
    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + offset;
        MoveCamara();
        playerLookfoward();
    }
    //카메라 회전 움직이기
    private void MoveCamara()
    {
        Vector3 mouseDelta = new Vector3 ( Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y") ); 
        Vector3 camAngle = transform.rotation.eulerAngles;
        //x값 제한 걸기
        float x = camAngle.x - mouseDelta.y;
        if (x < 180f)
        {
            x = Mathf.Clamp(x, -1f, 60f);
        }
        else
        {
            x = Mathf.Clamp(x, 355f, 361f);
        }
        transform.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
    }
    private void playerLookfoward()
    {
        Vector3 LookForward = new Vector3(transform.forward.x, 0f, transform.forward.z).normalized;
        target.forward = LookForward;
    }
}
