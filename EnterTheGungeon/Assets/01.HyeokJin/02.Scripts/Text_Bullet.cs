using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text_Bullet : MonoBehaviour
{
    private float bulletSpeed = default;


    private void Start()
    {
        bulletSpeed = 10f;
    }

    private void Update()
    {
        BulletControll();
    }

    private void BulletControll()
    {
        transform.Rotate(Vector3.forward * bulletSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}
