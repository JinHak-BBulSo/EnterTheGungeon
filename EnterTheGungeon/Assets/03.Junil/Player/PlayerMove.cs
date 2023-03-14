using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    private Rigidbody2D playerRigid2D = default;
    private Animator playerAni = default;


    public float playerSpeed = default;


    private void Awake()
    {
        playerRigid2D = gameObject.GetComponentMust<Rigidbody2D>();
        playerAni = gameObject.GetComponentMust<Animator>();


        playerSpeed = 5f;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMove(float inputX, float inputY)
    {
        playerRigid2D.velocity = new Vector2(inputX * playerSpeed, inputY * playerSpeed);

        if (inputX == 0 && inputY == 0)
        {
            playerAni.SetBool("isRun", false);

        }
        else
        {
            playerAni.SetBool("isRun", true);

        }

    }
}
