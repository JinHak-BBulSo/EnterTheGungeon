using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public Room room;
    public int coin;
    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.Instance.player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // @brief 방의 모든 몬스터 처치시 자동 획득되도록 설정
        if (room.enemyCount <= 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, 10 * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // 플레이어가 획득한 코인의 변수를 갖고있는 스크립트에 적용되도록변경
            // << 플레이어 매니저나 플레이어 컨트롤러나 게임매니저나
            PlayerManager.Instance.player.playerMoney += coin;
            PlayerManager.Instance.player.OnHitAndStatusEvent();

            Destroy(this.gameObject);
        }
    }
}
