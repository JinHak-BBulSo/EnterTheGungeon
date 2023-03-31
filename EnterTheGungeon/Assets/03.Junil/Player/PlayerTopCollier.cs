using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTopCollier : MonoBehaviour
{

    public BoxCollider2D playerTopCollider = default;
    public PlayerController player = default;

    private void Awake()
    {
        playerTopCollider = gameObject.GetComponentMust<BoxCollider2D>();
    }

    void Start()
    {
        player = PlayerManager.Instance.player;
    }

    //! 플레이어 머리 부분에 대한 충돌 작동에 대한 함수
    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "MonsterBullet" && !player.isAttacked)
        {
            GFunc.Log("상체 반응했음");
            player.GetHitPlayer();
        }
    }
}
