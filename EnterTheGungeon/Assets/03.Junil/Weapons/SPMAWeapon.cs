using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPMAWeapon : PlayerWeapon
{
    private GameObject spmaBulletPrefab = default;
    private GameObject[] spmaBullets = default;
    private Animator spmaAnimator = default;


    // 탄약이 비어있다면 참이 되는 bool 값
    public bool isEmptyBullet = false;

    private void OnEnable()
    {

        // 만약 켜질때 장탄 수가 0이면 다시 재장전해주는 조건
        if (countBullet == 0)
        {
            isEmptyBullet = true;
            isReload = false;
        }
        if(PlayerManager.Instance.player.gameObject == default)
        {
            Debug.Log("스크립트 오더 에러");
        }
        PlayerManager.Instance.player.nowWeaponHand = weaponHand;
    }

    //! 꺼질 때 만약 코루틴이 돌고 있다면 끌 것이다
    private void OnDisable()
    {
        StopOnReload();
    }


    void Awake()
    {
        SetWeaponData();
    }


    // Start is called before the first frame update
    void Start()
    {
        spmaBulletPrefab = Resources.Load<GameObject>("03.Junil/Prefabs/SPMA_Bullet");

        isAttack = false;

        // 재장전 중인지를 체크하는 bool 값
        isReload = false;
        deleyChkVal = 0f;
        isEmptyBullet = false;

        countBullet = weaponMagazine;

        gameObject.transform.localPosition = weaponPos;

        spmaBullets = new GameObject[weaponMagazine];

        Vector3 saveBulletPos_ = new Vector3(0f, 0f, 0f);

        for(int i = 0; i < spmaBullets.Length; i++)
        {
            spmaBullets[i] = Instantiate(spmaBulletPrefab, saveBulletPos_,
                Quaternion.identity, bulletObjs.transform);
            spmaBullets[i].GetComponent<SPMABulletMove>().SetBulletData(this);

            spmaBullets[i].SetActive(false);
        }


    }

    // Update is called once per frame
    void Update()
    {
        deleyChkVal += Time.deltaTime;

        if (isEmptyBullet == true)
        {
            isEmptyBullet = false;
            spmaAnimator.SetTrigger("EndReload");

            GFunc.Log("재장전 실행됨");
            ReloadBullet();
        }
        
    }


    public override void FireBullet()
    {
        
        GFunc.Log("공격함");

        if(isReload == true) { return; }

        if (weaponDeley <= deleyChkVal)
        {
            deleyChkVal = 0f;
            isAttack = false;
        }
        else { return; }



        if (isAttack == false)
        {

            isAttack = true;
            spmaAnimator.SetTrigger("OnAttack");


            spmaBullets[countBullet - 1].transform.position = firePos.position;

            spmaBullets[countBullet - 1].transform.rotation = gameObject.transform.rotation;

            spmaBullets[countBullet - 1].transform.Rotate(new Vector3(0f, 0f, -90f));

            spmaBullets[countBullet - 1].SetActive(true);

            float shotRange_ = Random.Range(bulletShotRange * -1, bulletShotRange + 1);

            spmaBullets[countBullet - 1].transform.Rotate(new Vector3(0f, 0f, shotRange_));



            countBullet--;

            if (countBullet == 0)
            {
                isEmptyBullet = true;

            }
        }
    }



    public override void ReloadBullet()
    {
        // 현재 재장전 중이거나 현재 총알 수가 최대 총알 수와 같다면 멈추게 하는 조건
        if (isReload == true || countBullet == weaponMagazine ) { return; }

        spmaAnimator.SetTrigger("OnReload");

        PlayerManager.Instance.player.weaponReload.ReloadStart(weaponReload);
        StartOnReload();

    }

    

    public override void SetWeaponData()
    {
        base.SetWeaponData();

        this.weaponName = "해병 휴대 무기";
        this.weaponDescription = "항상 그대 곁에\r\n" +
            "무한 탄환입니다. 비밀 벽을 드러내지 않습니다. 프라이머다인의 하급 병사가 총굴로 가지고 온 해병 휴대 무기입니다.\r\n" +
            "튼튼한 총처럼 보이지만, 정작 필요할 때는 오작동을 일으키는 것으로 알려졌습니다.";

        this.weaponPos = new Vector3(5f, 2f, 0f);
        this.weaponReload = 1.2f;
        this.weaponMagazine = 10;

        // -1은 탄약량이 무제한이라는 의미이다.
        this.weaponBulletValue = -1;
        this.knockBack = 12f;
        this.bulletSpeed = 11f;
        this.bulletDamage = 5;
        this.bulletRange = 18f;
        this.bulletShotRange = 5;
        this.weaponDeley = 0.25f;
        this.weaponHand = 1;

        spmaAnimator = gameObject.GetComponentMust<Animator>();
    }
}
