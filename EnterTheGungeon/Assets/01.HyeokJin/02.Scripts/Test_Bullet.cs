using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class Test_Bullet : MonsterBullets
{
    [SerializeField] public bool isRotate = default;
    [SerializeField] public bool isBounce = default;

    private ShopKeeperController shopkeeperController = default;
    private ObjectManager objectManager = default;

    private int bulletCount = default;

    private float bulletSpeed = default;
    private float maxPatternCount = default;
    private float curPatternCount = default;

    private void Awake()
    {
        shopkeeperController = GetComponent<ShopKeeperController>();
        objectManager = GameObject.Find("ObjectManager").GetComponent<ObjectManager>();
    }

    private void Update()
    {
        if (isRotate)
        {
            transform.Rotate(Vector3.forward * 3f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall") || collision.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }

        if (isBounce && collision.CompareTag("Wall"))
        {
            gameObject.SetActive(false);

            bulletSpeed = 8f;
            bulletCount = 32;

            for (int i = 0; i < bulletCount; i++)
            {
                GameObject bullet = objectManager.MakeObject("Bullet_Basic");
                bullet.transform.position = transform.position;
                bullet.transform.rotation = Quaternion.identity;

                Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
                Vector2 direction = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / bulletCount), Mathf.Sin(Mathf.PI * 2 * i / bulletCount));
                bulletRigidbody.AddForce(direction.normalized * bulletSpeed, ForceMode2D.Impulse);
            }
        }
    }
}
