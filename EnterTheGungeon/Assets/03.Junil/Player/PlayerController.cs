using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMove playerMove = default;
    private PlayerAttack playerAttack = default;
    private Canvas playerSort = default;


    // { [Junil] 공포탄 이벤트를 사용하기 위한 델리게이트, 이벤트 선언
    public delegate void PlayerBlankBullets();

    public static event PlayerBlankBullets OnPlayerBlankBullet;

    // } [Junil] 공포탄 이벤트를 사용하기 위한 델리게이트, 이벤트 선언


    /// @param int ChkBlankBullets : 소지하고 있는 공포탄 개수
    public int chkBlankBullets = default;

    public int armorVal = default;

    public bool isArmor = true;

    private void Awake()
    {
        playerMove = gameObject.GetComponentMust<PlayerMove>();
        GameObject rotateObjs_ = gameObject.FindChildObj("RotateObjs");
        playerAttack = rotateObjs_.FindChildObj("RotateWeapon").GetComponentMust<PlayerAttack>();
        playerSort = gameObject.transform.parent.gameObject.GetComponentMust<Canvas>();

        playerSort.sortingLayerName = "Player";
        playerSort.sortingOrder = 1;

        // 초기 시작 시, 공포탄 2개 지급
        chkBlankBullets = 2;
        armorVal = 1;
        isArmor = true;
    }



    // Start is called before the first frame update
    void Start()
    {
        // 플레이어 싱글톤 호출
        PlayerManager.Instance.player = this;
    }

    // Update is called once per frame
    void Update()
    {
        ResetPlayerAni();


        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        playerMove.OnMove(inputX, inputY);

        if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            // 마우스 휠 아래로
            if(0 < playerAttack.nowWeaponIndex && playerAttack.nowWeaponIndex != 0)
            {
                playerAttack.playerWeapons[playerAttack.nowWeaponIndex].SetActive(false);

                playerAttack.nowWeaponIndex--;

                playerAttack.playerWeapons[playerAttack.nowWeaponIndex].SetActive(true);

            }

        }
        else if (0 < Input.GetAxis("Mouse ScrollWheel"))
        {
            // 마우스 휠 위로
            if(playerAttack.nowWeaponIndex < playerAttack.playerWeapons.Count - 1)
            {
                playerAttack.playerWeapons[playerAttack.nowWeaponIndex].SetActive(false);

                playerAttack.nowWeaponIndex++;

                playerAttack.playerWeapons[playerAttack.nowWeaponIndex].SetActive(true);

            }
        }

        // 구르기를 사용
        if (Input.GetMouseButtonDown(1))
        {
            playerMove.PlayerAniRestart(isArmor);
            playerMove.OnDodge();
        }


        // 공포탄을 사용
        if (Input.GetKeyDown(KeyCode.Q))
        {

            if(chkBlankBullets == 0 && chkBlankBullets == default) { return; }
            OnPlayerBlankBullet();
            chkBlankBullets--;
        }

        // 현재 들고 있는 총을 쏜다
        if (Input.GetMouseButtonDown(0))
        {
            GFunc.Log("공격 클릭 됨");
            playerAttack.FireBulletWeapon();

        }

        // 재장전
        if (Input.GetKeyDown(KeyCode.R))
        {
            playerAttack.ReloadBulletWeapon();

        }


    }

    public void ResetPlayerAni()
    {
        if(armorVal == 0)
        {
            isArmor = false;
        }
        else
        {
            isArmor = true;
        }

        // 추후 무기 값도 가져오기
    }


}
