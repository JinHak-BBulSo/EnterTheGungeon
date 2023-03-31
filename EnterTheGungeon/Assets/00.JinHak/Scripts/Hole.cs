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
        yield return new WaitForSeconds(0.5f);
        PlayerManager.Instance.player.OnHitAndStatusEvent();
    }
}
