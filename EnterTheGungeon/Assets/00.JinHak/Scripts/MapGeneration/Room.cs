using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<string> enemy;
    public GameObject boss;
    public GameObject mapBoss;
    public Vector2 roomSize = default;
    public bool isPlayerEnter = false;
    public int enemyCount = 0;
    private List<GameObject> enemies = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < enemy.Count; i++)
        {
            GameObject enemy_ = EnemyManager.Instance.CreateEnemy(enemy[i], this.transform);
            enemies.Add(enemy_);
            enemies[i].SetActive(false);
        }
        if (boss != null)
        {
            mapBoss = EnemyManager.Instance.CreateBoss(boss, this.transform);
            mapBoss.SetActive(false);
        }
        enemyCount = enemy.Count;
    }

    void Update()
    {
        if(isPlayerEnter && enemy.Count == 0)
        {
            DoorManager.Instance.AllDoorOpen();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && collision.gameObject != null)
        {
            isPlayerEnter = true;
            DoorManager.Instance.AllDoorClose();

            foreach (GameObject enemy in enemies)
            {
                enemy.SetActive(true);
            }
            if(boss != null)
            {
                mapBoss.SetActive(true);
            }
        }
    }
}
