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
    bool isGorgunBulletCheck;
    SpriteRenderer image;
    Rigidbody2D rigid;
    public int patternBulletNumber;
    ObjectPool objectPool;
    bool isCreated;
    RectTransform rectTransform;

    public string enemyName;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        image = GetComponent<SpriteRenderer>();

        rigid = GetComponent<Rigidbody2D>();
        rectTransform = GetComponent<RectTransform>();
        objectPool = GameObject.Find("ObjectPool").GetComponent<ObjectPool>();
        gameObject.SetActive(false);
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

        if (!isGorgunBulletCheck)
        {
            isGorgunBulletCheck = true;
            if (isGorgunBullet)
            {
                image.enabled = false;
                GetComponent<CircleCollider2D>().enabled = false;
                StartCoroutine(Patten1Bullet());
            }
            else
            {
                image.enabled = true;
                GetComponent<CircleCollider2D>().enabled = true;
            }
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        //base.OnTriggerEnter2D(other);
        if (other.tag == "Player" && other.gameObject != null && 
            !PlayerManager.Instance.player.isAvoid && !PlayerManager.Instance.player.isAttacked)
        {
            isGorgunBullet = false;
            StartCoroutine(ReturnBullet(0.1f));

            //Destroy(this.gameObject);
        }
        else
        {
            if ((other.tag == "Wall") && other.gameObject != null)
            {
                isGorgunBullet = false;
                StartCoroutine(ReturnBullet(0.1f));
                //objectPool.ReturnObject(this.gameObject, 1);
            }
        }
    }

    IEnumerator ReturnBullet(float time_)
    {
        yield return new WaitForSeconds(time_);
        enemyName = default;
        isGorgunBullet = false;
        image.enabled = true;
        GetComponent<CircleCollider2D>().enabled = true;
        rigid.velocity = Vector3.zero;
        rectTransform.rotation = Quaternion.Euler(Vector3.zero);
        isCheckAngle = false;
        StopAllCoroutines();
        objectPool.ReturnObject(this.gameObject, 1);
        //Destroy(this.gameObject);
    }

    IEnumerator WaitToFire()
    {
        yield return new WaitForSeconds(1);

        rigid.velocity = transform.up * 5;

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

    public override void OnEnable()
    {
        if (isCreated)
        {
            PlayerController.OnBlankBullet += this.OnPlayerBlankBullet;
            StartCoroutine(ReturnBullet(5));
            isGorgunBulletCheck = false;
        }

        isCreated = true;

    }
    public override void OnDisable()
    {

    }

    public override void OnPlayerBlankBullet()
    {
        StartCoroutine(ReturnBullet(0.1f));

        //objectPool.ReturnObject(this.gameObject, 1);
    }
}
