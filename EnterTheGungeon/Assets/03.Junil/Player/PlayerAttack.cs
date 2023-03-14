using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private GameObject playerObj = default;
    private GameObject rotateObjs = default;
    private Animator playerAni = default;

    // Start is called before the first frame update
    void Start()
    {
        rotateObjs = gameObject.transform.parent.gameObject;
        playerObj = rotateObjs.transform.parent.gameObject;
        playerAni = rotateObjs.transform.parent.gameObject.GetComponentMust<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // { [Junil] 무기가 마우스 커서를 바라보는 코드
        /// @param Vector3 mousePos_ : 마우스 커서 위치 값
        Vector3 mousePos_ = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        /// @param Vector2 len_ : 마우스 커서 위치와 이 오브젝트의 위치를 뺀 값
        Vector2 len_ = mousePos_ - transform.position;

        float lookZ_ = Mathf.Atan2(len_.y, len_.x);

        float rotateZ_ = lookZ_ * Mathf.Rad2Deg;

        lookZ_ *= 100;

        
        transform.rotation = Quaternion.Euler(0, 0, rotateZ_);

        // } [Junil] 무기가 마우스 커서를 바라보는 코드

        /// @param Vector2 rotateStand_ : 제대로 된 x 축 반전을 구하기 위한 값
        Vector2 rotateStand_ = mousePos_ - rotateObjs.transform.position;


        if(rotateStand_.x > 0)
        {
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);

            playerObj.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(-1f, -1f, 1f);

            playerObj.transform.localScale = new Vector3(-1f, 1f, 1f);
        }


        playerAni.SetFloat("inputX", mousePos_.x);
        playerAni.SetFloat("inputY", mousePos_.y);




    }
}