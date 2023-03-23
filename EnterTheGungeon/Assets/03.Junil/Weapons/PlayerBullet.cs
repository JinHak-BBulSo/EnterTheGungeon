using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    
    // 총알 속도
    public float bulletSpeed = default;

    // 총알 피해량
    public int bulletDamage = default;

    // 무기 사거리
    public float bulletRange = default;

    public virtual void OnOffBullet()
    {
        StartCoroutine("OffBullet");
    }


    public void SetBulletData(Weapons weapons)
    {
                
        bulletSpeed = weapons.BulletSpeed();
        bulletDamage = weapons.BulletDamage();
        bulletRange = weapons.BulletRange();

    }


    public IEnumerator OffBullet()
    {
        // 추후 총알이 끝나는 애니메이션을 위한 대기 시간
        yield return new WaitForSeconds(0.3f);


        this.gameObject.SetActive(false);
    }



}
