using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float Dpi; //민감도
    public float speed; //이동 속도
    public float jumpfower;//점프 힘


    [SerializeField]
    private Transform characterBody;
    [SerializeField]
    private Transform cameraArm;

    Rigidbody rigid; //스크립트 객체의 rigidbody

    //입력 관련 키
    float hPlayerAsix; // <> 
    float vPlayerAsix; // ㅅV
    bool jDown; //space
    bool wDown; //left shift
    bool isJump; //점프중?
    float x_Mouse;
    float y_Mouse;

    Animator animator;

    void Start()
    {
        animator = characterBody.GetComponentInChildren<Animator>(); //애니메이션 불러오기
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        inputKey(); 
        if (isJump == false)
            LookAround();
        Move();
        Jump();
    }
    
    void inputKey() // 키 입력 모음
    {
        if (isJump == false) { //점프중 이동 방향 및 걷기 제한
            hPlayerAsix = Input.GetAxisRaw("Horizontal");
            vPlayerAsix = Input.GetAxisRaw("Vertical");
            wDown = Input.GetButton("Walk"); //걷기
        }
        x_Mouse = Input.GetAxis("Mouse X"); //마우스 위 아래
        y_Mouse = Input.GetAxis("Mouse Y"); //마우스 좌 우
        jDown = Input.GetButtonDown("Jump"); //점프 (스페이스)
    }
    private void Move() //캐릭터 이동
    {   
        Vector2 moveVec= new Vector2(hPlayerAsix,vPlayerAsix);
        Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
        Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
        Vector3 moveDir = lookForward * vPlayerAsix + lookRight * hPlayerAsix;

        characterBody.forward = lookForward;

        //shift누를 시 원래 속도에 0.3 곱
        transform.position += moveDir * Time.deltaTime * speed * (wDown ? 0.3f : 1.0f);
  

        animator.SetBool("isRun", moveVec != Vector2.zero);
        animator.SetBool("isWalk", wDown);
 
        Debug.DrawRay(cameraArm.position, new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized, Color.red); //캐릭 보는 방향 디버그
    }

    void Jump()
    {
        if (jDown&&!isJump)
        {
            rigid.AddForce(Vector3.up * jumpfower,ForceMode.Impulse);
            isJump = true;
            animator.SetBool("isJump", true);
            animator.SetTrigger("doJump");
        }
    }

    void LookAround() // 카메라 회전
    {
        Vector2 mouseDelta = new Vector2(x_Mouse * Dpi , y_Mouse * Dpi);
        Vector3 camAngle = cameraArm.rotation.eulerAngles;
        float x = camAngle.x - mouseDelta.y;

        if(x < 180f) //위아래 각도
        {
            x = Mathf.Clamp(x, -1f, 30f);
        }else{
            x = Mathf.Clamp(x, 330f, 361f);
        }

        cameraArm.rotation = Quaternion.Euler(x,camAngle.y+mouseDelta.x,camAngle.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "floor") //바닥에 닿으면
        {
            isJump = false;
            animator.SetBool("isJump", false);
        }
    }
}
