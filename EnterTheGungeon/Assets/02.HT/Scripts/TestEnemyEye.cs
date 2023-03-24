using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyEye : MonoBehaviour
{
    float angle;
    GameObject player;
    public RaycastHit2D hit = default;

    float distanceToPlayer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        // { Look At Player Position
        angle = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x)
        * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        // } Look At Player Position

        // { Raycast To Player
        Debug.DrawRay(transform.position, transform.up * 10, Color.red);
        int layerMask = 1 << LayerMask.NameToLayer("Ignore Raycast");
        layerMask = ~layerMask;
        //hit = Physics2D.Raycast(transform.position, transform.up, distanceToPlayer+10, layerMask);
        hit = Physics2D.Raycast(transform.position, transform.up, distanceToPlayer + 10, layerMask);
        // } Raycast To Player
    }
}