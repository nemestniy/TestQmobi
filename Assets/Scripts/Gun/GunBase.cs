using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunBase : MonoBehaviour
{
    public delegate void GunEvents(int points);
    public event GunEvents WasHit;
    public abstract void Shoot();

    public virtual void OnHitting(int points)
    {
        WasHit?.Invoke(points);
    }
}
