using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public GunBase gun;

    private new Rigidbody2D rigidbody;
    private InputBase inputBase;
    private Transform body;

    private string bodyObjectName = "Body";

    public void Initialize()
    {
        body = transform.Find(bodyObjectName);

        if (body == null)
            Debug.LogError("Body of player was not found.");

        rigidbody = GetComponent<Rigidbody2D>();
        if (rigidbody == null)
            Debug.LogError("Rigidbody2D component is missing on player object.");

        inputBase = GetComponent<InputBase>();
        if (inputBase == null)
            Debug.LogError("Input component is missing on player object.");

        gun = Instantiate(gun, body);
        gun.WasHit += Gun_WasHit;
    }

    //Check hitings by using event 
    private void Gun_WasHit(int points)
    {
        GameManager.IncreaseScore(points);
    }

    //Use InputBase for easy changing kind of input
    void Update()
    {
        if (inputBase != null)
        {
            var moveDirection = inputBase.GetDirection();
            if (moveDirection.magnitude > 1) moveDirection.Normalize();
            rigidbody.velocity = moveDirection * moveSpeed;

            var mousePosition = inputBase.CursorPosition();
            var direction = (mousePosition - (Vector2)transform.position).normalized;
            body.up = direction;

            if(gun != null)
                if (inputBase.Shoot())
                    gun.Shoot();
        }
    }
}
