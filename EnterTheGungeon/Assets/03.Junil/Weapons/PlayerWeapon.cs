using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : Weapons
{

    public GameObject bulletObjs = default;

    public Transform firePos = default;

    public GameObject rotateWeapon = default;

    public bool isReload = false;
    public bool isAttack = false;
    public int countBullet = default;
    public float deleyChkVal = default;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //! 총에 대한 정보를 한번에 처리하는 함수
    public virtual void SetWeaponData()
    {
        GameObject weapons_ = gameObject.transform.parent.gameObject;

        rotateWeapon = weapons_.transform.parent.gameObject;


        GameObject rotateObjs_ = rotateWeapon.transform.parent.gameObject;
        GameObject player_ = rotateObjs_.transform.parent.gameObject;
        GameObject playerObjs_ = player_.transform.parent.gameObject;

        bulletObjs = playerObjs_.FindChildObj("BulletObjs");

        // 총구 위치
        firePos = gameObject.FindChildObj("FirePos").transform;


    }
    
    // 총을 재장전하는 함수
    public virtual void ReloadBullet()
    {
        // -1 값은 재장전이 없다는 의미이다.
        if(weaponReload == -1) { /* Do Nothing */}
    }

    // 총을 발사하는 함수
    public virtual void FireBullet()
    {
        
    }

    public IEnumerator OnReload()
    {
        GFunc.Log("리로드 중");
        isReload = true;

        yield return new WaitForSeconds(weaponReload);
        countBullet = weaponMagazine;

        isReload = false;
    }
}
