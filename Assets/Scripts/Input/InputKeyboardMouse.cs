using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputKeyboardMouse : InputBase
{
    public override Vector2 CursorPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public override Vector2 GetDirection()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    public override bool Shoot()
    {
        return Input.GetMouseButton(0);
    }
}
