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
    BoxCollider2D tableBoxCollider = default;
    Rigidbody2D tableRigid = default;
    private bool isOver = false;
    private bool isRigidSet = false;
    public OverDistance distance;

    
    protected override void Start()
    {
        base.Start();
        tableBoxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isOver)
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

    IEnumerator ReSetCollider()
    {
        yield return new WaitForSeconds(0.3f);
        tableBoxCollider.enabled = false;
        isRigidSet = true;
        gameObject.AddComponent<PolygonCollider2D>();
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

    }
}
