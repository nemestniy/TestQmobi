using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DefaultBullet : BulletBase
{
    private new Rigidbody2D rigidbody;
    private GunBase gunParent;

    private void OnEnable()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    //Gun add force to bullet after shooting
    public override void AddPower(float power)
    {
        rigidbody.AddForce(transform.up * power);
    }

    //Checking object of collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.TryGetComponent<AsteroidController>(out var asteroid))
        {
            asteroid.CrushAsteroid(rigidbody.velocity);
            gunParent.OnHitting(asteroid.config.Reward);
        }

        if(collision.transform.TryGetComponent<UFOController>(out var ufo))
        {
            Destroy(ufo.gameObject);
            gunParent.OnHitting(ufo.config.Reward);
        }

        if(collision.transform.TryGetComponent<Player>(out var player))
        {
            GameManager.Instance.KillPlayer();
        }

        if (collision.transform.tag != transform.tag)
            Destroy(gameObject);
    }

    //It`s needed for calling event to put points from killed enemy
    public override void AssignGunParent(GunBase gun)
    {
        gunParent = gun;
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), gunParent.transform.parent.GetComponent<Collider2D>());
    }
}
