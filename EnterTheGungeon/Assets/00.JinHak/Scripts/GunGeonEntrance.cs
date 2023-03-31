using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunGeonEntrance : MonoBehaviour
{
    Animator playerAni = default;
    [SerializeField]
    SelectPlayerManager selectPlayerManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && collision.gameObject != null)
        {
            playerAni = PlayerManager.Instance.player.GetComponent<Animator>();
            // 선택되지 않은 플레이어 파괴
            selectPlayerManager.DestroyNotSelectPlayer();
            // 초기 무기 셋팅 호출
            InventoryManager.Instance.inventoryControl.AddFirstItem();
            playerAni.SetTrigger("Doorway");
            StartCoroutine(SceneLoadDelay());
        }
    }

    IEnumerator SceneLoadDelay()
    {
        PlayerManager.Instance.player.enabled = false;
        PlayerManager.Instance.player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        yield return new WaitForSeconds(0.6f);
        LoadingManager.Instance.LoadLoadingScene("03.StageScene");
    }
}
