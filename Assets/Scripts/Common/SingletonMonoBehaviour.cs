﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour
    where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    private void Awake()
    {
        var objects = FindObjectsOfType<T>();

        if (objects.Length > 1)
            Debug.LogWarning("Objects " + nameof(T) + " more than 1 on scene.");

        foreach(var item in objects)
        {
            if (Instance == null)
                Instance = item;
            else
                Destroy(item);
        }
    }
}
