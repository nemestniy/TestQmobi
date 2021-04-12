using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : SingletonMonoBehaviour<EnemyManager>
{
    public List<Enemy> enemies;
    public float spawnDelay;
    public float stepDelayDecreasing;
    public float minDelay;
    public float spawnDistance;

    private Coroutine spawnCoroutine;

    //Generate enemies by coroutine for easy control delay between enemies generation
    private IEnumerator GenerateEnemies(float delay)
    {
        while(true)
        {
            var randAngle = UnityEngine.Random.Range(0, 2 * Mathf.PI);
            var randDirection = new Vector2(Mathf.Sin(randAngle), Mathf.Cos(randAngle));
            var distance = (ObjectsHolder.FrameSize).magnitude + UnityEngine.Random.Range(0, spawnDistance);
            var spawnPosition = randDirection * distance;

            var randIndex = UnityEngine.Random.Range(0, enemies.Count);

            var rotation = Vector3.zero;

            if (enemies[randIndex].RotateRandomly)
                rotation.z = UnityEngine.Random.Range(0.0f, 360.0f);

            Instantiate(enemies[randIndex].Prefab, spawnPosition, Quaternion.Euler(rotation));

            if(spawnDelay > minDelay)
                spawnDelay -= stepDelayDecreasing;

            yield return new WaitForSeconds(delay);
        }
    }

    public void Initialize()
    {
        spawnCoroutine = StartCoroutine(GenerateEnemies(spawnDelay));
    }

    public void StopEnemiesGeneration()
    {
        StopCoroutine(spawnCoroutine);
    }
}

[Serializable]
public struct Enemy
{
    public GameObject Prefab;
    public bool RotateRandomly;
}
