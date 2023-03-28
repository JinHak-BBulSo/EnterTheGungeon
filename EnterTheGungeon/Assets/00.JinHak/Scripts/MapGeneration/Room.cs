using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<string> enemy;
    public GameObject boss;
    public GameObject mapBoss;
    private List<GameObject> enemies = new List<GameObject>();
    public int enemyCount = 0;

    public Vector2 roomSize = default;
    public GameObject[] spawnPoints = default;
    public GameObject monsterobjs = default;

    public bool isPlayerEnter = false;
    public bool isRoomClear = false;
    public bool isSpecialRoom = false;

    void Start()
    {
        monsterobjs = GameObject.Find("MonsterObjs");
        for (int i = 0; i < enemy.Count; i++)
        {
            GameObject enemy_ = EnemyManager.Instance.CreateEnemy(enemy[i], this.transform);
            enemies.Add(enemy_);
            enemy_.transform.position = spawnPoints[i].transform.position;
            enemy_.GetComponent<TestEnemy>().belongRoom = this;
            enemies[i].SetActive(false);
            enemy_.transform.parent = monsterobjs.transform;
            enemyCount++;
        }
        if (boss != null)
        {
            mapBoss = EnemyManager.Instance.CreateBoss(boss, this.transform);
            mapBoss.transform.position = spawnPoints[0].transform.position;
            mapBoss.SetActive(false);
        }
    }

    void Update()
    {
        if(isPlayerEnter && enemyCount == 0 && !isRoomClear)
        {
            isRoomClear = true;
            DoorManager.Instance.AllDoorOpen();
        }  
    }

    private void LateUpdate()
    {
        if (isPlayerEnter && isRoomClear)
        {
            DoorManager.Instance.AllDoorOpen();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && collision.gameObject != null)
        {
            isPlayerEnter = true;
            if (!isRoomClear)
            {
                DoorManager.Instance.AllDoorClose();

                foreach (GameObject enemy in enemies)
                {
                    enemy.SetActive(true);
                }
                if (boss != null)
                {
                    mapBoss.SetActive(true);
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isPlayerEnter = false;
        }
    }
}
