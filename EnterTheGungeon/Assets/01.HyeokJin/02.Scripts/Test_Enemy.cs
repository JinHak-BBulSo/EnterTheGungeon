using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Enemy : MonoBehaviour
{
    public string enemyName;
    public int enemyScore;
    public float speed;
    public int health;
    public Sprite[] sprites;

    public float maxShotDelay;
    public float curShotDelay;

    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = Vector2.down * speed;
    }

    private void Update()
    {


    }

    void OnHit(int dmg_)
    {
        health -= dmg_;

        spriteRenderer.sprite = sprites[1];
        Invoke("ReturnSprite", 0.1f);

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void ReturnSprite()
    {
        spriteRenderer.sprite = sprites[0];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(""))
        {
            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;
        }
        else if (collision.CompareTag("Bullet"))
        {
            Test_BulletController bullet = collision.gameObject.GetComponent<Test_BulletController>();
            OnHit(bullet.dmg);

            collision.gameObject.SetActive(false);
        }
    }
}
