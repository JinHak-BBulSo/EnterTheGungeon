using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    public void OnOffBullet()
    {
        StartCoroutine(this.OffBullet());
    }

    public virtual IEnumerator OffBullet()
    {
        // 추후 총알이 끝나는 애니메이션을 위한 대기 시간
        yield return new WaitForSeconds(0.3f);

        this.gameObject.SetActive(false);
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            OnOffBullet();

        }
    }
}
