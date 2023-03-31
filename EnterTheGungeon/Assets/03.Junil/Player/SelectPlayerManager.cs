using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPlayerManager : MonoBehaviour
{

    public GameObject playerRotateObjProfab = default;
    int playerCharacterIndex = -1;
    public GameObject[] playerObjs = default;
    private bool isSelected = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRotateObjProfab = Resources.Load<GameObject>("03.Junil/Prefabs/Player/RotateObjs");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isSelected)
        {
            Vector2 clickPos_ = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit_ = Physics2D.Raycast(clickPos_, Vector2.zero);



            if (hit_.collider != null)
            {
                if (hit_.collider.name == "PlayerMarine")
                {
                    GFunc.Log("마린이 선택");
                    playerCharacterIndex = 0;
                    StartCoroutine(SelectPlayerAni(hit_));
                }

                if (hit_.collider.name == "PlayerGuide")
                {
                    GFunc.Log("가이드가 선택");
                    playerCharacterIndex = 1;
                    StartCoroutine(SelectPlayerAni(hit_));
                }
                isSelected = true;
            }
        }
    }   // Update()

    IEnumerator SelectPlayerAni(RaycastHit2D hitPlayer_)
    {
        MoveCamera2D.target = hitPlayer_.collider.gameObject;
        hitPlayer_.collider.gameObject.GetComponent<PlayerSelectMove>().OnSelectPlayer();

        yield return new WaitForSeconds(1f);


        Instantiate(playerRotateObjProfab,
            hitPlayer_.collider.gameObject.transform.position,
            Quaternion.identity,
            hitPlayer_.collider.gameObject.transform).transform.SetAsFirstSibling();

        hitPlayer_.collider.gameObject.AddComponent<PlayerController>();
    }

    public void DestroyNotSelectPlayer()
    {
        if (playerCharacterIndex == 0)
        {
            Destroy(playerObjs[1]);
        }
        else
        {
            Destroy(playerObjs[0]);
        }
    }
}
