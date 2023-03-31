using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    // 무기 이름
    protected string weaponName = string.Empty;

    // 무기의 한줄 설명
    protected string weaponDesc = string.Empty;

    // 무기 설명
    public string weaponDataTxt = string.Empty;

    // 무기 위치
    protected Vector3 weaponPos = default;

    // 재장전 값
    protected float weaponReload = default;

    // 무기 탄창 크기
    protected int weaponMagazine = default;

    // 무기의 탄약량
    protected int weaponBulletValue = default;

    // 무기 넉백 값
    protected float knockBack = default;

    // 총알 속도
    protected float bulletSpeed = default;

    // 총알 피해량
    public int bulletDamage = default;

    // 무기 사거리
    protected float bulletRange = default;

    // 총알의 산탄도
    public int bulletShotRange = default;

    // 무기 발사 딜레이
    public float weaponDeley = default;

    // 무기가 한 손인지, 두 손인지, 안들고 있는지 확인
    protected int weaponHand = default;


    public string WeaponName()
    {
        return this.weaponName;
    }

    public string WeaponDesc()
    {
        return this.weaponDesc;
    }

    public string WeaponDataTxt()
    {
        return this.weaponDataTxt;
    }
    
    public Vector3 WeaponPos()
    {
        return this.weaponPos;
    }


    public float WeaponReload()
    {
        return this.weaponReload;
    }

    public int WeaponMagazine()
    {
        return this.weaponMagazine;
    }

    public int WeaponBulletValue()
    {
        return this.weaponBulletValue;
    }

    public float KnockBack()
    {
        return this.knockBack;
    }

    public float BulletSpeed()
    {
        return this.bulletSpeed;
    }

    public int BulletDamage()
    {
        return this.bulletDamage;
    }

    public float BulletRange()
    {
        return this.bulletRange;
    }

    public int BulletShotRange()
    {
        return this.bulletShotRange;
    }

    public float WeaponDeley()
    {
        return this.weaponDeley;
    }

    public int WeaponHand()
    {
        return this.weaponHand;
    }

}   // Weapons()

