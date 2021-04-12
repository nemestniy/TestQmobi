using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOController : MonoBehaviour
{
    public UFOConfig config;

    private float speed;
    private GunBase gun;
    private float minAttackDistance;

    private GameObject target;

    private void Start()
    {
        InitializeVars();

        var player = ObjectsHolder.Player;

        if(player != null)
            target = ObjectsHolder.Player.gameObject;

        gun = Instantiate(gun, transform);
        gun.transform.tag = transform.tag;
    }

    //Initialization of variables from configuration file
    private void InitializeVars()
    {
        speed = config.Speed;
        gun = config.Gun;
        minAttackDistance = config.MinAttackDistance;
    }

    //Move to Player by using only transform 
    private void Update()
    {
        if (target != null)
        {
            Vector2 difVector = target.transform.position - transform.position;
            if (difVector.magnitude > minAttackDistance)
                transform.Translate(difVector.normalized * speed * Time.deltaTime);
            else if(gun != null)
                gun.Shoot();

            if (gun != null)
                gun.transform.up = difVector.normalized;
        }
        else
        {
            var player = ObjectsHolder.Player;

            if (player != null)
                target = ObjectsHolder.Player.gameObject;
        }
    }
}
