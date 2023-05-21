using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gameCam;          //���� ī�޶�
    public PlayerMove player;           //�÷��̾�
    public float playTime;              //�÷��� �ð�
    public bool isBattle;               //���� �ο���ִ��� Ȯ��
    
    public GameObject gmaePanel;        //���� ��
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

    private void LateUpdate() //Update�� ���� �� ȣ��Ǵ� �����ֱ�
    {
        scoreText.text = string.Format("{0:n0}", player.score);
        playerHealthText.text = player.health + "/" + player.maxHealth;
        if (player.equipWeapon == null || player.equipWeapon.type == Weapon.Type.Melee)
            playerAmmoText.text = "- /" + player.ammo;
        else
            playerAmmoText.text = 5 + "/" + player.ammo;

        
    }

}
