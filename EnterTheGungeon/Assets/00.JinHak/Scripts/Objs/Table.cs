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
    BoxCollider2D deskBoxCollider = default;
    private bool isOver = false;
    private bool isOverSet = false;
    public OverDistance distance;

    
    protected override void Start()
    {
        base.Start();
        deskBoxCollider = GetComponent<BoxCollider2D>();
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
        deskBoxCollider.enabled = false;
        isOverSet = true;
        gameObject.AddComponent<PolygonCollider2D>();
        Rigidbody2D rigid_ = gameObject.AddComponent<Rigidbody2D>();
        rigid_.gravityScale = 0;
        rigid_.mass = 200;
        rigid_.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
