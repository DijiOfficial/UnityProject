using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingAttack : BasicAttack
{
    [SerializeField] protected float _speed = 30.0f;
    void FixedUpdate()
    {
        if (!WallDetection())
            transform.position += _speed * Time.deltaTime * transform.forward;
    }

    //This cannot be defined const as it can only apply to a field which is known at   compile-time. Which is not the case for an array, so doing static readonly, which means it can serve a very similar purpose.
    static readonly string[] RAYCAST_MASK = { "Ground", "StaticLevel" };
    bool WallDetection()
    {
        Ray collisionRay = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(collisionRay,
            Time.deltaTime * _speed, LayerMask.GetMask(RAYCAST_MASK)))
        {
            Kill();
            return true;
        }
        return false;
    }
}
