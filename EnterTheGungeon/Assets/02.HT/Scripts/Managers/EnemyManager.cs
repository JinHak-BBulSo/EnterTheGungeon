using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : GSingleton<EnemyManager>
{
    public bool isTest;
    EnemyData enemyData;
    GameObject enemyPrefab;
    GameObject weapon;

    public List<string> enemyName = new List<string> { "bulletKin", "bandanaBulletKin" };

    public void CreateEnemy(string enemyName)
    {
        enemyData = Resources.Load<EnemyData>($"02.HT/ScriptableObjects/Enemies/{enemyName}");

        enemyPrefab = Resources.Load<GameObject>("02.HT/Prefabs/EnemyPrefab");

        if (enemyData.Weapon.Count > 0)
        {
            int weaponNum = Random.Range(0, enemyData.Weapon.Count);
            weapon = enemyData.Weapon[weaponNum];
        }

        GameObject clone = Instantiate(enemyPrefab, GameObject.Find("TestRoom").transform);
        Instantiate(weapon, clone.transform.GetChild(2));
        clone.name = enemyData.EnemyName;

        // { image 변경 & image size 조절
        clone.transform.GetChild(0).GetComponent<Image>().sprite = enemyData.EnemyImage;
        clone.transform.GetChild(0).GetComponent<Image>().SetNativeSize();
        clone.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(clone.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.x * 3, clone.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.y * 3);
        clone.transform.GetComponent<RectTransform>().sizeDelta = clone.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta;
        clone.transform.GetComponent<CapsuleCollider2D>().size = clone.transform.GetComponent<RectTransform>().sizeDelta;
        // } image 변경 & image size 조절

        clone.transform.GetChild(0).GetComponent<Animator>().runtimeAnimatorController = enemyData.EnemyAnim;

        //attaktype설정
        clone.GetComponent<TestEnemy>().attackType = enemyData.AttackType;

        //attacktype이 0이 아닌경우(!useWeapon)
        if(enemyData.AttackType !=0)
        {
            clone.transform.GetChild(2).gameObject.SetActive(false);
        }

    }
}
