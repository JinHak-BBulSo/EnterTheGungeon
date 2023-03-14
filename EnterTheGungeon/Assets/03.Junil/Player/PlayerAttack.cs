using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private GameObject playerObj = default;
    private Animator playerAni = default;

    // Start is called before the first frame update
    void Start()
    {
        playerObj = gameObject.transform.parent.gameObject;
        playerAni = gameObject.transform.parent.gameObject.GetComponentMust<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /// @param Vector3 mousePos_ : 마우스 커서 위치 값
        Vector3 mousePos_ = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        /// @param Vector2 len_ : 마우스 커서 위치와 이 오브젝트의 위치를 뺀 값
        Vector2 len_ = mousePos_ - transform.position;

        float lookZ_ = Mathf.Atan2(len_.y, len_.x);

        float rotateZ_ = lookZ_ * Mathf.Rad2Deg;

        lookZ_ *= 100;

        
        transform.rotation = Quaternion.Euler(0, 0, rotateZ_);

        GFunc.Log($"{lookZ_}");
        if (-130f < lookZ_ && lookZ_ < 130f)
        {
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);

            playerObj.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if(-200f < lookZ_ && lookZ_ < -130f || 130f < lookZ_ && lookZ_ < 200f)
        {
            /* Do Nothing */
        }
        else 
        {

            gameObject.transform.localScale = new Vector3(-1f, -1f, 1f);

            playerObj.transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        playerAni.SetFloat("inputX", mousePos_.x);
        playerAni.SetFloat("inputY", mousePos_.y);




        // 캐릭터 애니메이션 작동
        if (-180f < lookZ_ && lookZ_ <= -120f)
        {
            // 아래 보기
        }
        else if (-120f < lookZ_ && lookZ_ <= 40f)
        {
            // 오른쪽 보기
        }
        else if (40f < lookZ_ && lookZ_ <= 120f)
        {
            // 위 오른쪽 보기
        }
        else if (120f < lookZ_ && lookZ_ <= 180f)
        {
            // 위 보기
        }
        else if (180f < lookZ_ && lookZ_ <= 280f)
        {
            // 위 왼쪽 보기
        }
        else if (280f < lookZ_ || lookZ_ <= -180f )
        {
            // 왼쪽 보기
        }


    }
}
