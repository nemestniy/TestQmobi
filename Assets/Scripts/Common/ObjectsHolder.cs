using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsHolder : SingletonMonoBehaviour<ObjectsHolder>
{
    public static Player Player { get; private set; }
    public static Vector2 FrameSize { get; private set; }

    public void Initialize()
    {
        var players = FindObjectsOfType<Player>();

        if (players.Length > 1)
            Debug.LogError("Players on scene more than 1.");

        if (players.Length != 0)
            Player = players[0];
        else
            Debug.LogError("Player was not found on scene.");

        FrameSize = Camera.main.ScreenToWorldPoint(new Vector2(Camera.main.scaledPixelWidth, Camera.main.scaledPixelHeight));
    }

    public void ChangePlayer(Player player)
    {
        Player = player;
    }
}
