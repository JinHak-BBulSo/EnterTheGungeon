using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectMove : MonoBehaviour
{

    public Animator playerAnimator = default;

    private void Awake()
    {
        playerAnimator = gameObject.GetComponentMust<Animator>();

        playerAnimator.SetTrigger("SelectIdle");
    }

    public void OnSelectPlayer()
    {
        playerAnimator.SetTrigger("OnSelect");

        gameObject.GetComponentMust<PlayerSelectMove>().enabled = false;
    }
}
