using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedShotgunKinController : MonoBehaviour
{
    private Animator animator = default;

    private float distance = default;
    public bool isRunning = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        AnimationControll();
    }

    private void Move()
    {
        distance = Vector2.Distance(transform.localPosition, GameObject.Find("Player").transform.localPosition);

        if (distance > 350)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, GameObject.Find("Player").transform.localPosition, 0.2f);

            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
    }   //  Move()

    private void AnimationControll()
    {
        if (isRunning == true)
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }   //  AnimationControll()
}
