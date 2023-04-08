using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    float hAxis; //좌우 움직임 <>
    float vAxis; //상하 움직임 ▲▼

    public float speed;
    bool  wdown;
    Vector3 moveVec;

    Animator anim;
    // Start is called before the first frame update
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
    }
    private void movePlayer()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        moveVec = new Vector3( hAxis, 0,vAxis).normalized;
       

        wdown = Input.GetButton("walk");
        //걷기에 따른 속도 번화
        transform.position += moveVec * speed * (wdown ? 0.3f : 1.0f) * Time.deltaTime;
        //걷는 방향으로 바라보기
        transform.LookAt(transform.position + moveVec);
        //애니메이션
        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isWalk", wdown);
    }

}

