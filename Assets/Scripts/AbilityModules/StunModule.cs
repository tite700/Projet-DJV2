using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunModule : GroundedControllerAbilityModule
{


    public override void FixedUpdateModule()
    {
        m_CharacterControllerBase.DefaultUpdateMovement();
    }

    public override bool IsApplicable(){
        if (m_CharacterControllerBase.GetStun() > 0)
        {
            return true;
        }
        return false;
    }


}

