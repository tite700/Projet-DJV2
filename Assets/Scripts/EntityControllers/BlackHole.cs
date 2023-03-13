using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{

    [SerializeField] float m_GravityStrength = 0.0f;
    [SerializeField] float f_GravityRadius = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Collider[] hitBoxColliders = Physics.OverlapSphere(transform.position, f_GravityRadius);
        foreach (var collider in hitBoxColliders)
        {
            if (collider.TryGetComponent(out MovableObject obj))
            {
                Vector3 dir = transform.position - obj.transform.position;
                Vector2 dir2d = new Vector2(dir.x/dir.sqrMagnitude, dir.y/dir.sqrMagnitude);
                obj.GravityAdd(  dir2d * m_GravityStrength);
            }
        }
    }
}
