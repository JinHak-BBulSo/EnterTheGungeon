using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBullets : BaseBullet
{
    // { [Junil] 몬스터 총알은 이 스크립트를 상속받기
    public virtual void OnEnable()
    {
        PlayerController.OnBlankBullet += this.OnPlayerBlankBullet;
    }

    public virtual void OnDisable()
    {
        PlayerController.OnBlankBullet -= this.OnPlayerBlankBullet;

    }

    /// @brief 공포탄 작동 시 적 총알 오브젝트가 꺼지는 함수
    public virtual void OnPlayerBlankBullet()
    {
        gameObject.SetActive(false);
    }

    // } [Junil] 몬스터 총알은 이 스크립트를 상속받기

}
