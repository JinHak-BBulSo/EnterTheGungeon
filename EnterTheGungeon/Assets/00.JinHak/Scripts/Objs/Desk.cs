using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desk : InteractiveObj
{
    BoxCollider2D deskBoxCollider = default;
    private bool isAttached = false;
    private bool isOver = false;
    private bool isOverSet = false;
    private OverDistance distance;

    public enum OverDistance
    {
        NONE = -1,
        UP,
        DOWN,
        LEFT,
        RIGHT
    }
    protected override void Start()
    {
        base.Start();
        deskBoxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (isAttached && Input.GetKeyDown(KeyCode.E) && !isOver)
        {
            Debug.Log(distance);
            switch (distance)
            {
                case OverDistance.UP:
                    objAni.SetTrigger("isTopUp");
                    break;
                case OverDistance.DOWN:
                    objAni.SetTrigger("isBottomUp");
                    break;
                case OverDistance.LEFT:
                    objAni.SetTrigger("isLeftUp");
                    break;
                case OverDistance.RIGHT:
                    objAni.SetTrigger("isRightUp");
                    break;
            }
            
            isOver = true;
            StartCoroutine(ReSetCollider());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("접촉");
            isAttached = true;

            if (collision.contacts[0].point.y > 0.7f)
            {
                distance = OverDistance.DOWN;
            }
            else if (collision.contacts[0].point.y < -0.7f)
            {
                distance = OverDistance.UP;
            }
            else if (collision.contacts[0].point.x > 0.9f)
            {
                distance = OverDistance.RIGHT;
            }
            else if (collision.contacts[0].point.x < -0.9f)
            {  
                distance = OverDistance.LEFT;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isAttached = false;
            distance = OverDistance.NONE;

            if(isOverSet)
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    IEnumerator ReSetCollider()
    {
        yield return new WaitForSeconds(0.3f);
        deskBoxCollider.enabled = false;
        isOverSet = true;
        gameObject.AddComponent<PolygonCollider2D>();
        Rigidbody2D rigid_ = gameObject.AddComponent<Rigidbody2D>();
        rigid_.gravityScale = 0;
        rigid_.mass = 200;
        rigid_.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
