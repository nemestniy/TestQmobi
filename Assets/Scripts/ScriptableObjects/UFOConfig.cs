using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UFOConfig", menuName = "Configs/UFOConfig")]
public class UFOConfig : EnemyScriptableObject
{
    public float Speed;
    public GunBase Gun;
    public float MinAttackDistance;
}
