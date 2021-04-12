using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static Vector2 GetVector2(this Vector3 vector)
    {
        return new Vector2(vector.x, vector.y);
    }
}
