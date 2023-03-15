using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPMAWeapon : PlayerWeapon
{
    private GameObject spmaBulletPrefab = default;

    private GameObject[] spmaBullets = default;

    private Transform firePos = default;

    private GameObject rotateWeapon = default;

    public Weapons weapons = default;

    public int countBullet = default;
    public float deleyChkVal = default;

    public bool isAttack = false;
    public bool isReload = false;


    // Start is called before the first frame update
    void Start()
    {

        spmaBulletPrefab = Resources.Load<GameObject>("03.Junil/Prefabs/SPMA_Bullet");

        firePos = gameObject.FindChildObj("FirePos").transform;
        isAttack = false;
        isReload = false;
        deleyChkVal = 0f;

        GameObject weapons_ = gameObject.transform.parent.gameObject;
        rotateWeapon = weapons_.transform.parent.gameObject;

        weapons = new MarineNorWeapon();


        SetWeaponData(weapons);

        countBullet = weaponMagazine;

        gameObject.transform.localPosition = weapons.WeaponPos();

        spmaBullets = new GameObject[weaponMagazine];

        Vector3 saveBulletPos_ = new Vector3(0f, 0f, 0f);

        for(int i = 0; i < spmaBullets.Length; i++)
        {
            spmaBullets[i] = Instantiate(spmaBulletPrefab, saveBulletPos_,
                Quaternion.identity, bulletObjs.transform);

            spmaBullets[i].SetActive(false);
        }


    }

    // Update is called once per frame
    void Update()
    {
        deleyChkVal += Time.deltaTime;
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

            spmaBullets[countBullet - 1].transform.position = firePos.position;

            spmaBullets[countBullet - 1].transform.rotation = gameObject.transform.rotation;

            spmaBullets[countBullet - 1].transform.Rotate(new Vector3(0f, 0f, -90f));
            spmaBullets[countBullet - 1].SetActive(true);

            float shotRange_ = Random.Range(bulletShotRange * -1, bulletShotRange + 1);

            spmaBullets[countBullet - 1].transform.Rotate(new Vector3(0f, 0f, shotRange_));



            countBullet--;

            if(countBullet == 0)
            {
                StartCoroutine(OnReload());
            }
        }
    }

    public override void ReloadBullet()
    {
        if (isReload == true) { return; }

        StartCoroutine(OnReload());

    }

    IEnumerator OnReload()
    {
        GFunc.Log("리로드 중");
        isReload = true;

        yield return new WaitForSeconds(weaponReload);
        countBullet = weaponMagazine;

        isReload = false;
    }

    
}
