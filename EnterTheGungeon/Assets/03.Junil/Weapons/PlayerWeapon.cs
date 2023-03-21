using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{

    public GameObject bulletObjs = default;

    public Transform firePos = default;

    public GameObject rotateWeapon = default;


    // 무기 이름
    public string weaponName = string.Empty;

    // 무기 설명
    public string weaponDescription = string.Empty;

    // 무기 위치
    public Vector3 weaponPos = default;

    // 재장전 값
    public float weaponReload = default;

    // 무기 탄창 크기
    public int weaponMagazine = default;

    // 무기의 탄약량
    public int weaponBulletValue = default;

    // 무기 넉백 값
    public float knockBack = default;

    // 총알 속도
    public float bulletSpeed = default;

    // 총알 피해량
    public int bulletDamage = default;

    // 무기 사거리
    public float bulletRange = default;

    // 총알의 산탄도
    public int bulletShotRange = default;

    // 무기 발사 딜레이
    public float weaponDeley = default;

    // 무기가 한 손인지, 두 손인지, 안들고 있는지 확인
    public int weaponHand = default;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /// [Junil] 2023/03/16
    /// @brief 총에 대한 정보를 한번에 처리하는 함수
    public void SetWeaponData(Weapons weapons)
    {
        GameObject weapons_ = gameObject.transform.parent.gameObject;

        rotateWeapon = weapons_.transform.parent.gameObject;


        GameObject rotateObjs_ = rotateWeapon.transform.parent.gameObject;
        GameObject player_ = rotateObjs_.transform.parent.gameObject;
        GameObject playerObjs_ = player_.transform.parent.gameObject;

        bulletObjs = playerObjs_.FindChildObj("BulletObjs");

        // 총구 위치
        firePos = gameObject.FindChildObj("FirePos").transform;

        weaponName = weapons.WeaponName();
        weaponDescription = weapons.WeaponDescription();
        weaponPos = weapons.WeaponPos();
        weaponReload = weapons.WeaponReload();
        weaponMagazine = weapons.WeaponMagazine();
        weaponBulletValue = weapons.WeaponBulletValue();
        knockBack = weapons.KnockBack();
        bulletSpeed = weapons.BulletSpeed();
        bulletDamage = weapons.BulletDamage();
        bulletRange = weapons.BulletRange();
        bulletShotRange = weapons.BulletShotRange();
        weaponDeley = weapons.WeaponDeley();
        weaponHand = weapons.WeaponHand();

    }

    /// [Junil] 2023/03/16
    /// @brief 총을 재장전하는 함수
    public virtual void ReloadBullet()
    {
        // -1 값은 재장전이 없다는 의미이다.
        if(weaponReload == -1) { /* Do Nothing */}
    }

    /// [Junil] 2023/03/16
    /// @brief 총을 발사하는 함수
    public virtual void FireBullet()
    {
        
    }
}
