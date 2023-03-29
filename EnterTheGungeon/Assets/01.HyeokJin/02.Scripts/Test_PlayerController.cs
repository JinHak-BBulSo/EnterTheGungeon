using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_PlayerController : MonoBehaviour
{
    private Rigidbody2D rb = default;

    private float playerSpeed = default;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        playerSpeed = 5f;
    }

    private void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(inputX * playerSpeed, inputY * playerSpeed);
    }

}
