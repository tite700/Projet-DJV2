using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeModule : GroundedControllerAbilityModule
{
    [SerializeField] float cooldown = 2.0f;
    [SerializeField] float length = 0.5f;

    protected bool canDodge = true;
    protected bool isDodging = false;
    protected float timeSinceBeginning;

    protected CapsuleCollider capsColl;



    public override void InitModule(CharacterControllerBase a_CharacterController)
    {
        base.InitModule(a_CharacterController);
        capsColl = m_CharacterControllerBase.gameObject.GetComponent<CapsuleCollider>();
    }

    protected override void ResetState()
    {
        base.ResetState();
        timeSinceBeginning = 0.0f;
        canDodge = true;
        isDodging = false;
    }

    //Called for every fixedupdate that this module is active
    public override void FixedUpdateModule()
    {
        timeSinceBeginning += Time.deltaTime;
        if (isDodging){
            isDodging = (timeSinceBeginning<length);
            if (!isDodging){
                capsColl.enabled = true;
            }
        } else {
            timeSinceBeginning = 0.0f;
            canDodge = false;
            isDodging = true;
            capsColl.enabled = false;
        }
        m_CharacterControllerBase.DefaultUpdateMovement();
    }

    public override void InactiveUpdateModule()
    {
        if (!canDodge)
        {
            timeSinceBeginning += Time.deltaTime;
            canDodge = (timeSinceBeginning>cooldown);
           
        }
        
    }
    
    //Query whether this module can be active, given the current state of the character controller (velocity, isGrounded etc.)
    //Called every frame when inactive (to see if it could be) and when active (to see if it should not be)
    public override bool IsApplicable(){
        if (!DoesInputExist("Dodge"))
        {
            Debug.LogError("Input for module " + GetName() + " not set up");
            return false;
        }
        if ((GetButtonInput("Dodge").m_IsPressed&&canDodge)||isDodging)
        {
            return true;
        }
        return false;
    }

    
}
