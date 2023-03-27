using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : BaseBullet
{
    
    // 총알 속도
    public float bulletSpeed = default;

    // 총알 피해량
    public int bulletDamage = default;

    // 무기 원본 총알 피해량
    public int originBulletDamage = default;

    // 무기 사거리
    public float bulletRange = default;

    protected virtual void OnEnable()
    {
        bulletDamage = originBulletDamage + PlayerManager.Instance.player.playerDamage;
        Debug.Log(bulletDamage);
    }

    public void SetBulletData(Weapons weapons)
    {
                
        bulletSpeed = weapons.BulletSpeed();
        originBulletDamage = weapons.BulletDamage();
        bulletRange = weapons.BulletRange();

    }



}
