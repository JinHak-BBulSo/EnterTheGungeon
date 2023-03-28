using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : GSingleton<EnemyManager>
{
    public bool isTest;
    public bool isBossTest;
    EnemyData enemyData;
    GameObject enemyPrefab;
    GameObject bossPrefab;
    GameObject weapon;

    public List<string> enemyName = new List<string> { "bulletKin", "bookllet", "gunNut", "redShotgunKin" };


    public List<Sprite> imageEnemyInfo;

    //public List<string> textEnemyExplain1 = new List<string> {"일반적인 적", "건저러 마법 개론", "어둠의 기사", "빨간색 건데드"};
    public List<string> textEnemyExplain1 = new List<string> { "Standard Issue", "Gunjuration 101", "Dark Knight", "Red, Dead." };
    public List<string> textEnemyExplain2 = new List<string> { "", "", "", "" };

    public List<string> textEnemyLongDesc = new List<string> {"총탄 일족은 가장 흔한 건데드입니다.\n지각을 갖춘 이들 탄약은 수백 년 전, 거대한 총탄이 충돌하여 총굴이 생겼을 때 생명을 얻었습니다.\n단순한 괴물이지만, 경계심이 많고 충성스럽습니다.",
    "이 불타는 책은 초보 건저러들이 읽어야 할 필독서입니다.", "건넛은 총탄 킹의 재가를 받아 하급 건데드를 이끌고, 순수한 총탄으로 빚어낸 강력한 검으로 총굴의 여러 회랑을 순찰하는 기사입니다.",
    "빨간 총탄 일족은 산탄총 건데드 중에서 가장 뚱뚱하고 내구성이 높습니다.\n산탄총 일족은 총탄 사회에서 자신들보다 작은 친족의 집행자 역할을 종종 맡아서 수행합니다."};


    public List<string> bossName = new List<string> { "gorGun" };

    //test
    public Dictionary<string, bool> enemyFindCheck = new Dictionary<string, bool>()
    {{"bulletKin", true}, {"bookllet", false}, {"gunNut", false}, {"redShotgunKin", false}};
    /* public Dictionary<string, bool> enemyFindCheck = new Dictionary<string, bool>()
    {{"bulletKin", false}, {"bandanaBulletKin", false}, {"gunNut", false}}; */


    public Dictionary<string, bool> bossFindCheck = new Dictionary<string, bool>()
    {{"gorGun", false}};


    public override void Awake()
    {
        imageEnemyInfo = new List<Sprite> { Resources.Load<Sprite>("02.HT/Sprites/Ammonomicon/EnemyBook/Ammonomicon_Bullet_Kin"),
    Resources.Load<Sprite>("02.HT/Sprites/Ammonomicon/EnemyBook/Ammonomicon_Bookllet"), Resources.Load<Sprite>("02.HT/Sprites/Ammonomicon/EnemyBook/Ammonomicon_Gun_Nut"),
    Resources.Load<Sprite>("02.HT/Sprites/Ammonomicon/EnemyBook/Ammonomicon_Red_Shotgun_Kin")};
    }

    public GameObject CreateEnemy(string enemyName, Transform transform_)
    {
        enemyData = Resources.Load<EnemyData>($"02.HT/ScriptableObjects/Enemies/{enemyName}");

        enemyPrefab = Resources.Load<GameObject>("02.HT/Prefabs/EnemyPrefab");
        //enemyPrefab = Resources.Load<GameObject>("02.HT/Prefabs/EnemyPrefabRenderVersion");

        GameObject clone = Instantiate(enemyPrefab, transform_);
        if (enemyData.Weapon.Count > 0)
        {
            int weaponNum = Random.Range(0, enemyData.Weapon.Count);
            weapon = enemyData.Weapon[weaponNum];
            Instantiate(weapon, clone.transform.GetChild(2));
        }
        else { }

        clone.name = enemyData.EnemyName;
        clone.GetComponent<TestEnemy>().enemyName = enemyData.EnemyName;
        clone.GetComponent<TestEnemy>().dropCoinCount = enemyData.DropCoinCount;


        // { image 변경 & image size 조절
        //clone.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = enemyData.EnemyImage;
        clone.transform.GetChild(0).GetComponent<Image>().sprite = enemyData.EnemyImage;
        clone.transform.GetChild(0).GetComponent<Image>().SetNativeSize();
        clone.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(clone.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.x * 3, clone.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.y * 3);
        clone.transform.GetComponent<RectTransform>().sizeDelta = clone.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta;
        clone.transform.GetComponent<CapsuleCollider2D>().size = clone.transform.GetComponent<RectTransform>().sizeDelta;
        clone.transform.GetComponent<RectTransform>().localScale = new Vector3(0.0139f, 0.0139f, 0.0139f);
        // } image 변경 & image size 조절

        clone.transform.GetChild(0).GetComponent<Animator>().runtimeAnimatorController = enemyData.EnemyAnim;
        //clone.transform.GetChild(0).GetChild(0).GetComponent<Animator>().runtimeAnimatorController = enemyData.EnemyAnim;

        //attaktype설정
        clone.GetComponent<TestEnemy>().attackType = enemyData.AttackType;

        //attacktype이 0이 아닌경우(!useWeapon)
        if (enemyData.AttackType != 0)
        {
            clone.transform.GetChild(2).gameObject.SetActive(false);
        }

        //Status 설정
        clone.GetComponent<TestEnemy>().maxHp = enemyData.EnemyHp;
        clone.GetComponent<TestEnemy>().moveSpeed = enemyData.EnemyMoveSpeed;

        return clone;
    }

    public GameObject CreateBoss(GameObject bossPrefab_, Transform transform_)
    {
        GameObject clone = Instantiate(bossPrefab_, transform_);
        return clone;
    }

    public void EnemyFindCheck(string enemyName_)
    {
        if (!enemyFindCheck[enemyName_])
        {
            enemyFindCheck[enemyName_] = true;
        }
        else
        {

        }
    }
}
