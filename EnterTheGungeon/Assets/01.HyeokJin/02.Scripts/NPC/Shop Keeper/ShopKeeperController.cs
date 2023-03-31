using System.Collections;
using UnityEngine;

public class ShopKeeperController : MonoBehaviour
{
    private Animator shopkeeperAnimator = default;
    private ObjectManager objectManager = default;
    private GameObject player = default;
    public Room belongRoom = default;

    private bool isInShop = false;
    private bool isPlayerShoot = false;
    public bool isTargetFront = false;
    public bool isTargetRight = false;
    public bool isTargetRightBelow = false;

    private int shootCount = default;
    private int curPatternCount = default;
    private int maxPatternCount = default;

    private float bulletCount = default;
    private float bulletSpeed = default;
    private float enemyRadius = default;

    private void Awake()
    {
        shopkeeperAnimator = GetComponent<Animator>();
        objectManager = GameObject.Find("ObjectManager").GetComponent<ObjectManager>();
        player = PlayerManager.Instance.player.gameObject;
    }

    private void Update()
    {

        TargetPositionCheck();
        ShootCheck();
        EnterShop();
        Status();
    }

    private void Start()
    {
        curPatternCount = 0;
    }

    #region Status
    private void Status()
    {
        if (isInShop)
        {
            switch (shootCount)
            {
                case 0:
                    /* Do Nothing */
                    break;
                case 1:
                    Warning();
                    break;
                case 2:
                    Threat();
                    break;
                default:
                    OpenFire();
                    break;
            }
        }
    }   //  Status()
    #endregion

    #region EnterShop
    private void EnterShop()
    {
        if (true)
        {
            isInShop = true; // 임시 : true
        }
    }   //  EnterShop()
    #endregion

    #region ShootCheck
    private void ShootCheck()
    {
        if (Input.GetMouseButtonDown(0) && belongRoom.isPlayerEnter)    //  임시 : 마우스 왼클릭(발사) 한 경우
        {
            isPlayerShoot = true;
            shootCount++;
        }
    }   //  ShootCheck()
    #endregion

    #region Warning
    private void Warning()
    {
        //  플레이어가 공격하면(1회) 경고함
        if (isPlayerShoot && shootCount == 1)
        {
            shopkeeperAnimator.SetBool("isWarning", true);
            isPlayerShoot = false;
            StartCoroutine("Warning_AnimationControll");
        }
    }   //  Warning()

    #region Warning_AnimationControll
    IEnumerator Warning_AnimationControll()
    {
        yield return new WaitForSeconds(5.2f);
        shopkeeperAnimator.SetBool("isWarning", false);
    }   //  Warning_AnimationControll()
    #endregion
    #endregion

    #region TargetPositionCheck
    private void TargetPositionCheck()
    {
        Vector2 distance = player.transform.position - transform.position;

        if (distance.x < 4)
        {
            isTargetFront = true;
            isTargetRightBelow = false;
            isTargetRight = false;
        }
        else if (distance.x > 4 && distance.y < -2)
        {
            isTargetFront = false;
            isTargetRightBelow = true;
            isTargetRight = false;
        }
        else if (distance.y > -2)
        {
            isTargetFront = false;
            isTargetRightBelow = false;
            isTargetRight = true;
        }
    }   //  TargetPositionCheck()
    #endregion

    #region Threat
    private void Threat()
    {
        //  플레이어가 공격하면(2회) 샷건을 들고 위협함
        if (isPlayerShoot && shootCount == 2)
        {
            shopkeeperAnimator.SetBool("isThreat", true);
            isPlayerShoot = false;
        }
        StartCoroutine("Threat_AnimationControll");
    }   //  Threat()

    #region Threat_AnimationControll
    IEnumerator Threat_AnimationControll()
    {
        yield return new WaitForSeconds(0.1f);
        shopkeeperAnimator.SetBool("isThreat", false);

        if (isTargetFront)
        {
            shopkeeperAnimator.SetBool("isThreatFront", true);
            shopkeeperAnimator.SetBool("isThreatRightBelow", false);
            shopkeeperAnimator.SetBool("isThreatRight", false);
        }
        else if (isTargetRightBelow)
        {
            shopkeeperAnimator.SetBool("isThreatFront", false);
            shopkeeperAnimator.SetBool("isThreatRightBelow", true);
            shopkeeperAnimator.SetBool("isThreatRight", false);
        }
        else if (isTargetRight)
        {
            shopkeeperAnimator.SetBool("isThreatFront", false);
            shopkeeperAnimator.SetBool("isThreatRightBelow", false);
            shopkeeperAnimator.SetBool("isThreatRight", true);
        }
    }   //  Threat_AnimationControll()
    #endregion Threat_AnimationControll
    #endregion

    private void DoublePrice()
    {
        //  플레이어가 공격(2회)하면 이루부터 상점 내 모든 아이템의 가격이 두 배가 됨
    }

    #region OpenFire
    private void OpenFire()
    {
        //  플레이어가 공격하면 샷건을 난사함
        if (isPlayerShoot && shootCount == 3)
        {
            isPlayerShoot = false;
            OpenFire_Pattern();
        }
        StartCoroutine("OpenFire_AnimationControll");
    }   //  OpenFire()

