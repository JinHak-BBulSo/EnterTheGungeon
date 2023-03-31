using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBottomCollier : MonoBehaviour
{

    public BoxCollider2D playerBottomCollider = default;
    PlayerController player = default;

    private void Awake()
    {
        playerBottomCollider = gameObject.GetComponentMust<BoxCollider2D>();

        playerBottomCollider.offset = new Vector2(-0.5f, -4f);
        playerBottomCollider.size = new Vector2(16f, 19f);

    }

    //! 선택된 플레이어의 콜라이더를 몸통에 맞게 바꿔주는 함수
    public void ResettingCollider()
    {
        playerBottomCollider.offset = new Vector2(-0.5f, -9f);
        playerBottomCollider.size = new Vector2(9f, 9f);
        player = PlayerManager.Instance.player;
    }


    //! 플레이어 몸통 부분에 대한 충돌 작동에 대한 함수
    public void OnCollisionEnter2D(Collision2D collision)
    {
        /*if (collision.collider.CompareTag("MonsterBullet"))
        {
            player.GetHitPlayer();

        }*/
    }


    //! 트리거 체크가 된 것에 대한 플레이어 몸통 충돌 작동에 대한 함수
    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "MonsterBullet")
        {
            /*player.GetHitPlayer();

            if (PlayerManager.Instance.player.playerHp > 0)
            {
                if (PlayerManager.Instance.player.playerShield >= 1f)
                {
                    player.playerShield--;
                    player.hpController.SetPlayerHp(player.playerHp, player.playerMaxHp, player.playerShield);
                }
                else
                {
                    player.playerHp--;
                    player.hpController.SetPlayerHp(player.playerHp, player.playerMaxHp, player.playerShield);
                }
            }*/
        }
    }
}
