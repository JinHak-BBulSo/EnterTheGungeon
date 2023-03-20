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

       

    public Weapons weapons = default;

    public LineRenderer moonLineRenderer = default;
    public Transform moonTransform = default;

    public int countBullet = default;
    public float deleyChkVal = default;

    public int layerMask = default;

    // 반사되는 최대 횟수
    public int reflectMax = default;


    public bool isLoopActive = true;
    public bool isLaserOn = false;
    public bool isChkMagazine = false;


    // Start is called before the first frame update
    void Start()
    {
        weapons = new MoonrakerWeaponVal();
        SetWeaponData(weapons);


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
        gameObject.transform.localPosition = weapons.WeaponPos();


        layerMask = (1 << LayerMask.NameToLayer("Player"))
            | (1 << LayerMask.NameToLayer("Bullet"));
        layerMask = ~layerMask;
        
    }

    // Update is called once per frame
    void Update()
    {
        GFunc.Log($"{weaponMagazine}");

        if (isLaserOn == true)
        {
            if(isChkMagazine == false)
            {
                StartCoroutine("MinusWeaponMagazine");
                isChkMagazine = true;
            }

            TestLaser();

            
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            OffLaser();
        }
    }

    public override void FireBullet()
    {
        isLaserOn = true;

    }

    public override void ReloadBullet()
    {
        base.ReloadBullet();
    }

    public void TestLaser()
    {
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
                Vector3 temp_ = firstPos;

                countLaser_++;
                moonLineRenderer.positionCount = countLaser_;
                directLaser = Vector3.Reflect(directLaser, hit_.normal);
                firstPos = (Vector2) directLaser.normalized + hit_.point;
                moonLineRenderer.SetPosition(countLaser_ - 1, hit_.point);
                

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
        StopCoroutine("MinusWeaponMagazine");
        isLaserOn = false;
        isChkMagazine = false;
        moonLineRenderer.enabled = false;
    }



    IEnumerator MinusWeaponMagazine()
    {
        while (isLaserOn == true)
        {
            yield return new WaitForSeconds(weaponDeley);

            weaponMagazine--;
        }

    }


}