    #region OpenFire_Pattern
    private void OpenFire_Pattern()
    {
        bulletSpeed = 8f;
        maxPatternCount = 20;
        bulletCount = 18;

        for (int i = 0; i < bulletCount; i++)
        {

            GameObject bullet = objectManager.MakeObject("Bullet_Basic");

            if (isTargetFront)
            {
                bullet.transform.position = transform.position + new Vector3(0.3f, -0.3f, 0f);
            }
            else if (isTargetRightBelow)
            {
                bullet.transform.position = transform.position + new Vector3(1f, -0.5f, 0f);
            }
            else if (isTargetRight)
            {
                bullet.transform.position = transform.position + new Vector3(1.8f, 0f, 0f);
            }

            bullet.transform.rotation = Quaternion.identity;

            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            Vector2 direction = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / bulletCount), Mathf.Sin(Mathf.PI * 2 * i / bulletCount));
            bulletRigidbody.AddForce(direction.normalized * bulletSpeed, ForceMode2D.Impulse);
        }

        for (int i = 0; i < bulletCount; i++)
        {
            GameObject bullet = objectManager.MakeObject("Bullet_Basic");

            if (isTargetFront)
            {
                bullet.transform.position = transform.position + new Vector3(0.8f, -0.3f, 0f);
            }
            else if (isTargetRightBelow)
            {
                bullet.transform.position = transform.position + new Vector3(1.5f, -0.5f, 0f);
            }
            else if (isTargetRight)
            {
                bullet.transform.position = transform.position + new Vector3(2.3f, 0f, 0f);
            }

            bullet.transform.rotation = Quaternion.identity;

            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            Vector2 direction = new Vector2(Mathf.Sin(Mathf.PI * 2 * i / bulletCount), Mathf.Cos(Mathf.PI * 2 * i / bulletCount));
            bulletRigidbody.AddForce(direction.normalized * bulletSpeed, ForceMode2D.Impulse);
        }

        StartCoroutine("OpenFire_BulletBounce");

        curPatternCount++;

        if (curPatternCount < maxPatternCount)
        {
            Invoke("OpenFire_Pattern", 0.5f);
        }
        else
        {
            Hide();
        }

    }   //  OpenFire_Pattern()
    #endregion

    #region OpenFire_BulletBounce
    IEnumerator OpenFire_BulletBounce()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            bulletSpeed = 5f;
            maxPatternCount = 20;
            bulletCount = 1;

            if (curPatternCount % 4 == 0)
            {
                GameObject bullet = objectManager.MakeObject("Bullet_TypeF");

                if (isTargetFront)
                {
                    bullet.transform.position = transform.position + new Vector3(1f, -0.3f, 0f);
                }
                else if (isTargetRightBelow)
                {
                    bullet.transform.position = transform.position + new Vector3(1.8f, -0.5f, 0f);
                }
                else if (isTargetRight)
                {
                    bullet.transform.position = transform.position + new Vector3(2.5f, 0f, 0f);
                }

                bullet.transform.rotation = Quaternion.identity;

                Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
                Vector2 direction = player.transform.position - bullet.transform.position;
                bulletRigidbody.AddForce(direction.normalized * bulletSpeed, ForceMode2D.Impulse);
            } 

            yield return new WaitForSeconds(0.1f);
        }
    }   //  OpenFire_BulletBounce()
    #endregion

    #region OpenFire_AnimationControll
    IEnumerator OpenFire_AnimationControll()
    {
        yield return new WaitForSeconds(0.1f);

        if (isTargetFront)
        {
            shopkeeperAnimator.SetBool("isThreatFront", true);
            shopkeeperAnimator.SetBool("isThreatRightBelow", false);
            shopkeeperAnimator.SetBool("isThreatRight", false);
        }
        else if (isTargetRightBelow)
        {
            shopkeeperAnimator.SetBool("isThreatFront", false);
            shopkeeperAnimator.SetBool("isThreatRightBelow", true);
            shopkeeperAnimator.SetBool("isThreatRight", false);
        }
        else if (isTargetRight)
        {
            shopkeeperAnimator.SetBool("isThreatFront", false);
            shopkeeperAnimator.SetBool("isThreatRightBelow", false);
            shopkeeperAnimator.SetBool("isThreatRight", true);
        }
    }   //  OpenFire_AnimationControll()
    #endregion
    #endregion

    #region Hide
    private void Hide()
    {
        //  샷건을 난사한 후 책상아래로 숨어서 버튼을 누름
        StartCoroutine("Hide_AnimationControll");
    }

    #region Hide_AnimationControll
    IEnumerator Hide_AnimationControll()
    {
        shopkeeperAnimator.SetTrigger("isHide");
        yield return new WaitForSeconds(4f);
        gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        KickOut();
    }   //  Hide_AnimationControll()
    #endregion
    #endregion

    private void KickOut()
    {
        //  상점 주인이 버튼을 누른 이후 플레이어는 강제로 상점 밖으로 내쫒는다 / 이후 물건을 팔지 않음
    }

    private void PlayerDie()
    {
        //  상점 주인에게 죽으면 플레이어가 정의에 의해 죽었다고 표시된다.
    }
}
