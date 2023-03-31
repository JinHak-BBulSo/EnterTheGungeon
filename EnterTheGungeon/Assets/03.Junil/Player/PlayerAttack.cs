using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{


    private GameObject playerObj = default;
    private GameObject rotateObjs = default;
    public GameObject weaponObjs = default;
    private Animator playerAni = default;


    // 지금 사용 가능한 무기 배열
    public List<GameObject> playerWeapons = new List<GameObject>();

    // 지금 사용할 무기 스크립트 가져오는 배열
    public List<PlayerWeapon> playerWeaponScript = new List<PlayerWeapon>();

    public bool isDodgeing = false;

    public int nowWeaponIndex = default;

    public bool isReloadNow = false;


    //// Start is called before the first frame update
    //private void Awake()
    //{
    //    rotateObjs = gameObject.transform.parent.gameObject;
    //    playerObj = rotateObjs.transform.parent.gameObject;
    //    playerAni = rotateObjs.transform.parent.gameObject.GetComponentMust<Animator>();
    //    weaponObjs = gameObject.FindChildObj("Weapons");

        
    //    nowWeaponIndex = 0;

    //    isDodgeing = false;

        
    //}

    // Update is called once per frame
    void Update()
    {
        // 셋팅 되기 전에는 업데이트 문을 못 돌게 하는 조건
        if(PlayerManager.Instance.player.isSetOk == false) { return; }

        if (isDodgeing == true) { return; }

        // { [Junil] 무기가 마우스 커서를 바라보는 코드
        // 마우스 커서 위치 값
        Vector3 mousePos_ = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        

        // 마우스 커서 위치와 이 오브젝트의 위치를 뺀 값
        Vector2 len_ = mousePos_ - transform.position;

        float lookZ_ = Mathf.Atan2(len_.y, len_.x);

        float rotateZ_ = lookZ_ * Mathf.Rad2Deg;

        lookZ_ *= 100;

        
        transform.rotation = Quaternion.Euler(0, 0, rotateZ_);

        // } [Junil] 무기가 마우스 커서를 바라보는 코드

        // 제대로 된 x 축 반전을 구하기 위한 값
        Vector2 rotateStand_ = mousePos_ - playerObj.transform.position;



        if (rotateStand_.x > 0)
        {
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);

            playerObj.transform.localScale = new Vector3(1f, 1f, 1f);

            if (isReloadNow == true)
            {
                PlayerManager.Instance.player.weaponReload.transform.localScale = new Vector3(1f, 1f, 1f);

            }
            else
            {
                PlayerManager.Instance.player.weaponReload.transform.localScale = new Vector3(0.0001f, 0.0001f, 0.0001f);

            }
        }
        else
        {
            gameObject.transform.localScale = new Vector3(-1f, -1f, 1f);

            playerObj.transform.localScale = new Vector3(-1f, 1f, 1f);

            if(isReloadNow == true)
            {
                PlayerManager.Instance.player.weaponReload.transform.localScale = new Vector3(-1f, 1f, 1f);

            }
            else
            {
                PlayerManager.Instance.player.weaponReload.transform.localScale = new Vector3(-0.0001f, 0.0001f, 0.0001f);

            }
        }


        playerAni.SetFloat("inputX", rotateStand_.x);
        playerAni.SetFloat("inputY", rotateStand_.y);


        float lookZ2_ = Mathf.Atan2(rotateStand_.y, rotateStand_.x);


        if(playerWeaponScript.Count == 0 ||
            playerWeaponScript[nowWeaponIndex] == default ||
           playerWeaponScript[nowWeaponIndex] == null)
        {
            /* Do Nothing */
        }
        else
        {
            chkPosWeaponSort(rotateStand_.x, rotateStand_.y);

        }



    }

    public void SetPlayerAttack()
    {
        rotateObjs = gameObject.transform.parent.gameObject;
        playerObj = rotateObjs.transform.parent.gameObject;
        playerAni = rotateObjs.transform.parent.gameObject.GetComponentMust<Animator>();
        weaponObjs = gameObject.FindChildObj("Weapons");


        nowWeaponIndex = 0;

        isDodgeing = false;
    }


    //! 현재 들고 있는 총을 발사하는 함수
    public void FireBulletWeapon()
    {
        if (playerWeaponScript.Count == 0 ||
            playerWeaponScript[nowWeaponIndex] == default ||
            playerWeaponScript[nowWeaponIndex] == null)
        {
            PlayerManager.Instance.player.OnHitAndStatusEvent();
            return;
        }

        GFunc.Log("공격 클릭 됨");
        playerWeaponScript[nowWeaponIndex].FireBullet();
    }

    //! 현재 총을 재장전해주는 함수
    public void ReloadBulletWeapon()
    {
        if (nowWeaponIndex == default)
        {
            PlayerManager.Instance.player.OnHitAndStatusEvent();

            return;
        }

        playerWeaponScript[nowWeaponIndex].ReloadBullet();

    }

    

    //! 무기 위치에 따라 레이어 값 바꾸기
    public void chkPosWeaponSort(float mousePosX, float mousePosY)
    {

        float lookZ2_ = Mathf.Atan2(mousePosY, mousePosX) * Mathf.Rad2Deg;


        if (26f < lookZ2_&& lookZ2_ < 153f)
        {
            playerWeapons[nowWeaponIndex].GetComponentMust<SpriteRenderer>().sortingOrder = 0;
            
        }
        else
        {
            playerWeapons[nowWeaponIndex].GetComponentMust<SpriteRenderer>().sortingOrder = 2;

        }
    }
}
