using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMove playerMove = default;



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



        // 초기 시작 시, 공포탄 2개 지급
        chkBlankBullets = 2;
        armorVal = 1;
        isArmor = true;
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ResetPlayerAni();


        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        playerMove.OnMove(inputX, inputY);



        /// @brief 구르기를 사용하는 함수
        if (Input.GetMouseButtonDown(1))
        {
            playerMove.PlayerAniRestart(isArmor);
            playerMove.OnDodge();
        }


        /// @brief 공포탄을 사용하는 함수
        if (Input.GetKeyDown(KeyCode.Q))
        {

            if(chkBlankBullets == 0 && chkBlankBullets == default) { return; }
            OnPlayerBlankBullet();
            chkBlankBullets--;
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
