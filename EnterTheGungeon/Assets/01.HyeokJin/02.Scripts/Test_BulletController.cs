using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_BulletController : MonoBehaviour
{
    private Rigidbody2D bulletRigidbody = default;

    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
