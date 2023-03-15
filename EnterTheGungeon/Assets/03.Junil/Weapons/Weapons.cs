using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons
{
    // 무기 이름
    protected string weaponName = string.Empty;

    // 무기 설명
    protected string weaponDescription = string.Empty;

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
    protected int bulletDamage = default;

    // 무기 사거리
    protected float bulletRange = default;

    // 총알의 산탄도
    protected int bulletShotRange = default;

    // 무기 발사 딜레이
    protected float weaponDeley = default;

    // 무기가 한 손인지, 두 손인지, 안들고 있는지 확인
    protected int weaponHand = default;


    public string WeaponName()
    {
        return this.weaponName;
    }

    public string WeaponDescription()
    {
        return this.weaponDescription;
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



class NoWeapon : Weapons
{

    public NoWeapon()
    {
        this.weaponHand = 0;
    }
}


class MarineNorWeapon : Weapons
{
    public MarineNorWeapon()
    {
        this.weaponName = "해병 휴대 무기";
        this.weaponDescription = "항상 그대 곁에\r\n" +
            "무한 탄환입니다. 비밀 벽을 드러내지 않습니다. 프라이머다인의 하급 병사가 총굴로 가지고 온 해병 휴대 무기입니다.\r\n" +
            "튼튼한 총처럼 보이지만, 정작 필요할 때는 오작동을 일으키는 것으로 알려졌습니다.";
        this.weaponReload = 1.2f;
        this.weaponMagazine = 10;
        
        // -1은 탄약량이 무제한이라는 의미이다.
        this.weaponBulletValue = -1;
        this.knockBack = 12f;
        this.bulletSpeed = 25f;
        this.bulletDamage = 5;
        this.bulletRange = 18f;
        this.bulletShotRange = 5;
        this.weaponDeley = 0.25f;
        this.weaponHand = 1;
    }
}