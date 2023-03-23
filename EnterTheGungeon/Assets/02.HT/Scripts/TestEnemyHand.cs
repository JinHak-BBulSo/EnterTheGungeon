using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyHand : MonoBehaviour
{

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        if (transform.localPosition.x < -30)
        {
            transform.localPosition = new Vector3(-30f, transform.localPosition.y, transform.localPosition.z);
        }
        else if(transform.localPosition.x > 30)
        {
            transform.localPosition = new Vector3(30f, transform.localPosition.y, transform.localPosition.z);
        }
    }
}
