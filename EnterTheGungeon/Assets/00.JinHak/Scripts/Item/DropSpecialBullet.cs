using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSpecialBullet : DropPassive
{
    public override void GetDropItem()
    {
        //총알을 OnEnble시 대미지 설정을 새롭게 해줄 필요가 있음
        //playerDamage + weapon의 bulletDamage로 설정 필요함
        //PlayerManager.Instance.player.playerDamage += 2;
        GetPassive();
    }
}
