using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float Dpi; //민감도
    public float speed; //이동 속도

    [SerializeField]
    private Transform characterBody;
    [SerializeField]
    private Transform cameraArm;
    bool wDown;

    Animator animator;

    void Start()
    {
        animator = characterBody.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        LookAround();
        Move();
    }
    
    private void Move()
    {   
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
        wDown = Input.GetButton("Walk"); //걷기

        Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
        Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
        Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

        characterBody.forward = lookForward;

        if(wDown) //shift누를 시
            transform.position += moveDir * Time.deltaTime * speed * 0.3f; //원래 속도에 0.3 곱
        else
            transform.position += moveDir * Time.deltaTime * speed;

        animator.SetBool("isRun", moveInput != Vector2.zero);
        animator.SetBool("isWalk", wDown);
        
        

        Debug.DrawRay(cameraArm.position, new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized, Color.red);
    }

    void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X") * Dpi ,Input.GetAxis("Mouse Y") * Dpi);
        Vector3 camAngle = cameraArm.rotation.eulerAngles;
        float x = camAngle.x - mouseDelta.y;

        if(x < 180f)
        {
            x = Mathf.Clamp(x, -1f, 70f);
        }else{
            x = Mathf.Clamp(x, 335f, 361f);
        }

        cameraArm.rotation = Quaternion.Euler(x,camAngle.y+mouseDelta.x,camAngle.z);


    }
}
