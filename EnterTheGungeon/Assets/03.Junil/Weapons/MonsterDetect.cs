using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDetect : MonoBehaviour
{

    private MissileBulletMove missileMove = default;
    private bool isFirstTarget = false;


    private void OnDisable()
    {
        isFirstTarget = false;
    }

    private void Start()
    {
        missileMove = gameObject.transform.parent.
                gameObject.GetComponentMust<MissileBulletMove>();

        isFirstTarget = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Monster")
        {
            if (isFirstTarget == true) { return; }
            GFunc.Log("몬스터 감지됨");

            isFirstTarget = true;
            // 감지된 몬스터의 오브젝트 정보를 미사일 스크립트에 넘긴다.
            missileMove.targetMonster = collision.gameObject;
            
        }
    }
}
