using SaveData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    public WeaponReload weaponReload = default;
    public PlayerMove playerMove = default;
    public PlayerAttack playerAttack = default;
    public Image playerImage = default;

    public ActiveItem playerActiveItem = default;
    public ActiveItem PlayerActiveItem
    {
        get { return playerActiveItem; }
        private set { }
    }

    // { [Junil] 공포탄 이벤트를 사용하기 위한 델리게이트, 이벤트 선언
    public delegate void PlayerBlankBullets();

    public static event PlayerBlankBullets OnPlayerBlankBullet;

    // } [Junil] 공포탄 이벤트를 사용하기 위한 델리게이트, 이벤트 선언


    // { [Junil] 플레이어 스탯 캐싱 값

    // 플레이어 HP 수치
    public int playerHp = default;

    // 플레이어 Hp Max 수치
    public int playerMaxHp = default;

    // 플레이어 아머 수치
    public int playerShield = default;

    // 플레이어 공포탄 개수
    public int playerBlank = default;

    // 플레이어 돈 개수
    public int playerMoney = default;

    // 플레이어 열쇠 개수
    public int playerKey = default;

    // 플레이어 데미지
    public int playerDamage = default;

    // } [Junil] 플레이어 스탯 캐싱 값


    // 아머를 장착하고 있는지의 유무 체크하는 bool 값
    public bool isShield = true;

    // 인벤토리가 열려있는지 확인하는 bool 값
    public bool isOnInventory = false;

    // 현재 장착중인 무기의 한손인지 두손인지 체크하는 int 값
    public int nowWeaponHand = default;


    // 피격이나 쉴드를 먹는 등의 이벤트가 발생하면 참이 되는 bool 값
    public bool isStatusEvent = true;

    public System.Action activeItem = default;

    // 플레이어가 피격을 당했는지 확인하는 bool 값
    public bool isAttacked = false;

    private void Awake()
    {
        playerMove = gameObject.GetComponentMust<PlayerMove>();
        GameObject rotateObjs_ = gameObject.FindChildObj("RotateObjs");
        playerAttack = rotateObjs_.FindChildObj("RotateWeapon").GetComponentMust<PlayerAttack>();
        playerImage = gameObject.GetComponentMust<Image>();
        weaponReload = gameObject.transform.GetChild(3).gameObject.GetComponentMust<WeaponReload>();


        PlayerState playerData = new PlayerState
        {
            hp = 6,
            maxHp = 6,
            shield = 0,
            blank = 2,
            money = 0,
            key = 1,
            // 다른 필드에 대한 기본값 설정                
        };

        playerHp = playerData.hp;
        playerMaxHp = playerData.maxHp;
        playerShield = playerData.shield;
        playerBlank = playerData.blank;
        playerMoney = playerData.money;
        playerKey = playerData.key;

        isStatusEvent = true;
        isOnInventory = false;
        isAttacked = false;

        nowWeaponHand = 0;

        // 플레이어 싱글톤 호출
        PlayerManager.Instance.player = this;
        GFunc.Log("플레이어 호출");
    }



    // Start is called before the first frame update
    void Start()
    {
        //// 플레이어 싱글톤 호출
        //PlayerManager.Instance.player = this;
        //GFunc.Log("플레이어 호출");

    }

    // Update is called once per frame
    void Update()
    {
        if(isStatusEvent == true)
        {
            //HpController.SetPlayerHp(playerHp, playerMaxHp, playerShield);
            //BlankController.SetPlayerBlank(playerBlank);
            //KeyController.SetPlayerKey(playerKey);
            //CashController.SetPlayerCash(playerMoney);
            ResetPlayerAni();

            playerMove.PlayerAniRestart(isShield, nowWeaponHand);

            isStatusEvent = false;
        }

        if (isOnInventory == true) { return; }



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

            if(playerBlank == 0 && playerBlank == default) { return; }
            OnPlayerBlankBullet();
            playerBlank--;
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

        // 액티브 사용
        if (Input.GetKeyDown(KeyCode.Space) && PlayerActiveItem != default)
        {
            playerActiveItem.UseActive();
            playerActiveItem = default;
        }
    }

    public void ResetPlayerAni()
    {
        if(playerShield == 0)
        {
            isShield = false;
        }
        else
        {
            isShield = true;
        }

        // 추후 무기 값도 가져오기
    }

    //! 플레이어의 현재 애니메이션을 갱신하기 위해 발동시키는 함수
    public void OnHitAndStatusEvent()
    {
        isStatusEvent = true;
    }   // OnHitAndStatusEvent()

    
    //! 플레이어가 피격 시 깜빡거리는 효과
    public void AttackedPlayer()
    {

        if(isAttacked == true) { return; }

        StartCoroutine(AttackedAction());
    }

    //! 플레이어 이미지의 알파값을 조절하여 깜빡이는 효과를 주는 코루틴
    IEnumerator AttackedAction()
    {
        isAttacked = true;

        int countTime_ = 0;

        Color playerColor_ = playerImage.color;

        while (countTime_ < 10)
        {
            if (countTime_ % 2 == 0)
            {
                playerColor_.a = 90f / 255f;
                playerImage.color = playerColor_;

            }
            else
            {
                playerColor_.a = 180f / 255f;

                playerImage.color = playerColor_;

            }

            yield return new WaitForSeconds(0.2f);

            countTime_++;
        }

        playerColor_.a = 255f / 255f;

        playerImage.color = playerColor_;

        isAttacked = false;
    }
    
}
