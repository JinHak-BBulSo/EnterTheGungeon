using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    public static Animator playerAni = default;
    public static Rigidbody2D playerRigid = default;
    public static void PlayerSet()
    {
        playerAni = PlayerManager.Instance.player.GetComponent<Animator>();
        playerRigid = PlayerManager.Instance.player.GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            SoundManager.Instance.Play("Player/forever_fall_01", Sound.SFX);
            // [Junil] 무기가 꺼지는 함수 호출

            PlayerManager.Instance.player.StartOffWeaponObjs();
            playerAni.SetTrigger("Falling");
            PlayerManager.Instance.player.enabled = false;
            playerRigid.velocity = Vector3.zero;
            StartCoroutine(FallReturn());
        }
    }

    IEnumerator FallReturn()
    {
        yield return new WaitForSeconds(1.5f);
        PlayerManager.Instance.player.enabled = true;
        PlayerManager.Instance.player.transform.position = new Vector2(0, 0);
        playerAni.SetTrigger("FallReturn");
        SoundManager.Instance.Play("Player/fall_respawn_01", Sound.SFX);
        yield return new WaitForSeconds(0.5f);
        PlayerManager.Instance.player.OnHitAndStatusEvent();
    }
}
