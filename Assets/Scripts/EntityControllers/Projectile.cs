using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MovableObject
{
    protected Vector2 velocity  = Vector2.zero;

    [SerializeField] int damage = 10;
    [SerializeField] int stun = 3;
    [SerializeField] Vector2 Knockback;
    

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
        if (collider.collider.TryGetComponent(out CharacterControllerBase character))
        {
            character.TakeDamage(damage, stun, Knockback);
        }
        Destroy(gameObject);
    }
    
}
