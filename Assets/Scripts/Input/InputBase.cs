using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputBase : MonoBehaviour
{
    public abstract Vector2 GetDirection();
    public abstract Vector2 CursorPosition();
    public abstract bool Shoot();
}
