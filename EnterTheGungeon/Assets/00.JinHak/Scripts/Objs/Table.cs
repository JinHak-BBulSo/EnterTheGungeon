using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OverDistance
{
    NONE = -1,
    UP,
    DOWN,
    LEFT,
    RIGHT
}

public class Table : InteractiveObj
{
    private BoxCollider2D tableBoxCollider = default;
    private SpriteRenderer tableRenderer = default;
    private Rigidbody2D tableRigid = default;
    private bool isRigidSet = false;
    private int tableHp = 10;

    public bool isOver = false;
    public OverDistance distance;

    public Sprite[] brokenSprite = default;
    
    protected override void Start()
    {
        base.Start();
        tableBoxCollider = GetComponent<BoxCollider2D>();
        tableRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isOver && distance != OverDistance.NONE)
        {
            Debug.Log(distance);
            switch (distance)
            {
                case OverDistance.UP:
                    objAni.SetTrigger("isBottomUp");
                    break;
                case OverDistance.DOWN:
                    objAni.SetTrigger("isTopUp");
                    break;
                case OverDistance.RIGHT:
                    objAni.SetTrigger("isLeftUp");
                    break;
                case OverDistance.LEFT:
                    objAni.SetTrigger("isRightUp");
                    break;
            }
            
            isOver = true;
            StartCoroutine(ReSetCollider());
        }
    }

    IEnumerator ReSetCollider()
    {
        yield return new WaitForSeconds(0.3f);
        objAni.enabled = false;
        tableBoxCollider.enabled = false;
        isRigidSet = true;
        gameObject.AddComponent<PolygonCollider2D>();
        gameObject.tag = "Wall";
        tableRigid = gameObject.AddComponent<Rigidbody2D>();
        tableRigid.gravityScale = 0;
        tableRigid.mass = 200;
        tableRigid.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (isRigidSet)
            {
                tableRigid.velocity = Vector3.zero;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.tag == "PlayerBullet" ||  collision.tag == "MonsterBullet") && gameObject.tag == "Wall")
        {
            tableHp--;
            if(tableHp > 6)
            {
                tableRenderer.sprite = brokenSprite[(int)distance * 3 + 0];
            }
            else if(tableHp > 3 && tableHp <= 6)
            {
                tableRenderer.sprite = brokenSprite[(int)distance * 3 + 1];
            }
            else
            {
                gameObject.GetComponent<PolygonCollider2D>().enabled = false;
                tableRenderer.sprite = brokenSprite[(int)distance * 3 + 2];
            }
        }
    }
}
