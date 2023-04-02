using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonrakerWeapon : PlayerWeapon
{

    // 레이저 길이
    [SerializeField]
    private float defDistanceRay = default;

    // 레이저 발사 위치
    private Vector3 firstPos = default;

    // 레이저 발사 방향
    private Vector3 directLaser = default;

    public LineRenderer moonLineRenderer = default;
    public Transform moonTransform = default;

    private int layerMask = default;

    // 반사되는 최대 횟수
    public int reflectMax = default;


    public bool isLoopActive = true;
    public bool isLaserOn = false;
    public bool isChkMagazine = false;

    public int originBulletDamage = default;



    private void OnEnable()
    {
        PlayerManager.Instance.player.nowWeaponHand = weaponHand;

    }

    void Awake()
    {
        SetWeaponData();
    }

    // Start is called before the first frame update
    void Start()
    {

        moonLineRenderer = gameObject.GetComponentMust<LineRenderer>();
        moonTransform = gameObject.GetComponentMust<Transform>();
        
        isChkMagazine = false;
        moonLineRenderer.enabled = false;
        countBullet = weaponMagazine;

        deleyChkVal = 0f;
        reflectMax = 2;

        isLoopActive = false;
        isLaserOn = false;

        firstPos = new Vector3();
        directLaser = new Vector3();


        // 레이저 길이
        defDistanceRay = (int)bulletRange;
        gameObject.transform.localPosition = weaponPos;


        layerMask = (1 << LayerMask.NameToLayer("Player"))
            | (1 << LayerMask.NameToLayer("Bullet")
            | (1 << LayerMask.NameToLayer("Ignore Raycast"))
            );
        layerMask = ~layerMask;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isLaserOn == true)
        {
            if(isChkMagazine == false)
            {
                StartMinusMagazine();
                isChkMagazine = true;
            }

            AttackLaser();

            
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            //[KJH] ADD
            SoundManager.Instance.Stop(Sound.SFX);
            OffLaser();
        }


        deleyChkVal += Time.deltaTime;
    }

    public override void FireBullet()
    {
        isLaserOn = true;
        //[KJH] ADD
        SoundManager.Instance.Play("MoonRaker/moonrakerLaser_shot_01", Sound.SFX);
    }

    public override void ReloadBullet()
    {
        base.ReloadBullet();
    }

    public void AttackLaser()
    {
        if(moonLineRenderer.enabled == false)
        {
            //[KJH] ADD
            SoundManager.Instance.Play("MoonRaker/moonrakerLaser_loop_01", Sound.SFX);
        }
        moonLineRenderer.enabled = true;
        isLoopActive = true;

        firstPos = firePos.position;

        int countLaser_ = 1;

        directLaser = transform.right;

        // 라인렌더러 굴기
        moonLineRenderer.startWidth = 0.20f;
        moonLineRenderer.endWidth = 0.20f;


        moonLineRenderer.numCornerVertices = 20;
        // 라인렌더러 끝부분의 둥글기
        moonLineRenderer.numCapVertices = 6;
        moonLineRenderer.positionCount = countLaser_;
        moonLineRenderer.SetPosition(0, firstPos);

        while(isLoopActive == true)
        {
            RaycastHit2D hit_ = Physics2D.Raycast(firstPos, directLaser, defDistanceRay, layerMask);

            if(hit_ != default)
            {
                Debug.Log(hit_.collider.name);
                Vector3 temp_ = firstPos;

                countLaser_++;
                moonLineRenderer.positionCount = countLaser_;
                directLaser = Vector3.Reflect(directLaser, hit_.normal);
                firstPos = (Vector2) directLaser.normalized + hit_.point;
                moonLineRenderer.SetPosition(countLaser_ - 1, hit_.point);


                // 레이저 공격
                if (weaponDeley < deleyChkVal)
                {
                    if (hit_.collider.tag == "Monster")
                    {
                        // 적 몬스터 스크립트에 접근하여 체력을 깍는 작동을 한다
                        bulletDamage = originBulletDamage + PlayerManager.Instance.player.playerDamage;
                        if (hit_.collider.transform.parent.gameObject.GetComponentMust<TestEnemy>() != null)
                        {
                            hit_.collider.transform.parent.gameObject.GetComponentMust<TestEnemy>().currentHp -= bulletDamage;
                        }
                        else if(hit_.collider.transform.parent.GetComponent<BossGorGun>() != null)
                        {
                            hit_.collider.transform.parent.GetComponent<BossGorGun>().currentHp -= bulletDamage;
                        }
                        else if (hit_.collider.transform.GetComponent<BulletKingController>() != null)
                        {
                            hit_.collider.transform.GetComponent<BulletKingController>().currentHp -= bulletDamage;
                        }
                    }
                }
            }
            else
            {
                countLaser_++;
                moonLineRenderer.positionCount = countLaser_;
                moonLineRenderer.SetPosition(countLaser_ - 1, firstPos + (directLaser.normalized * defDistanceRay));
                isLoopActive = false;
            }

            if(reflectMax < countLaser_)
            {
                isLoopActive = false;
            }


        }

        
    }


    public void OffLaser()
    {
        StopMinusMagazine();
        isLaserOn = false;
        isChkMagazine = false;
        moonLineRenderer.enabled = false;
    }



    IEnumerator MinusMagazine = default;

    void StartMinusMagazine()
    {
        MinusMagazine = MinusWeaponMagazine();
        StartCoroutine(MinusMagazine);
    }

    void StopMinusMagazine()
    {
        if(MinusMagazine != null)
        {
            StopCoroutine(MinusMagazine);
        }
    }


    IEnumerator MinusWeaponMagazine()
    {
        while (isLaserOn == true)
        {
            yield return new WaitForSeconds(weaponDeley);

            weaponMagazine--;
        }

    }

    public override void SetWeaponData()
    {
        base.SetWeaponData();
        this.weaponName = "문스크래퍼";
        this.weaponDesc = "비이이이이오오옹";
        this.weaponType = "공구";
        this.weaponDataTxt = "이 강력한 레이저는 테라포밍 프로젝트에서 고속으로 월면석을 깎아낼 목적으로 설계된 것입니다.";

        this.weaponPos = new Vector3(5f, 2f, 0f);

        // -1은 재장전이 없다는 의미다.
        this.weaponReload = -1;

        this.weaponMagazine = 700;

        this.weaponBulletValue = 700;
        this.knockBack = 20f;

        // default는 탄속이 없다는 의미다.
        this.bulletSpeed = default;

        this.bulletDamage = 9;
        originBulletDamage = bulletDamage;

        this.bulletRange = 30f;
        this.bulletShotRange = 5;
        this.weaponDeley = 0.1f;
        this.weaponHand = 1;
    }
}
