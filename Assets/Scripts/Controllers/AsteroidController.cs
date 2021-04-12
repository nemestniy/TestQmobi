using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class AsteroidController : MonoBehaviour
{
    public AsteroidConfig config;

    private List<AsteroidController> crushingAsteroids;
    private float biasDirection;
    private float power;
    private float dyingZone;
    private int crushingWeight;

    private new Rigidbody2D rigidbody;
    private float asteroidSize;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        asteroidSize = GetComponent<Collider2D>().bounds.extents.magnitude;
    }

    private void Start()
    {
        if (config != null)
            Initialize(config);
        else
            Debug.LogError("Config of asteroid is missing.");

        //Check if asteroid outside of frame move it in player direction
        if (transform.position.magnitude > ObjectsHolder.FrameSize.magnitude)
            MoveToPlayer();
    }

    private void Initialize(AsteroidConfig config)
    {
        crushingAsteroids = config.CrushingAsteroids;
        biasDirection = config.BiasDirection;
        power = config.Power;
        dyingZone = config.DyingZone;
        crushingWeight = config.CrushingWeight;
    }

    public void MoveToPlayer()
    {
        var player = ObjectsHolder.Player;
        if (player == null) return;

        var directionPosition = (Vector2)transform.position +
                                 Vector2.up * Random.Range(-biasDirection, biasDirection) +
                                 Vector2.right * Random.Range(-biasDirection, biasDirection);

        var direction = ((Vector2)player.transform.position - directionPosition).normalized;

        rigidbody.AddForce(direction * power);
    }

    private void CheckDyingZone()
    {
        var distance = ((Vector2)transform.position).magnitude - ObjectsHolder.FrameSize.magnitude;
        if (distance > dyingZone)
        {
            Destroy(gameObject);
        }
    }

    //After being hit by player asteroid will be destroyed and Instantiate new smaller asteroids
    //CrushWeight say how much we can Instantiate new different smaller asteroids on place of previous asteroid
    public void CrushAsteroid(Vector2 direction)
    {
        if (crushingAsteroids.Count < 1)
        {
            Destroy(gameObject);
            return;
        }

        float angleDirection = Mathf.Acos(direction.y / direction.magnitude);

        while (crushingWeight > 0)
        {
            float randAngleDirection = Random.Range(0.0f, Mathf.PI) + angleDirection;
            float randDistance = Random.Range(0.0f, asteroidSize);
            var randAsteroid = crushingAsteroids[Random.Range(0, crushingAsteroids.Count)];

            var randDirection = new Vector2(Mathf.Cos(randAngleDirection), Mathf.Sin(randAngleDirection)) * randDistance;

            crushingWeight -= randAsteroid.config.CrushingWeight;
            var asteroid = Instantiate(randAsteroid,
                                       randDirection + (Vector2)transform.position,
                                       Quaternion.Euler(0, 0, Random.Range(0.0f, 360.0f)));

            if(asteroid.TryGetComponent<Rigidbody2D>(out var rigidbody))
            {
                rigidbody.AddForce(randDirection.normalized * asteroid.config.Power + direction);
            }
        }

        Destroy(gameObject);
    }

    private void Update()
    {
        CheckDyingZone();
    }
}
