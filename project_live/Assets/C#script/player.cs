using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    float hAxis; //�¿� ������ <>
    float vAxis; //���� ������ ���

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
        //�ȱ⿡ ���� �ӵ� ��ȭ
        transform.position += moveVec * speed * (wdown ? 0.3f : 1.0f) * Time.deltaTime;
        //�ȴ� �������� �ٶ󺸱�
        transform.LookAt(transform.position + moveVec);
        //�ִϸ��̼�
        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isWalk", wdown);
    }

}

