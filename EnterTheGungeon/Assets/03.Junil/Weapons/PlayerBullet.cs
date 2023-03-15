using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float bulletSpeed = default;


    public virtual void OnOffBullet()
    {
        StartCoroutine("OffBullet");
    }


    public IEnumerator OffBullet()
    {
        yield return new WaitForSeconds(0.3f);

        this.gameObject.SetActive(false);
    }


}
