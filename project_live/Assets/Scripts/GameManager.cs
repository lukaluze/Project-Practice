using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gameCam;          //게임 카메라
    public PlayerMove player;           //플레이어
    public float playTime;              //플레이 시간
    public bool isBattle;               //현재 싸우고있는지 확인
    
    public GameObject gmaePanel;        //게임 판
    public Text scoreText;
    public Text playTimeText;
    public Text playerHealthText;
    public Text playerAmmoText;

    public Image weaponImage1;
    public Image weaponImage2;
    public Image weaponImage3;

    private void Start()
    {
        
    }

    private void LateUpdate() //Update가 끝난 후 호출되는 생명주기
    {
        scoreText.text = string.Format("{0:n0}", player.score);
        playerHealthText.text = player.health + "/" + player.maxHealth;
        if (player.equipWeapon == null || player.equipWeapon.type == Weapon.Type.Melee)
            playerAmmoText.text = "- /" + player.ammo;
        else
            playerAmmoText.text = 5 + "/" + player.ammo;

        
    }

}
