using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDownModule : GroundedControllerAbilityModule
{
    [SerializeField] int startUp = 3;
    [SerializeField] int riseFrame = 18;
    [SerializeField] int topFrame = 10;
    [SerializeField] int endLag = 20;
    [SerializeField] float threshold = 0.05f;
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] int stun;
    [SerializeField] int damage = 20;
    [SerializeField] float hitboxRadius = 0.55f;
    [SerializeField] Vector2 Knockback;


    List<CharacterControllerBase> hitEnnemies;


    protected Vector2 movementVector;

    protected int FrameTillNext;
    protected bool canEnd = true;
    protected string state = "idle";

    protected override void ResetState()
    {
        base.ResetState();
        FrameTillNext = 0;
        canEnd = true;
        state = "idle";
        hitEnnemies = new List<CharacterControllerBase>();
    }

    public override void FixedUpdateModule()
    {
        canEnd = false;
        if (state == "idle")
        {
            FrameTillNext = startUp;
            state = "start";
        }
        else
        {
            FrameTillNext--;
        }
        if (state == "start")
        {
            if (FrameTillNext == 0)
            {
                if (m_ControlledColliderBase.IsGrounded())
                {
                    FrameTillNext = riseFrame;
                    state = "rise";
                    movementVector = new Vector2(movementSpeed*0.15f * (m_CharacterControllerBase.IsLookingRight() ? 1 : -1), movementSpeed);
                }
                else
                {
                    FrameTillNext = topFrame;
                    state = "top";
                }
            }
            
        }
        if (state == "rise")
        {
            if(FrameTillNext == 0)
            {
                FrameTillNext = topFrame;
                state = "top";
            }
            else
            {

                m_ControlledColliderBase.UpdateWithVelocity(movementVector);
            }
        }
        if (state == "top")
        {
            if(FrameTillNext == 0)
            {
                state = "fall";
                movementVector = new Vector2( 0, -3*movementSpeed);
            }
        }
        if (state == "fall")
        {
            if (!m_ControlledColliderBase.IsGrounded())
            {
                m_ControlledColliderBase.UpdateWithVelocity(movementVector);
                Collider[] hitBoxColliders = Physics.OverlapSphere(m_CharacterControllerBase.transform.position, hitboxRadius);
                foreach (var collider in hitBoxColliders)
                {
                    if (collider.TryGetComponent(out CharacterControllerBase character))
                    {
                        if (character != m_CharacterControllerBase && !hitEnnemies.Contains(character))
                        {
                            character.TakeDamage(damage, stun, Knockback);;
                            hitEnnemies.Add(character);
                        }
                    }
                }
            }
            else
            {
                state = "end";
                FrameTillNext = endLag;
                hitEnnemies.Clear();
            }
        }
        if (state == "end" && FrameTillNext == 0)
        {
            state = "idle";
            canEnd = true;
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
        if (GetButtonInput("Attack").m_IsPressed && Mathf.Abs(inputDir.y) > threshold && inputDir.y <= 0-Mathf.Abs(inputDir.x))
        {
            return true;
        }
        return false;
    }


}
