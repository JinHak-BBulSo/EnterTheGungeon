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

    // 아머 수치
    public int armorVal = default;

    // 아머를 장착하고 있는지의 유무 체크하는 bool 값
    public bool isArmor = true;

    public bool isOnInventory = false;

    // 현재 장착중인 무기의 한손인지 두손인지 체크하는 int 값
    public int nowWeaponHandVal = default;


    // 피격이나 아머를 먹는 등의 이벤트가 발생하면 참이 되는 bool 값
    public bool isStatusEvent = true;

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
        isStatusEvent = true;
        isOnInventory = false;

        // 임시로 1로 지정
        nowWeaponHandVal = 1;
    }



    // Start is called before the first frame update
    void Start()
    {
        // 플레이어 싱글톤 호출
        PlayerManager.Instance.player = this;
        GFunc.Log("플레이어 캐싱 ok");
    }

    // Update is called once per frame
    void Update()
    {
        if(isStatusEvent == true)
        {
            playerMove.PlayerAniRestart(isArmor, nowWeaponHandVal);

            isStatusEvent = false;
        }

        if (isOnInventory == true) { return; }

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
            //playerMove.PlayerAniRestart(isArmor, );
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

    //! 플레이어의 현재 애니메이션을 갱신하기 위해 발동시키는 함수
    public void OnHitAndStatusEvent()
    {
        isStatusEvent = true;
    }   // OnHitAndStatusEvent()

}
