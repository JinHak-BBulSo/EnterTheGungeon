using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    public void SetPlayer()
    {
        PlayerManager.Instance.player.enabled = true;
        PlayerManager.Instance.player.OnHitAndStatusEvent();

        PlayerManager.Instance.player.transform.position =
            transform.position + new Vector3(0, -4, 0);
        Camera.main.gameObject.transform.position = PlayerManager.Instance.player.transform.position;
    }
}
