using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Data", menuName = "Scriptable Object/Enemy Data", order = int.MaxValue - 1)]
public class EnemyData : ScriptableObject
{
    [SerializeField]
    private string enemyName;
    public string EnemyName { get { return enemyName; } }

    [SerializeField]
    // 0: useWeapon, 1: summonBullet, 2: tackle       useWeapon: have hand & weapon// else: have not
    private int attackType;
    public int AttackType { get { return attackType; } }

    [SerializeField]
    private List<GameObject> weapon;
    public List<GameObject> Weapon { get { return weapon; } }
    //

    [SerializeField]
    private Sprite enemyImage;
    public Sprite EnemyImage { get { return enemyImage; } }

    [SerializeField]
    private RuntimeAnimatorController enemyAnim;
    public RuntimeAnimatorController EnemyAnim { get { return enemyAnim; } }

    [SerializeField]
    private int enemyHp;
    public int EnemyHp { get { return enemyHp; } }

    [SerializeField]
    private float enemyMoveSpeed;
    public float EnemyMoveSpeed { get { return enemyMoveSpeed; } }



}
