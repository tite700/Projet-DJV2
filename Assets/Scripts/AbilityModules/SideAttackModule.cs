using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideAttackModule : GroundedControllerAbilityModule
{
    [SerializeField] int startUp = 7;
    [SerializeField] int endFrame = 16;
    [SerializeField] int damage;
    [SerializeField] int stun;
    [SerializeField] float hitboxRadius;
    [SerializeField] float threshold = 0.05f;
    [SerializeField] Vector2 Knockback;


    protected int currentFrame;
    protected bool canEnd = true;
    protected Vector3 lookDir;

    protected override void ResetState()
    {
        base.ResetState();
        currentFrame = 0;
        canEnd = true;
    }

    public override void FixedUpdateModule()
    {
        canEnd = false;
        if (currentFrame == 0)
        {
            lookDir = new Vector3(m_CharacterControllerBase.GetInputMovement().x, 0,0).normalized;
        }
        currentFrame++;
        if (currentFrame == startUp)
        {
            Collider[] hitBoxColliders = Physics.OverlapSphere(m_CharacterControllerBase.transform.position + lookDir * hitboxRadius, hitboxRadius);
            foreach (var collider in hitBoxColliders)
            {
                if (collider.TryGetComponent(out CharacterControllerBase character))
                {
                    if (character != m_CharacterControllerBase)
                    {
                        character.TakeDamage(damage, stun, Knockback);
                    }
                }
            }
        }
        if (currentFrame == endFrame)
        {
            canEnd = true;
            currentFrame = 0;
        }
        if (!m_ControlledColliderBase.IsGrounded())
        {
            m_CharacterControllerBase.DefaultUpdateMovement();
        }
        else
        {
            m_ControlledColliderBase.UpdateWithVelocity(Vector2.zero);
        }
        
    }

    

    public override bool CanEnd()
    {
        return (canEnd||m_CharacterControllerBase.GetStun() >0);
    }

    public override bool IsApplicable(){
        if (!DoesInputExist("Attack"))
        {
            Debug.LogError("Input for module " + GetName() + " not set up");
            return false;
        }
        Vector2 inputDir = m_CharacterControllerBase.GetInputMovement();
        if (GetButtonInput("Attack").m_IsPressed && Mathf.Abs(inputDir.x) > threshold && Mathf.Abs(inputDir.x) >= Mathf.Abs(inputDir.y))
        {
            Debug.Log(inputDir);
            return true;
        }
        return false;
    }


}