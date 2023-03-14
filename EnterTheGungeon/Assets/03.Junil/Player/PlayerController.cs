using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMove playerMove = default;



    // { [Junil] 공포탄 이벤트를 사용하기 위한 델리게이트, 이벤트 선언
    public delegate void PlayerBlankBullets();

    public static event PlayerBlankBullets OnPlayerBlankBullet;

    // } [Junil] 공포탄 이벤트를 사용하기 위한 델리게이트, 이벤트 선언


    /// @param int ChkBlankBullets : 소지하고 있는 공포탄 개수
    public int ChkBlankBullets = default;


    private void Awake()
    {
        playerMove = gameObject.GetComponentMust<PlayerMove>();



        // 초기 시작 시, 공포탄 2개 지급
        ChkBlankBullets = 2;
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        playerMove.OnMove(inputX, inputY);





        /// @brief 공포탄을 사용하는 함수
        if (Input.GetKeyDown(KeyCode.Q))
        {

            if(ChkBlankBullets == 0) { return; }
            OnPlayerBlankBullet();
            ChkBlankBullets--;
        }
    }
}
