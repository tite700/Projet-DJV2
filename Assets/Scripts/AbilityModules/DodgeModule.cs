using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeModule : GroundedControllerAbilityModule
{
    [SerializeField] float cooldown = 2.0f;
    [SerializeField] float length = 0.5f;
    [SerializeField] float mix = 0.5f;

    protected bool canDodge = true;
    protected bool isDodging = false;
    protected float timeSinceBeginning;

    protected CapsuleCollider capsColl;

    protected Renderer[] _renderers;
    private List<Color> _colors;
    private List<Material> _materials;





    public override void InitModule(CharacterControllerBase a_CharacterController)
    {
        base.InitModule(a_CharacterController);
        capsColl = m_CharacterControllerBase.gameObject.GetComponent<CapsuleCollider>();
        _colors = new List<Color>();
        _materials = new List<Material>();
        _renderers = m_CharacterControllerBase.gameObject.GetComponentsInChildren<Renderer>();
        foreach (var ren in  _renderers)
        {
            foreach (var mat in ren.materials)
            {
            _materials.Add(mat);
            _colors.Add(mat.color);
            }
        }
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
                changeColor(0.0f);
            }
        } else {
            timeSinceBeginning = 0.0f;
            canDodge = false;
            isDodging = true;
            capsColl.enabled = false;

            changeColor(mix);
        }
        m_CharacterControllerBase.DefaultUpdateMovement();
    }

    private void changeColor(float amount){
        int i = 0;
            foreach (var mat in _materials)
            {
                
                mat.color = Color.Lerp(_colors[i], Color.black, amount);
                i++;
            }
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
