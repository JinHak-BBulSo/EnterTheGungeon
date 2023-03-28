using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{


    private GameObject playerObj = default;
    private GameObject rotateObjs = default;
    public GameObject weaponObjs = default;
    private Animator playerAni = default;
    private Canvas rotateSort = default;

    // 무기 프리팹 저장하는 배열
    public GameObject[] playerWeaponPrefabs = default;

    // 지금 사용 가능한 무기 배열
    public List<GameObject> playerWeapons = new List<GameObject>();

    // 지금 사용할 무기 스크립트 가져오는 배열
    public List<PlayerWeapon> playerWeaponScript = new List<PlayerWeapon>();

    public bool isDodgeing = false;

    public int nowWeaponIndex = default;

    // Start is called before the first frame update
    private void Awake()
    {
        rotateObjs = gameObject.transform.parent.gameObject;
        playerObj = rotateObjs.transform.parent.gameObject;
        playerAni = rotateObjs.transform.parent.gameObject.GetComponentMust<Animator>();
        weaponObjs = gameObject.FindChildObj("Weapons");


        rotateSort = weaponObjs.GetComponentMust<Canvas>();
        rotateSort.sortingLayerName = "Player";
        
        nowWeaponIndex = 0;

        isDodgeing = false;

        //playerWeaponPrefabs = Resources.LoadAll<GameObject>("03.Junil/Prefabs/PlayerWeapons");


        //for (int i = 0; i < playerWeaponPrefabs.Length; i++)
        //{

        //    playerWeapons.Add(Instantiate(playerWeaponPrefabs[i], weaponObjs.transform.position,
        //        Quaternion.identity, weaponObjs.transform));

        //    playerWeaponScript.Add(playerWeapons[i].GetComponentMust<PlayerWeapon>());


            
        //    playerWeapons[i].name = playerWeaponPrefabs[i].name;
        //    playerWeaponScript[i].name = playerWeaponPrefabs[i].name + "_Script";

        //    playerWeaponPrefabs[i].SetActive(false);
        //    playerWeapons[i].SetActive(false);

        //}


        //playerWeapons[nowWeaponIndex].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

        if(isDodgeing == true) { return; }

        // { [Junil] 무기가 마우스 커서를 바라보는 코드
        /// @param Vector3 mousePos_ : 마우스 커서 위치 값
        Vector3 mousePos_ = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        

        /// @param Vector2 len_ : 마우스 커서 위치와 이 오브젝트의 위치를 뺀 값
        Vector2 len_ = mousePos_ - transform.position;

        float lookZ_ = Mathf.Atan2(len_.y, len_.x);

        float rotateZ_ = lookZ_ * Mathf.Rad2Deg;

        lookZ_ *= 100;

        
        transform.rotation = Quaternion.Euler(0, 0, rotateZ_);

        // } [Junil] 무기가 마우스 커서를 바라보는 코드

        /// @param Vector2 rotateStand_ : 제대로 된 x 축 반전을 구하기 위한 값
        Vector2 rotateStand_ = mousePos_ - playerObj.transform.position;



        if(rotateStand_.x > 0)
        {
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);

            playerObj.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(-1f, -1f, 1f);

            playerObj.transform.localScale = new Vector3(-1f, 1f, 1f);
        }


        playerAni.SetFloat("inputX", rotateStand_.x);
        playerAni.SetFloat("inputY", rotateStand_.y);


        float lookZ2_ = Mathf.Atan2(rotateStand_.y, rotateStand_.x);

        chkPosWeaponSort(rotateStand_.x, rotateStand_.y);



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
        playerWeaponScript[nowWeaponIndex].ReloadBullet();

    }

    //! 무기를 바꾸는 함수
    public void ChangeWeapons()
    {
        
    }


    //! 무기 위치에 따라 레이어 값 바꾸기
    public void chkPosWeaponSort(float mousePosX, float mousePosY)
    {

        float lookZ2_ = Mathf.Atan2(mousePosY, mousePosX) * Mathf.Rad2Deg;


        if (26f < lookZ2_&& lookZ2_ < 153f)
        {
            rotateSort.sortingOrder = 0;
        }
        else
        {
            rotateSort.sortingOrder = 2;
        }
    }
}
