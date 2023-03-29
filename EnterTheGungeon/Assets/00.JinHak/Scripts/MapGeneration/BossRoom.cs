using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : Room
{
    GameObject bossRoomEntrance = default;
    public GameObject[] roomEntrances = new GameObject[4];
    public bool isBossKill = false;
    public int bossCount = 1;

    private void Awake()
    {
        bossRoomEntrance = gameObject.transform.GetChild(3).gameObject;
        for (int i = 0; i < bossRoomEntrance.transform.childCount; i++)
        {
            roomEntrances[i] = bossRoomEntrance.transform.GetChild(i).gameObject;
            roomEntrances[i].SetActive(false);
        }
    }
    public override void Start()
    {
        base.Start();
        mapBoss.GetComponent<Boss>().belongRoom = this;      
    }

    public override void Update()
    {
        if (isPlayerEnter && enemyCount == 0 && !isRoomClear)
        {
            isRoomClear = true;
            DoorManager.Instance.AllDoorOpen();
        }
    }
}
