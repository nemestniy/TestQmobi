using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AsteroidConfig", menuName = "Configs/AsteroidConfig")]
public class AsteroidConfig : EnemyScriptableObject
{
    public List<AsteroidController> CrushingAsteroids;
    public float BiasDirection;
    public float Power;
    public float DyingZone;
    public int CrushingWeight;
}
