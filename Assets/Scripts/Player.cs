using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerController controller;

    private string enemyTag = "Enemy";

    public void Initialize()
    {
        controller = GetComponent<PlayerController>();
        if (controller != null)
            controller.Initialize();
        else
            Debug.LogError("PlayerController component is missing on player object.");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == enemyTag)
            GameManager.Instance.KillPlayer();
    }
}
