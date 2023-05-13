using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type {Melee, Range}//공격의 타입(근접, 원거리)
    public Type type;
    public int damage; //뎀지
    public float rate; //공속
    public BoxCollider meleeArea;      //근접공격 범위
    public TrailRenderer trailEffect;   //효과변수

    public void Use(){
        if(type == Type.Melee){
            StopCoroutine("Swing");
            StartCoroutine("Swing"); 
        }
    }

    IEnumerator Swing()//코루틴 사용 (메인루틴과 같이 실행)
    {
        //1
        yield return new WaitForSeconds(0.1f); //0.1초 대기
        meleeArea.enabled = true;
        trailEffect.enabled = true;
        //2
        yield return new WaitForSeconds(0.7f); //0.7초 대기
        meleeArea.enabled = false;
        yield return new WaitForSeconds(0.3f);
        trailEffect.enabled = false;
    }

}
