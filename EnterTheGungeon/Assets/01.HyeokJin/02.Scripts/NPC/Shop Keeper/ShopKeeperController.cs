using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeperController : MonoBehaviour
{
    Animator shopkeeperAnimator = default;

    private bool isInShop = false;
    private bool isPlayerShoot = false;

    private int shootCount = default;

    private void Awake()
    {
        shopkeeperAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        EnterShop();
        ShootCheck();

        Debug.Log(shootCount);
    }

    private void Status()
    {
        if (isInShop)
        {
            switch (shootCount)
            {
                case 1:
                    Warning();
                    break;
                case 2:
                    Threat();
                    break;
                case 3:
                    OpenFire();
                    break;
            }
        }
    }

    private void EnterShop()
    {
        if (true)
        {
            isInShop = true; // 임시 : true
        }
    }

    private void ShootCheck()
    {
        if (Input.GetMouseButtonDown(0))    //  임시 : 마우스 왼클릭(발사) 한 경우
        {
            shootCount++;
        }
    }

    private void Warning()
    {
        //  플레이어가 공격하면(1회) 경고함
    }

    private void Threat()
    {
        //  플레이어가 공격하면(2회) 샷건을 들고 위협함
    }

    private void DoublePrice()
    {
        //  플레이어가 공격(2회)하면 이루부터 상점 내 모든 아이템의 가격이 두 배가 됨
    }

    private void OpenFire()
    {
        //  플레이어가 공격하면 샷건을 난사함
    }

    private void Hide()
    {
        //  샷건을 난사한 후 책상아래로 숨어서 버튼을 누름
    }

    private void KickOut()
    {
        //  상점 주인이 버튼을 누른 이후 플레이어는 강제로 상점 밖으로 내쫒는다 / 이후 물건을 팔지 않음
    }

    private void PlayerDie()
    {
        //  상점 주인에게 죽으면 플레이어가 정의에 의해 죽었다고 표시된다.
    }
}
