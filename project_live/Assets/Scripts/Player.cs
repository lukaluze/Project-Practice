using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Rigidbody myRigid;
    
    float hAxis, vAxis;
    public float speed; //플레이어 이동 속도
    public float Dpi; //마우스 감도
    bool wDown;

    Vector3 moveVec;

    Animator anim; //애니메이션

    void Awake()
    {
        anim = GetComponentInChildren<Animator>(); //애니메이션
        myRigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        hAxis = Input.GetAxisRaw("Horizontal"); //좌우
        vAxis = Input.GetAxisRaw("Vertical");   //앞뒤
        wDown = Input.GetButton("Walk");        //shift누를시 걸음

        moveVec = new Vector3(hAxis, 0, vAxis).normalized; //이동

        if(wDown) //shift누를 시
            transform.position += moveVec * speed * 0.3f * Time.deltaTime; //원래 속도에 0.3 곱
        else
            transform.position += moveVec * speed * Time.deltaTime;

        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isWalk", wDown);

        //transform.LookAt(transform.position + moveVec);
    }
    

    
}
