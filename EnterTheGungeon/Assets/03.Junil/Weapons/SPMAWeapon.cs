using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPMAWeapon : PlayerWeapon
{
    private GameObject spmaBulletPrefab = default;

    private GameObject[] spmaBullets = default;



    public Weapons weapons = default;

    public int countBullet = default;


    // Start is called before the first frame update
    void Start()
    {

        spmaBulletPrefab = Resources.Load<GameObject>("03.Junil/Prefabs/SPMA_Bullet");

        weapons = new MarineNorWeapon();

        SetWeaponData(weapons);

        countBullet = weaponMagazine;


        spmaBullets = new GameObject[weaponMagazine];

        Vector3 saveBulletPos_ = new Vector3(0f, -2000f, 0f);

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
        
    }
}
