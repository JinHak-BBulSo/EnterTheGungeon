using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonrakerWeapon : PlayerWeapon
{

    [SerializeField]
    private float defDistanceRay = default;

    public Weapons weapons = default;

    public LineRenderer moonLineRenderer = default;
    public Transform moonTransform = default;

    public int countBullet = default;
    public float deleyChkVal = default;

    public int layerMask = default;

    public bool isLaserOn = false;


    // Start is called before the first frame update
    void Start()
    {
        weapons = new MoonrakerWeaponVal();
        SetWeaponData(weapons);

        moonLineRenderer = gameObject.GetComponentMust<LineRenderer>();
        moonTransform = gameObject.GetComponentMust<Transform>();

        moonLineRenderer.enabled = false;
        countBullet = weaponMagazine;

        deleyChkVal = 0f;
        isLaserOn = false;

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
        if(isLaserOn == true)
        {
            ShootLaser();
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



    public void ShootLaser()
    {

        moonLineRenderer.enabled = true;
        

        RaycastHit2D hit_ = Physics2D.Raycast(moonTransform.position, transform.right, defDistanceRay, layerMask);

        if (hit_ != default )
        {
            
            Draw2DRay(firePos.position, hit_.point);

            ReflectLaser(hit_.point, firePos.position);

        }
        else
        {
            Draw2DRay(firePos.position, firePos.transform.right * defDistanceRay);
        }

    }

    //! 레이저 반사되는 함수
    public void ReflectLaser(Vector3 hitPos, Vector3 startPos) 
    {
        // 입사 벡터 값
        float inComingVecter_ = Vector3.SignedAngle(hitPos, startPos, -Vector3.right);

        // 충돌할 면의 벡터 값
        float normalVecter_ = Mathf.Atan2(hitPos.y, hitPos.x) * 180f / Mathf.PI;

        // 입사 벡터를 각도로 변환
        //float inComingAngle = Vector3.SignedAngle()
    }


    public void OffLaser()
    {
        isLaserOn = false;
        moonLineRenderer.enabled = false;
    }


    public void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        moonLineRenderer.SetPosition(0, startPos);
        moonLineRenderer.SetPosition(1, endPos);

    }




}
