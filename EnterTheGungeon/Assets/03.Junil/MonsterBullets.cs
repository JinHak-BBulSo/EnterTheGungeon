using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBullets : MonoBehaviour
{
    // { [Junil] 몬스터 총알은 이 스크립트를 상속받기
    private void OnEnable()
    {
        PlayerController.OnPlayerBlankBullet += this.OnPlayerBlankBullet;
    }

    private void OnDisable()
    {
        PlayerController.OnPlayerBlankBullet -= this.OnPlayerBlankBullet;

    }

    /// @brief 공포탄 작동 시 적 총알 오브젝트가 꺼지는 함수
    void OnPlayerBlankBullet()
    {
        gameObject.SetActive(false);
    }

    // } [Junil] 몬스터 총알은 이 스크립트를 상속받기


}
