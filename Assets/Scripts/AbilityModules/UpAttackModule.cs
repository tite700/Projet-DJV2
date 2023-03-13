using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpAttackModule : GroundedControllerAbilityModule
{
    [SerializeField] int startUp = 10;
    [SerializeField] int endFrame = 32;
    [SerializeField] int endLag = 45;
    [SerializeField] float threshold = 0.05f;

    [SerializeField] GameObject OriginalBlackHole;

    GameObject copiedBlackHole;



    protected int currentFrame;
    protected bool canEnd = true;

    protected override void ResetState()
    {
        base.ResetState();
        currentFrame = 0;
        canEnd = true;
    }

    public override void FixedUpdateModule()
    {
        canEnd = false;
        currentFrame++;
        if (currentFrame == startUp)
        {
            copiedBlackHole = Instantiate(OriginalBlackHole, transform);
            copiedBlackHole.SetActive(true);
        }
        if (currentFrame == endFrame)
        {
            copiedBlackHole.GetComponent<AttackBlackHole>().End();
            Destroy(copiedBlackHole);
        }
        if (currentFrame == endLag)
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
        if (GetButtonInput("Attack").m_IsPressed && inputDir.y > threshold && inputDir.y >= Mathf.Abs(inputDir.x))
        {
            return true;
        }
        return false;
    }


}