using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public BossRoom belongRoom = default;

    public int currentHp;
    private int damageTaken;

    public virtual void PatternStart()
    {
        /* override using */
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerBullet")
        {
            damageTaken = other.GetComponent<PlayerBullet>().bulletDamage;// = other.GetComponent<PlayerBullet>().damage; after setting playerbullet, change this.
            currentHp -= damageTaken;
            damageTaken = 0;
        }
    }
}
