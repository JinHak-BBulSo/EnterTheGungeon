using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestBullet : MonsterBullets
{
    //0: fired from weapon, 1: summon from Enemy
    public int bulletType;

    public float angle;
    GameObject player;

    bool isCheckAngle;

    public bool isGorgunBullet;
    Image image;
    public int patternBulletNumber;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        image = GetComponent<Image>();


        StartCoroutine(DestroyBullet());

        if (isGorgunBullet)
        {
            image.enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
            StartCoroutine(Patten1Bullet());
        }
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
            //gameObject.GetComponent<Rigidbody2D>().velocity = transform.up * 5;
        }
        else if (bulletType == 1)
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
    IEnumerator Patten1Bullet()
    {
        for (int i = 0; i < 5; i++)
        {
            if (patternBulletNumber == i)
            {
                yield return new WaitForSeconds(i + 1);
                image.enabled = true;
                GetComponent<CircleCollider2D>().enabled = true;
            }
        }
    }
}
