using UnityEngine;
using System.Collections;
//--------------------------------------------------------------------
//Gravity module is a movement ability
//If the Special input is pressed, upward gravity is applied
//--------------------------------------------------------------------
public class GravityUpModule : GroundedControllerAbilityModule
{
    [SerializeField] float m_GravityStrength = 0.0f;

    //Called for every fixedupdate that this module is active
    public override void FixedUpdateModule(){

        m_CharacterControllerBase.DefaultUpdateMovement(Vector2.up * m_GravityStrength);
    }
    //Character needs to be on the floor and pressing the Sprint button
    //Query whether this module can be active, given the current state of the character controller (velocity, isGrounded etc.)
    //Called every frame when inactive (to see if it could be) and when active (to see if it should not be)
    public override bool IsApplicable(){
        if (!DoesInputExist("Special"))
        {
            Debug.LogError("Input for module " + GetName() + " not set up");
            return false;
        }
        if (GetButtonInput("Special").m_IsPressed)
        {
            return true;
        }
        return false;
    }

    
}
