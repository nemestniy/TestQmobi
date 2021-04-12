using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultGun : GunBase
{
    public float Delay;
    public float Power;
    public BulletBase Bullet;
    public Transform Barrel;

    private bool isCanShoot;
    private float timer;

    public override void Shoot()
    {
        if (isCanShoot)
        {
            var bullet = Instantiate(Bullet, Barrel);
            bullet.transform.parent = null;
            bullet.AddPower(Power);
            bullet.AssignGunParent(this);

            timer = Delay;
            isCanShoot = false;
        }
    }

    void Start()
    {
        isCanShoot = true;
        timer = 0;
    }

    void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;

        if(!isCanShoot && timer <= 0)
        {
            isCanShoot = true;
        }
    }
}
