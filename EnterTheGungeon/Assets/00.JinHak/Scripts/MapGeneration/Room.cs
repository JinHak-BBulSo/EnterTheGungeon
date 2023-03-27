using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<string> enemy;
    public GameObject boss;
    public Vector2 roomSize = default;
    public bool isPlayerEnter = false;
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
            GameObject boss_ = EnemyManager.Instance.CreateBoss(boss, this.transform);
            boss_.GetComponent<RectTransform>().localScale = new Vector3(0.0139f,0.0139f,0.0139f);

            //test
            boss_.transform.position = new Vector2(0,10);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && collision.gameObject != null)
        {
            isPlayerEnter = true;
            foreach (GameObject enemy in enemies)
            {
                enemy.SetActive(true);
            }
        }
    }
}
