using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletBase : MonoBehaviour
{
    public abstract void AssignGunParent(GunBase gun);
    public abstract void AddPower(float power);
}
