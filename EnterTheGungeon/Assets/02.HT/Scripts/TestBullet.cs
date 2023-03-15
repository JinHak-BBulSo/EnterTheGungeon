using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBullet : MonsterBullets
{
    //0: fired from weapon, 1: summon from Enemy
    public int bulletType;

    float angle;
    GameObject player;

    bool isCheckAngle;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");

        StartCoroutine(DestroyBullet());
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCheckAngle)
        {
            if (bulletType == 1)
            {
                angle = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x)
                * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            }
            isCheckAngle = true;
        }

        if (bulletType != 1)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = transform.up * 5;
        }
        else if(bulletType == 1)
        {
            StartCoroutine(WaitToFire());
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }

    IEnumerator WaitToFire()
    {
        yield return new WaitForSeconds(1);
        gameObject.GetComponent<Rigidbody2D>().velocity = transform.up * 5;

    }
}
