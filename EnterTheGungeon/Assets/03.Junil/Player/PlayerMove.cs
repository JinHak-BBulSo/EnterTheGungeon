using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    
    private Rigidbody2D playerRigid2D = default;
    private Animator playerAni = default;
    //private PlayerAttack playerAttack = default;

    

    public float playerSpeed = default;
    public bool isDodgeing = false;

    public bool isNowArmor = false;

    public bool isReDodgeing = false;



    // Start is called before the first frame update
    void Start()
    {
        

    }

    //! 플레이어 움직임에 대한 초기 셋팅
    public void SetPlayerMove()
    {
        playerRigid2D = gameObject.GetComponentMust<Rigidbody2D>();
        playerAni = gameObject.GetComponentMust<Animator>();

        playerRigid2D.constraints = RigidbodyConstraints2D.None;
        playerRigid2D.constraints = RigidbodyConstraints2D.FreezeRotation;


        GameObject rotateObjs_ = gameObject.transform.GetChild(0).gameObject;


        isNowArmor = false;
        isDodgeing = false;
        isReDodgeing = false;
        playerSpeed = 5f;
    }
    

    //! 플레이어 좌우 상하 움직임을 담당하는 함수
    public void OnMove(float inputX, float inputY)
    {

        if(isDodgeing == true || isReDodgeing == true) { return; }

        playerRigid2D.velocity = new Vector2(inputX * playerSpeed, inputY * playerSpeed);

        if (inputX == 0 && inputY == 0)
        {
            playerAni.SetBool("isRun", false);

        }
        else
        {
            playerAni.SetBool("isRun", true);

        }

    }


    //! 플레이어의 구르기를 담당하는 함수
    public void OnDodge()
    {
        if (isDodgeing == true) 
        {
            return;
        }

        if (isReDodgeing == true)
        {
            StopReDodge();
            isReDodgeing = false;

        }
        PlayerAniDodge();

        isDodgeing = true;

        // 마우스 커서 위치 값
        Vector3 mousePos_ = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // 마우스 커서 위치와 이 오브젝트의 위치를 뺀 값
        Vector2 len_ = mousePos_ - transform.position;

        playerRigid2D.velocity = len_.normalized * playerSpeed;

        PlayerManager.Instance.player.StartOffWeaponObjs();


        StartCoroutine(OffDodge());
    }


    IEnumerator OffDodge()
    {
        
        yield return new WaitForSeconds(0.8f);
        isDodgeing = false;

        StartReDodge();
    }

    //! 다시 구르기를 할지에 대한 텀을 주는 코루틴
    IEnumerator ReDodgeCoroutine = default;

    void StartReDodge()
    {
        ReDodgeCoroutine = ReDodge();
        StartCoroutine(ReDodgeCoroutine);
    }

    void StopReDodge()
    {
        if(ReDodgeCoroutine != null)
        {
            StopCoroutine(ReDodgeCoroutine);
        }
    }

    IEnumerator ReDodge()
    {
        isReDodgeing = true;
        yield return new WaitForSeconds(0.2f);
        isReDodgeing = false;

        PlayerManager.Instance.player.OnHitAndStatusEvent();
    }


    //! 플레이어의 쉴드 유무를 확인하여 구르기 애니메이션 호출을 정해주는 함수
    public void PlayerAniDodge()
    {
        switch (PlayerManager.Instance.player.isShield)
        {
            case true:
                playerAni.SetTrigger("OnArmorDodge");

                break;

            case false:
                playerAni.SetTrigger("OffArmorDodge");

                break;
        }
    }

    // 아머의 유무와 현재 장착하고 있는 무기가 없거나, 한 손, 두 손인 경우를 받아서
    // 그에 맞는 애니메이션을 작동시키는 함수이다.
    public void PlayerAniRestart(bool isShield, int nowWeaponHand)
    {
        if(isShield == true)
        {
            switch (nowWeaponHand)
            {
                case 0:
                    playerAni.SetTrigger("OnArmorTwo");

                    break;

                case 1:
                    playerAni.SetTrigger("OnArmorOne");

                    break;

                case 2:
                    playerAni.SetTrigger("OnArmorZero");

                    break;
            }
        }
        else
        {
            switch (nowWeaponHand)
            {
                case 0:
                    playerAni.SetTrigger("OffArmorTwo");

                    break;

                case 1:
                    playerAni.SetTrigger("OffArmorOne");

                    break;

                case 2:
                    
                    playerAni.SetTrigger("OffArmorZero");

                    break;
            }
        }
    }

}
