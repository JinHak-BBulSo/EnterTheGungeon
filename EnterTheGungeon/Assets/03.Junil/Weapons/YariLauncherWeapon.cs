using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YariLauncherWeapon : PlayerWeapon
{
    private GameObject missileBulletPrefab = default;
    private GameObject[] missileBullets = default;
    private Animator yariAnimator = default;

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
        if (PlayerManager.Instance.player.gameObject == default)
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
        missileBulletPrefab = Resources.Load<GameObject>("03.Junil/Prefabs/Missile_Bullet");

        isAttack = false;

        // 재장전 중인지를 체크하는 bool 값
        isReload = false;
        deleyChkVal = 0f;
        isEmptyBullet = false;

        countBullet = weaponMagazine;

        gameObject.transform.localPosition = weaponPos;

        missileBullets = new GameObject[weaponMagazine];

        Vector3 saveBulletPos_ = new Vector3(0f, 0f, 0f);

        for (int i = 0; i < missileBullets.Length; i++)
        {
            missileBullets[i] = Instantiate(missileBulletPrefab, saveBulletPos_,
                Quaternion.identity, bulletObjs.transform);
            missileBullets[i].GetComponent<MissileBulletMove>().SetBulletData(this);

            missileBullets[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        deleyChkVal += Time.deltaTime;

        if (isEmptyBullet == true)
        {
            isEmptyBullet = false;
            yariAnimator.SetTrigger("EndReload");

            GFunc.Log("재장전 실행됨");
            ReloadBullet();
        }
    }

    public override void FireBullet()
    {
        

        GFunc.Log("공격함");

        if (isReload == true) { return; }

        if (weaponDeley <= deleyChkVal)
        {
            deleyChkVal = 0f;
            isAttack = false;
        }
        else { return; }



        if (isAttack == false)
        {

            isAttack = true;
            yariAnimator.SetTrigger("OnAttack");

            //[KJH] ADD
            SoundManager.Instance.Play("YariRocket/yarirocketlauncher_shot_01", Sound.SFX);

            missileBullets[countBullet - 1].transform.position = firePos.position;

            missileBullets[countBullet - 1].transform.rotation = gameObject.transform.rotation;

            missileBullets[countBullet - 1].transform.Rotate(new Vector3(0f, 0f, -90f));

            missileBullets[countBullet - 1].SetActive(true);

            float shotRange_ = Random.Range(bulletShotRange * -1, bulletShotRange + 1);

            missileBullets[countBullet - 1].transform.Rotate(new Vector3(0f, 0f, shotRange_));



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
        if (isReload == true || countBullet == weaponMagazine) { return; }

        yariAnimator.SetTrigger("OnReload");

        PlayerManager.Instance.player.weaponReload.ReloadStart(weaponReload);
        StartOnReload();
        //[KJH] ADD
        SoundManager.Instance.Play("YariRocket/yarirocketlauncher_reload_01", Sound.SFX);

    }

    //! 야이리 발사기의 정보 값을 셋팅하는 함수
    public override void SetWeaponData()
    {
        base.SetWeaponData();

        this.weaponName = "야이리 발사기";
        this.weaponDesc = "지옥에 잘 왔습니다";
        this.weaponType = "다연장 로켓런처";
        this.weaponDataTxt = "자동추적 로켓을 연속으로 발사합니다.\n" +
            "질라 인더스트리가 생산한 전설적인 무기로, 지금은 잊혀진 한 강력한 전사의 주문에 맞춰서 만들어졌습니다.";

        this.weaponPos = new Vector3(5f, 2f, 0f);
        this.weaponReload = 1.5f;
        this.weaponMagazine = 20;

        this.weaponBulletValue = 140;
        this.knockBack = 30f;
        this.bulletSpeed = 16f;
        this.bulletDamage = 20;
        this.bulletRange = 90f;
        this.bulletShotRange = 10;
        this.weaponDeley = 0.04f;
        this.weaponHand = 2;

        yariAnimator = gameObject.GetComponentMust<Animator>();
    }
}
