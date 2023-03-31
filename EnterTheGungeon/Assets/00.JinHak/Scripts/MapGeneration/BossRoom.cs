using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : Room
{
    GameObject bossRoomEntrance = default;
    public GameObject[] roomEntrances = new GameObject[4];
    public int bossCount = 1;
    public float introDelay = 0;
    private GameObject cutInObjs = default;
    public int bossIndex = -1;

    private void Awake()
    {
        bossRoomEntrance = gameObject.transform.GetChild(3).gameObject;
        cutInObjs = GFunc.GetRootObj("CutInObjs");
        for (int i = 0; i < bossRoomEntrance.transform.childCount; i++)
        {
            roomEntrances[i] = bossRoomEntrance.transform.GetChild(i).gameObject;
            roomEntrances[i].SetActive(false);
        }
    }
    public override void Start()
    {
        base.Start();
        if (boss.name == "BossGorgun")
        {
            mapBoss.GetComponent<Boss>().belongRoom = this;
        }
        else
        {
            Debug.Log(mapBoss.name);
            mapBoss.transform.GetChild(0).GetComponent<Boss>().belongRoom = this;
        }
    }

    public override void Update()
    {
        if (isPlayerEnter && bossCount == 0 && !isRoomClear)
        {
            isRoomClear = true;
            DoorManager.Instance.AllDoorOpen();
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if(collision.tag == "Player" && !isRoomClear && !PlayerManager.Instance.nowPlayerInRoom.isPlayerEnter)
        {
            PlayerManager.Instance.playerCamera.isBossIntro = true;
            MoveCamera2D.target = this.gameObject;
            StartCoroutine(BossIntroEnd(introDelay));
        }
    }

    IEnumerator BossIntroEnd(float introDelay_)
    {
        yield return new WaitForSeconds(1.0f);
        cutInObjs.transform.GetChild(bossIndex).gameObject.SetActive(true);
        yield return new WaitForSeconds(introDelay_);
        cutInObjs.transform.GetChild(bossIndex).gameObject.SetActive(false);
        PlayerManager.Instance.playerCamera.isBossIntro = false;
        MoveCamera2D.target = PlayerManager.Instance.player.gameObject;
    }
}
