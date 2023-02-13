using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileModule : GroundedControllerAbilityModule
{
    [SerializeField] float cooldown = 0.7f;
    [SerializeField] float ProjectileSpeed = 0.0f;
    [SerializeField] GameObject projectile;

    protected float cooldownLeft;
    protected bool canFire = true;

    protected override void ResetState()
    {
        base.ResetState();
        cooldownLeft = cooldown;
        canFire = true;
    }

    public override void FixedUpdateModule()
    {
        Vector2 forward = m_CharacterControllerBase.IsLookingRight() ? Vector2.right : Vector2.left;
        GameObject newProj = Instantiate(projectile, m_CharacterControllerBase.transform.position + new Vector3(3*forward.x, forward.y, 0), Quaternion.identity);
        newProj.SetActive(true);
        Projectile proj = newProj.GetComponent<Projectile>();
        proj.GravityAdd(forward * ProjectileSpeed);
        canFire = false;
        cooldownLeft = cooldown;
    }

    public override void InactiveUpdateModule()
    {
        if (!canFire)
        {
            cooldownLeft -= Time.deltaTime;
            canFire = (cooldownLeft<0.0f);
           
        }
        
    }

    public override bool IsApplicable(){
        if (!DoesInputExist("Attack"))
        {
            Debug.LogError("Input for module " + GetName() + " not set up");
            return false;
        }
        if (GetButtonInput("Attack").m_IsPressed&& canFire)
        {
            return true;
        }
        return false;
    }
}
