using System.Collections;
using UnityEngine;

public class MovableObject : MonoBehaviour
{
    protected Vector2 GravityVector = Vector2.zero;
    [SerializeField] protected float mass;


    protected virtual void FixedUpdate ()
    { 
        DefaultUpdateMovement();
    }


    public virtual void DefaultUpdateMovement()
    {
	}

    public void GravityAdd(Vector2 GravityModifier)
    {
        GravityVector += GravityModifier;
    }
}
