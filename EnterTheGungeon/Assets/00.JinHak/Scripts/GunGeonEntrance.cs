using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunGeonEntrance : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && collision.gameObject != null)
        {
            LoadingManager.Instance.LoadScene("05.StageScene");
        }
    }
}
