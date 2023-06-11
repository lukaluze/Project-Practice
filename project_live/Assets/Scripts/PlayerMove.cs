using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float Dpi; //민감도
    public float speed; //이동 속도
    public float jumpfower;//점프 힘
    public GameObject[] weapons; //획득할 무기의 종류 확인 변수
    public bool[] hasWeapons;
    public int[] eatWeapons = {-1,-1,-1};

    [SerializeField]
    private Transform characterBody;
    [SerializeField]
    private Transform cameraArm;

    Rigidbody rigid; //스크립트 객체의 rigidbody

    float originalSpeed;    //기존 spped의 대체
    float bounceSpeed;   //벽에 튕겨져 나올때 값

    //입력 관련 키
    float hPlayerAsix; // <> 
    float vPlayerAsix; // ㅅV
    bool jDown; //space
    bool wDown; //left shift
    bool isJump; //점프중?
    bool iDown; //상호작용

    bool sDown1; //장비 번호
    bool sDown2;
    bool sDown3;
    bool isSwap; //무기 변경중인지 확인하는 bool값
    bool isFireReady; //공격 준비완료

    bool fDown;     //공격 버튼
    float x_Mouse;
    float y_Mouse;

    float fireDelay; //공격의 딜레이

    Animator animator;

    GameObject nearObject; //item먹었을 경우 이벤트
    public Weapon equipWeapon; //장착중인 무기
    int equipWeaponIndex = -1; //무기 없을때는 교체 안되도록

    public int score; //플레이어 점수부분
    public int health; //플레이어 체력부분
    public int ammo; //플레이어 총알 개수 부분
    public int maxHealth; //플레이어 최대체력부분

    void Start()
    {
        bounceSpeed = 5f;   //벽에 튕겨져 나올때 값

        originalSpeed = speed;          //기존 스피드를 속도의 기본값으로 지정
        animator = characterBody.GetComponentInChildren<Animator>(); //애니메이션 불러오기
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        inputKey(); 
        LookAround();
        Move();
        Jump();
        Attack();       //공격 함수
        Interation();
        Swap();         //플레이어가 들고있는 무기 변경 함수
        SwapOut();      //플레이어가 무기교체중인지의 대한 bool값을 확인(변경) 하는 함수
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
        jDown = Input.GetButtonDown("Jump");
        iDown = Input.GetButtonDown("Interation"); //e키를 누를시 idow이 활성화

        sDown1 = Input.GetButtonDown("Swap1"); //1키를 누를시 해머
        sDown2 = Input.GetButtonDown("Swap2"); //2키를 누를시 권총
        sDown3 = Input.GetButtonDown("Swap3"); //3키를 누를시 머신건

        fDown = Input.GetButtonDown("Fire1"); //왼쪽마우스 클리시 
    }


    private void Move() //캐릭터 이동
    {   
        Vector2 moveVec= new Vector2(hPlayerAsix,vPlayerAsix);
        Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
        Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
        Vector3 moveDir = lookForward * vPlayerAsix + lookRight * hPlayerAsix;

        characterBody.forward = lookForward;

        if(wDown) //shift누를 시
            transform.position += moveDir * Time.deltaTime * speed * 0.3f; //원래 속도에 0.3 곱
        else
            transform.position += moveDir * Time.deltaTime * speed;

        animator.SetBool("isRun", moveVec != Vector2.zero);
        animator.SetBool("isWalk", wDown);
 
        Debug.DrawRay(cameraArm.position, new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized, Color.red); //캐릭 보는 방향 디버그
    }

    void Jump()
    {
        if (jDown && !isJump && !isSwap)
        {
            rigid.AddForce(Vector3.up * jumpfower,ForceMode.Impulse);
            isJump = true;
            animator.SetBool("isJump", true);
            animator.SetTrigger("doJump");
        }
    }

    void Attack()
    {
        if(equipWeapon == null)//손에 무기가 있는지 확인
            return;
        
        fireDelay += Time.deltaTime;
        isFireReady = equipWeapon.rate < fireDelay;

        if(fDown && isFireReady && !isSwap)
        {
            equipWeapon.Use(); //무기 스크립트에 있는 use
            animator.SetTrigger("doSwing");
            fireDelay = 0;//공격을 했기에 딜레이를 0으로 설정
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

        if (collision.gameObject.CompareTag("Wall")) 
        {
            speed = -originalSpeed;         //벽의 부디쳤을때 speed값을 -로
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {

            speed = originalSpeed; // 원래 속도로 복원
        }
    }



    void Swap(){
        //무기 먹지 않았거나 같은 무기를 들고 있을때는 변경 불가능
        if(sDown1 && (!hasWeapons[0]|| equipWeaponIndex == eatWeapons[0] ))
            return;
        if(sDown2 && (!hasWeapons[1] || equipWeaponIndex == eatWeapons[1]))
            return;
        if(sDown3 && (!hasWeapons[2] || equipWeaponIndex == eatWeapons[2]))
            return;

        int weaponIndex = -1; //무기 인덱스 값 변수지정
        if(sDown1) weaponIndex = eatWeapons[0]; 
        if(sDown2) weaponIndex = eatWeapons[1]; 
        if(sDown3) weaponIndex = eatWeapons[2]; 

        if((sDown1 || sDown2 || sDown3) && !isJump ){ //무기변경, 점프시에는 불가능
            if(equipWeapon != null){
                equipWeapon.gameObject.SetActive(false);
            } 
            equipWeaponIndex = weaponIndex;
            equipWeapon = weapons[weaponIndex].GetComponent<Weapon>();
            weapons[weaponIndex].gameObject.SetActive(true); //무기 활성화

            animator.SetTrigger("doSwap");

            isSwap = true;//무기 교체중
            Invoke("SwapOut", 0.4f); //0.4초후에 무기 교체 false로 할당 
        }
    }

    void SwapOut(){
        isSwap = false;
    }

    void Interation(){
        if(iDown && nearObject != null && !isJump){
            if(nearObject.tag == "Weapon"){
                Item item = nearObject.GetComponent<Item>();
                int weaponIndex = item.value; //망치 0, 권총 1, 머신건 2
                for (int i = 0; i < 3; i++) //아이템 순서대로 먹기
                {
                    for(int m =0; m< 3;m++)
                    {
                        if(eatWeapons[m] == item.value) break; //같은 무기가 존재하면 break;
                    }
                    if(eatWeapons[i] == -1)
                    {
                        eatWeapons[i] = item.value;
                        hasWeapons[i] = true; //무기가 들어갔다고 표시
                        break;
                    }
                }


                Destroy(nearObject);//현재 가지고 있는 무기 제거
            }
        }
    }
    
    private void OnTriggerStay(Collider other) { //item먹었을때 이벤트
        if(other.tag == "Weapon"){
            nearObject = other.gameObject;
        } //item의 tag가 weapon일 경우
            
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "Weapon"){
            nearObject = null;
        }
    }
    
}
