using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBlackHole : MonoBehaviour
{

    [SerializeField] float m_GravityStrength = 0.0f;
    [SerializeField] float f_GravityRadius = 5.0f;
    [SerializeField] int damagePerFrame = 1;
    [SerializeField] int finalDamage = 5;
    [SerializeField] MovableObject Character;
    [SerializeField] int stun = 10;
    [SerializeField] Vector2 Knockback;

    bool hasended = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!hasended)
        {
        Collider[] hitBoxColliders = Physics.OverlapSphere(transform.position, f_GravityRadius);
        foreach (var collider in hitBoxColliders)
        {
            if (collider.TryGetComponent(out MovableObject obj))
            {
                if (obj != Character)
                {
                    Vector3 dir = transform.position - obj.transform.position;
                    if (dir.sqrMagnitude < 0.25f)
                    {
                        
                        if (obj.TryGetComponent(out CharacterControllerBase charac))
                        {
                            charac.SetPosition(transform.position);
                            charac.TakeDamage(damagePerFrame, 1, Vector2.zero);
                        }
                        else
                        {
                            obj.transform.position = transform.position;
                        }
                    }
                    else
                    {
                        Vector2 dir2d = new Vector2(dir.x/dir.sqrMagnitude, dir.y/dir.sqrMagnitude);
                        obj.GravityAdd(  dir2d * m_GravityStrength);
                    }
                    
                }
                
            }
        }
        }
    }

    public void End()
    {
        hasended = true;
        Collider[] hitBoxColliders = Physics.OverlapSphere(transform.position, f_GravityRadius);
        foreach (var collider in hitBoxColliders)
        {
        if (collider.TryGetComponent(out MovableObject obj))
            {
                Vector3 dir = transform.position - obj.transform.position;
                if (dir.sqrMagnitude < 0.25f)
                {
                    if (obj.TryGetComponent(out CharacterControllerBase charac))
                    {
                        charac.TakeDamage(finalDamage, stun, Knockback);
                    }
                }
            }
        }
    }



}
