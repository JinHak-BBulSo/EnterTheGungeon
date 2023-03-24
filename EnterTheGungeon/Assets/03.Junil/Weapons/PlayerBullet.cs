using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : BaseBullet
{
    
    // 총알 속도
    public float bulletSpeed = default;

    // 총알 피해량
    public int bulletDamage = default;

    // 무기 사거리
    public float bulletRange = default;



    public void SetBulletData(Weapons weapons)
    {
                
        bulletSpeed = weapons.BulletSpeed();
        bulletDamage = weapons.BulletDamage();
        bulletRange = weapons.BulletRange();

    }



}
