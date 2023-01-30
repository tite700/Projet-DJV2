using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MovableObject
{
    protected Vector2 velocity  = Vector2.zero;
    

    public override void DefaultUpdateMovement()
    {
        Vector2 prevVel = velocity;

        Vector2 newVel = prevVel + GravityVector/mass;

        Vector3 movement = new Vector3(newVel.x, newVel.y, 0);

        transform.position += movement;

        velocity = newVel;

        GravityVector = Vector2.zero;
    }


    void OnCollisionEnter(Collision collider)
    {
        Destroy(gameObject);
    }
    
}
