using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGorgunBody : MonoBehaviour
{
    float angleForSummonBullet;
    float xValue;
    float yValue;
    Vector2 directionForSummonBullet;

    public int patternCount = 0;

    BossGorgunMuzzle muzzleLeft;
    BossGorgunMuzzle muzzleRight;

    Transform eye;

    RectTransform rectTransform;

    ObjectPool objectPool;
    List<GameObject> enemyBulletPool;
    GameObject enemyBulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
        eye = transform.parent.GetChild(1);
        rectTransform = GetComponent<RectTransform>();

        muzzleLeft = transform.parent.GetChild(2).GetChild(0).GetComponent<BossGorgunMuzzle>();
        muzzleRight = transform.parent.GetChild(2).GetChild(1).GetComponent<BossGorgunMuzzle>();

        objectPool = GameObject.Find("ObjectPool").GetComponent<ObjectPool>();
        enemyBulletPool = objectPool.enemyBulletPool;
        enemyBulletPrefab = objectPool.enemyBulletPrefab;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // { add function to animation event 
    public void ShotBulletAttack1Pattern1()
    {
        for (float i = 0; i <= 360; i += 15)
        {
            GameObject clone_ = objectPool.GetObject(enemyBulletPool, enemyBulletPrefab, 2);
            clone_.transform.position = transform.position;
            clone_.GetComponent<RectTransform>().localScale = Vector3.one;
            clone_.GetComponent<TestBullet>().bulletType = 0;

            angleForSummonBullet = i * Mathf.PI / 180.0f;
            xValue = Mathf.Cos(angleForSummonBullet);
            yValue = Mathf.Sin(angleForSummonBullet);
            directionForSummonBullet = new Vector2(xValue, yValue);
            clone_.GetComponent<Rigidbody2D>().AddForce(directionForSummonBullet.normalized * 5, ForceMode2D.Impulse);
        }
    }
    public void ShotBulletAttack2Pattern1()
    {
        for (float i = 5; i <= 360; i += 15)
        {
            GameObject clone_ = objectPool.GetObject(enemyBulletPool, enemyBulletPrefab, 2);
            clone_.transform.position = transform.position;
            clone_.GetComponent<RectTransform>().localScale = Vector3.one;
            clone_.GetComponent<TestBullet>().bulletType = 0;

            angleForSummonBullet = i * Mathf.PI / 180.0f;
            xValue = Mathf.Cos(angleForSummonBullet);
            yValue = Mathf.Sin(angleForSummonBullet);
            directionForSummonBullet = new Vector2(xValue, yValue);
            clone_.GetComponent<Rigidbody2D>().AddForce(directionForSummonBullet.normalized * 5, ForceMode2D.Impulse);
        }
    }
    public void AttackPattern2()
    {
        GameObject clone_ = Instantiate(Resources.Load<GameObject>("02.HT/Prefabs/BossGorgun/GorgunSonicWave"), eye.transform.position, transform.rotation);
        clone_.transform.SetParent(GameObject.Find("GameObjs").transform);
        clone_.GetComponent<RectTransform>().localScale = Vector3.one;
    }

    public void AttackPattern3(int rotateEmptySpace_)
    {
        for (float i = 0; i <= 360; i += 10)
        {
            if (i >= 0 + rotateEmptySpace_ && i < 45 + rotateEmptySpace_ || i >= 120 + rotateEmptySpace_ && i < 165 + rotateEmptySpace_ || i >= 240 + rotateEmptySpace_ && i < 285 + rotateEmptySpace_)
            {
                GameObject clone_ = objectPool.GetObject(enemyBulletPool, enemyBulletPrefab, 2);
                clone_.transform.position = eye.transform.position;
                clone_.GetComponent<RectTransform>().localScale = Vector3.one;

                clone_.GetComponent<TestBullet>().isGorgunBullet = true;

                angleForSummonBullet = i * Mathf.PI / 180.0f;
                xValue = Mathf.Cos(angleForSummonBullet);
                yValue = Mathf.Sin(angleForSummonBullet);
                directionForSummonBullet = new Vector2(xValue, yValue);
                clone_.GetComponent<Rigidbody2D>().AddForce(directionForSummonBullet.normalized * 5, ForceMode2D.Impulse);
                //Destroy(clone_, 3);
            }
            else
            {
                GameObject clone_ = objectPool.GetObject(enemyBulletPool, enemyBulletPrefab, 2);
                clone_.transform.position = eye.transform.position;
                clone_.GetComponent<RectTransform>().localScale = Vector3.one;
                clone_.GetComponent<TestBullet>().bulletType = 0;

                angleForSummonBullet = i * Mathf.PI / 180.0f;
                xValue = Mathf.Cos(angleForSummonBullet);
                yValue = Mathf.Sin(angleForSummonBullet);
                directionForSummonBullet = new Vector2(xValue, yValue);
                clone_.GetComponent<Rigidbody2D>().AddForce(directionForSummonBullet.normalized * 5, ForceMode2D.Impulse);
            }
        }
    }

    public void CountCheck()
    {
        patternCount++;
    }

    public void MoveMuzzle()
    {
        muzzleLeft.MoveCheck();
        muzzleRight.MoveCheck();
    }
    public void ResetMuzzleMoveCount()
    {
        muzzleLeft.ResetMoveCount();
        muzzleRight.ResetMoveCount();
    }

    public void ShotBulletStart()
    {
        muzzleLeft.isShotBullet = true;
        muzzleRight.isShotBullet = true;
    }

    public void ShotBulletEnd()
    {
        muzzleLeft.isShotBullet = false;
        muzzleRight.isShotBullet = false;
    }

    public void FlipImage(int rotateValue_)
    {
        Vector3 rotation_ = new Vector3(0, rotateValue_, 0);
        rectTransform.rotation = Quaternion.Euler(rotation_);

    }

    public void ChangeGorgunState()
    {
        transform.parent.GetComponent<BossGorGun>().isChangeTostatue = true;
    }

    // } add function to animation event 

}
