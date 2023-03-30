using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestEnemy : MonoBehaviour
{
    public int attackType;
    public string enemyName;
    public int dropCoinCount;
    TestEnemyWeapon weapon;
    TestEnemyEye eye;
    GameObject hand;
    float dist;
    bool isDelayEnd;

    float angle;
    GameObject player;
    Rigidbody2D rigid;

    Vector2 direction;

    // { status
    public int maxHp;
    public int currentHp;
    public float moveSpeed;

    bool isDead;
    // { status
    Image bodyImage;
    RectTransform bodyImageRectTransform;

    // { animation var

    bool isTargetLeft;
    bool isTargetRight;
    bool isTargetLeftBack;
    bool isTargetRightBack;

    public bool isMove;
    bool IsSpawnEnd;
    bool isAttack;

    // } animation var


    // { Var for summonBullet
    float xValue;
    float yValue;
    Vector2 directionForSummonBullet;
    Vector2 summonBulletPosition;
    float angleForSummonBullet;
    // } Var for summonBullet

    //If FSM R&D, Rebuild animaion



    // { Var for FindPlayerDirection
    float minDist = default;
    float getDist;
    float minDistArrayIndex;

    Vector2 dir1;
    Vector2 dir2;
    Vector2 dir3;
    Vector2 dir4;
    Vector2 dir5;
    Vector2 dir6;
    Vector2 dir7;
    Vector2 dir8;

    Vector2[] directionArray = new Vector2[8];
    // } Var for FindPlayerDirection


    // { Var for PathFinder
    GameObject pathFinder;
    RectTransform rectTransform;

    public List<Vector2> completePath;
    public bool isPathFind;
    // } Var for PathFinder

    int damageTaken;

    ObjectPool objectPool;
    List<GameObject> enemyBulletPool;
    GameObject enemyBulletPrefab;
    public Room belongRoom = default;

    bool isDrop;

    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;

        eye = transform.GetChild(1).gameObject.GetComponent<TestEnemyEye>();
        bodyImage = transform.GetChild(0).GetComponent<Image>();
        bodyImageRectTransform = transform.GetChild(0).GetComponent<RectTransform>();
        rigid = GetComponent<Rigidbody2D>();

        if (attackType == 0)
        {
            hand = transform.GetChild(2).gameObject;
            weapon = hand.transform.GetChild(0).GetComponent<TestEnemyWeapon>();
        }

        rectTransform = GetComponent<RectTransform>();
        player = GameObject.FindWithTag("Player");

        StartCoroutine(SpawnTime());
        //moveSpeed = 0.3f;


        objectPool = GameObject.Find("ObjectPool").GetComponent<ObjectPool>();
        enemyBulletPool = objectPool.enemyBulletPool;
        enemyBulletPrefab = objectPool.enemyBulletPrefab;
    }

    // Update is called once per frame
    void Update()
    {
        ImageSizeSet();
        

        if (!IsSpawnEnd) { }
        else
        {

            isMove = false;
            isAttack = transform.GetChild(0).GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("attack");

            dist = Vector2.Distance(transform.position, player.transform.position);

            if (dist > 500 / 71.94f && !isAttack)
            {
   
                Move();
            }
            else { }

            // { Raycast To Player & Condition Check RayCast Hit
            //Debug.Log(eye.hit.collider.tag);
            if (eye.hit != default)
            {
                if (eye.hit.collider.tag == "Player")
                {
                    Attack();
                }
                else if (eye.hit.collider.tag != "Player" && !isAttack)
                {
                    Move();
                }
            }

            // } Raycast To Player & Condition Check RayCast Hit


            // { set var for using animation
            direction = player.transform.position - transform.position;
            transform.GetChild(0).GetComponent<Animator>().SetFloat("inputX", direction.x);
            transform.GetChild(0).GetComponent<Animator>().SetFloat("inputY", direction.y);
            // ] set var for using animation
        }

        if (isMove)
        {
            transform.GetChild(0).GetComponent<Animator>().SetBool("IsMove", true);
        }
        else
        {
            //transform.GetChild(0).GetComponent<Animator>().SetBool("IsMove", false);
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            transform.GetChild(0).GetComponent<Animator>().SetBool("IsMove", false);
        }

        FindPlayerDirection();

        //-working
        if (!isPathFind)
        {
            //PathFind();
        }
        //-working

        //
        if (currentHp <= 0)
        {
            isDead = true;
            if (!isDrop)
            {
                DropCoin();
            }
            Die();
        }

        // /Debug.Log(EnemyManager.Instance.enemyName.BinarySearch(enemyName));



    }
    void Move()
    {
        isMove = true;

        GetComponent<Rigidbody2D>().velocity = direction.normalized * moveSpeed * 10;

        //transform.localPosition = Vector3.MoveTowards(transform.localPosition, GameObject.Find("TestPlayer").transform.localPosition, moveSpeed);
        //transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * 10 * Time.deltaTime);
    }
    void Attack()
    {
        // use weapon
        if (attackType == 0)
        {
            weapon.fire();
        }
        // summon bullet
        else if (attackType == 1)
        {
            if (!isDelayEnd)
            {
                isDelayEnd = true;
                transform.GetChild(0).GetComponent<Animator>().SetTrigger("IsAttack");

                //bookllet casttime_ = 1, gunNut casttime_ = 0.5f
                StartCoroutine(Fire(1, 5));
            }
        }
        else
        {

        }
    }
    void ImageSizeSet()
    {
        bodyImage.SetNativeSize();
        bodyImageRectTransform.sizeDelta = new Vector2(bodyImageRectTransform.sizeDelta.x * 3, bodyImageRectTransform.sizeDelta.y * 3);
    }
    IEnumerator SpawnTime()
    {
        yield return new WaitForSeconds(1.5f);
        IsSpawnEnd = true;
        transform.GetChild(0).GetComponent<Animator>().SetBool("IsSpawnEnd", true);
    }

    IEnumerator Fire(float castTime_, float delayTime_)   //castime & delaytime < scriptableobj에 값 추가 예정
    {
        yield return new WaitForSeconds(castTime_);
        // { enemy pattern
        if (this.name == "bookllet")
        {
            int patternNum_ = Random.Range(0, 2);

            if (patternNum_ == 0)
            {
                float[] xPos_ = { -30, -30, -30, -30, -30, -30, -30, -15, -8.35f, 15, 30, 35, 30, 15, -8.35f, -15, 15, 25, 35 };
                float[] yPos_ = { -45, -30, -15, 0, 15, 30, 45, 45, 45, 45, 40, 20, 5, 0, 0, 0, -15, -30, -45 };
                for (int i = 0; i < 19; i++)
                {
                    GameObject clone_ = objectPool.GetObject(enemyBulletPool, enemyBulletPrefab, 2);
                    //clone_.GetComponent<RectTransform>().localScale = Vector3.one;
                    clone_.GetComponent<TestBullet>().bulletType = 1;
                    clone_.GetComponent<TestBullet>().enemyName = enemyName;


                    clone_.transform.position = new Vector2(transform.position.x + xPos_[i] / 71.94f, transform.position.y + yPos_[i] / 71.94f);
                    //clone_.transform.localPosition = new Vector2(transform.localPosition.x + xPos_[i], transform.localPosition.y + yPos_[i]);
                    //clone_.transform.SetParent(GameObject.Find("GameObjs").transform);
                    yield return new WaitForSeconds(0.03f);
                }
            }
            else if (patternNum_ == 1)
            {
                for (float i = 0; i <= 360; i += 10)
                {
                    GameObject clone_ = objectPool.GetObject(enemyBulletPool, enemyBulletPrefab, 2);
                    //clone_.GetComponent<RectTransform>().localScale = Vector3.one;
                    //GameObject clone_ = Instantiate(Resources.Load<GameObject>("02.HT/Prefabs/TestBullet"), transform.position, transform.rotation);
                    clone_.GetComponent<TestBullet>().bulletType = 0;
                    clone_.GetComponent<TestBullet>().enemyName = enemyName;
                    //clone_.transform.SetParent(GameObject.Find("GameObjs").transform);

                    angleForSummonBullet = i * Mathf.PI / 180.0f;
                    xValue = Mathf.Cos(angleForSummonBullet);
                    yValue = Mathf.Sin(angleForSummonBullet);
                    directionForSummonBullet = new Vector2(xValue, yValue);
                    summonBulletPosition = (Vector2)transform.position + directionForSummonBullet * 50 / 71.94f;

                    clone_.transform.position = summonBulletPosition;
                    StartCoroutine(WaitToSummonAllBullet(clone_, directionForSummonBullet.normalized));
                    //clone_.GetComponent<Rigidbody2D>().AddForce(directionForSummonBullet.normalized * 5, ForceMode2D.Impulse);
                }
                for (int i = 0; i <= 60; i += 10)
                {
                    GameObject clone_ = objectPool.GetObject(enemyBulletPool, enemyBulletPrefab, 2);
                    //clone_.GetComponent<RectTransform>().localScale = Vector3.one;
                    //GameObject clone_ = Instantiate(Resources.Load<GameObject>("02.HT/Prefabs/TestBullet"), transform.position, transform.rotation);
                    clone_.GetComponent<TestBullet>().bulletType = 0;
                    clone_.GetComponent<TestBullet>().enemyName = enemyName;
                    //clone_.transform.SetParent(GameObject.Find("GameObjs").transform);
                    clone_.transform.position = new Vector2(transform.position.x, transform.position.y + i / 71.94f);
                    StartCoroutine(WaitToSummonAllBullet(clone_, clone_.transform.up.normalized));
                }
                for (int i = 0; i >= -60; i -= 10)
                {
                    GameObject clone_ = objectPool.GetObject(enemyBulletPool, enemyBulletPrefab, 2);
                    //clone_.GetComponent<RectTransform>().localScale = Vector3.one;
                    //GameObject clone_ = Instantiate(Resources.Load<GameObject>("02.HT/Prefabs/TestBullet"), transform.position, transform.rotation);
                    clone_.GetComponent<TestBullet>().bulletType = 0;
                    clone_.GetComponent<TestBullet>().enemyName = enemyName;
                    //clone_.transform.SetParent(GameObject.Find("GameObjs").transform);
                    clone_.transform.position = new Vector2(transform.position.x, transform.position.y + i / 71.94f);
                    StartCoroutine(WaitToSummonAllBullet(clone_, -clone_.transform.up.normalized));
                }
            }
        }

        if (this.name == "gunNut")
        {
            for (float i = minDistArrayIndex * 45; i <= minDistArrayIndex * 45 + 90; i += 5)
            {
                GameObject clone_ = objectPool.GetObject(enemyBulletPool, enemyBulletPrefab, 2);
                //clone_.GetComponent<RectTransform>().localScale = Vector3.one;
                //GameObject clone_ = Instantiate(Resources.Load<GameObject>("02.HT/Prefabs/TestBullet"), transform.position, transform.rotation);
                clone_.GetComponent<TestBullet>().bulletType = 0;
                clone_.GetComponent<TestBullet>().enemyName = enemyName;
                //clone_.transform.SetParent(GameObject.Find("GameObjs").transform);

                angleForSummonBullet = i * Mathf.PI / 180.0f;
                xValue = Mathf.Cos(angleForSummonBullet);
                yValue = Mathf.Sin(angleForSummonBullet);
                directionForSummonBullet = new Vector2(xValue, yValue);
                summonBulletPosition = (Vector2)transform.position + directionForSummonBullet * 50 / 71.94f;

                clone_.transform.position = summonBulletPosition;
                clone_.GetComponent<Rigidbody2D>().AddForce(directionForSummonBullet.normalized * 5, ForceMode2D.Impulse);
            }
        }
        // } enemy pattern
        yield return new WaitForSeconds(delayTime_);
        isDelayEnd = false;
    }

    IEnumerator WaitToSummonAllBullet(GameObject clone_, Vector2 direction_)
    {
        yield return new WaitForSeconds(1);
        clone_.GetComponent<Rigidbody2D>().AddForce(direction_ * 5, ForceMode2D.Impulse);

    }

    // @brief find player's direction from current enemy position.
    void FindPlayerDirection()
    {
        dir1 = (Vector2.up + Vector2.right).normalized;
        dir2 = (Vector2.up).normalized;
        dir3 = (Vector2.left + Vector2.up).normalized;
        dir4 = Vector2.left.normalized;
        dir5 = (Vector2.down + Vector2.left).normalized;
        dir6 = (Vector2.down).normalized;
        dir7 = (Vector2.right + Vector2.down).normalized;
        dir8 = (Vector2.right).normalized;


        Vector2[] directionArray_ = { dir1, dir2, dir3, dir4, dir5, dir6, dir7, dir8 };

        directionArray = directionArray_;

        for (int i = 0; i < directionArray_.Length; i++)
        {
            getDist = Vector2.Distance((player.transform.position - transform.position).normalized, directionArray_[i]);
            if (i == 0 || minDist > getDist)
            {
                minDist = getDist;
            }
            else { }

            if (minDist == getDist)
            {
                minDistArrayIndex = i;
            }
            else { }
        }
    }

    bool isCreatedPathFinder;
    void PathFind()
    {
        if (!isCreatedPathFinder)
        {
            pathFinder = Instantiate(Resources.Load<GameObject>("02.HT/Prefabs/PathFinder/PathFinder"));
            pathFinder.transform.SetParent(transform.parent);

            //pathFinder.GetComponent<RectTransform>().sizeDelta = transform.parent.GetComponent<RectTransform>().sizeDelta;
            pathFinder.GetComponent<RectTransform>().sizeDelta = transform.parent.GetComponent<Room>().roomSize;

            //pathFinder.GetComponent<RectTransform>().localScale = Vector3.one;

            pathFinder.GetComponent<GridLayoutGroup>().cellSize = rectTransform.sizeDelta * 0.0139f;

            pathFinder.name = $"{this.name}" + "PathFinder";

            pathFinder.GetComponent<PathFinder>().enemy = this.gameObject;
        }
        else
        {
            for (int i = 0; i < pathFinder.transform.childCount; i++)
            {
                pathFinder.transform.GetChild(i).gameObject.SetActive(true);
            }
            //pathFinder.SetActive(true);
        }
        isPathFind = true;
        isCreatedPathFinder = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerBullet")
        {
            damageTaken = other.GetComponent<PlayerBullet>().bulletDamage; //after setting playerbullet, change this.
            currentHp -= damageTaken;
            damageTaken = 0;

            OnDamage(other.transform.position);
        }
    }

    void OnDamage(Vector3 collisionPos_)
    {
        Vector2 dir_ = transform.position - collisionPos_;

        rigid.velocity = Vector2.zero;
        rigid.AddForce(dir_.normalized * 100, ForceMode2D.Impulse);//?
    }

    void Die()
    {
        if (isDead == true)
        {
            isDead = false;

            belongRoom.enemyCount--; // KJH ADD
            StopAllCoroutines();
            gameObject.SetActive(false);

            //Destroy(this.gameObject); will be add amimation event
        }
    }


    void DropCoin()
    {
        isDrop = true;

        if (dropCoinCount < 5)
        {
            GameObject coinPrefab_ = Resources.Load<GameObject>("02.HT/Prefabs/CoinPrefab1");

            for (int i = 0; i < dropCoinCount; i++)
            {
                float rangeXPos = Random.Range(-1, 1);
                float rangeYPos = Random.Range(-1, 1);
                GameObject clone_ = Instantiate(coinPrefab_, transform.parent);
                clone_.transform.position = new Vector2(transform.position.x + rangeXPos, transform.position.y + rangeYPos);
            }
        }
        else
        {
            GameObject coinPrefab1_ = Resources.Load<GameObject>("02.HT/Prefabs/CoinPrefab1");
            GameObject coinPrefab2_ = Resources.Load<GameObject>("02.HT/Prefabs/CoinPrefab2");
            GameObject clone2_ = Instantiate(coinPrefab2_, transform.parent);
            float rangeXPos = Random.Range(-1, 1);
            float rangeYPos = Random.Range(-1, 1);
            clone2_.transform.position = new Vector2(transform.position.x + rangeXPos, transform.position.y + rangeYPos);

            for (int i = 0; i < dropCoinCount - 5; i++)
            {
                rangeXPos = Random.Range(-1, 1);
                rangeYPos = Random.Range(-1, 1);
                GameObject clone1_ = Instantiate(coinPrefab1_, transform.parent);
                clone1_.transform.position = new Vector2(transform.position.x + rangeXPos, transform.position.y + rangeYPos);
            }

        }
    }
}

