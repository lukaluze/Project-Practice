using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//������Ʈ�� �ı��� ���õ� �ڵ��Դϴ�.
public class ObjBreak : MonoBehaviour
{
    public int maxHealth; //�ִ� Hp
    public int curHealth; //���� Hp
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
        if(other.tag == "Melee") { //���� ����� �������
            Weapon weapon = other.GetComponent<Weapon>();
            curHealth -= weapon.damage; //���� ���� �������� �����
            StartCoroutine(OnDamage());
        }
    }

    IEnumerator OnDamage() //������ �� �Ծ�����
    {
        mat.color = Color.red;  
        yield return new WaitForSeconds(0.1f);

        if(curHealth > 0) //����ֳ�?
        {
            mat.color = Color.white;
        }
        else //������?
        {
            mat.color = Color.grey;
            if(dropItem != null)
                Instantiate(dropItem, gameObject.transform.position, Quaternion.identity); //������Ʈ ����
            Destroy(gameObject, 1);
        }
    }
}
