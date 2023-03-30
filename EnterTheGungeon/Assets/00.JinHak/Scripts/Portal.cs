using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public void OnClickPortal()
    {
        PlayerManager.Instance.player.transform.position = this.transform.position;
    }
}
