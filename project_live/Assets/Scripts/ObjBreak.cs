using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//오브젝트의 파괴와 관련된 코드입니다.
public class ObjBreak : MonoBehaviour
{
    public int maxHealth; //최대 Hp
    public int curHealth; //현재 Hp
    public GameObject dropItem;
    Rigidbody rigid;
    BoxCollider boxColler;
    Material mat;
   // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxColler = GetComponent<BoxCollider>();
        curHealth = maxHealth;
        mat = GetComponent<MeshRenderer>().material;
    }

  
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Melee") { //근접 무기로 때릴경우
            Weapon weapon = other.GetComponent<Weapon>();
            curHealth -= weapon.damage; //근접 무기 데미지로 대미지
            StartCoroutine(OnDamage());
        }
    }

    IEnumerator OnDamage() //데미지 를 입었을때
    {
        mat.color = Color.red;  
        yield return new WaitForSeconds(0.1f);

        if(curHealth > 0) //살아있네?
        {
            mat.color = Color.white;
        }
        else //디졌네?
        {
            mat.color = Color.grey;
            if(dropItem != null)
                Instantiate(dropItem, gameObject.transform.position, Quaternion.identity); //오브젝트 생성
            Destroy(gameObject, 1);
        }
    }
}
