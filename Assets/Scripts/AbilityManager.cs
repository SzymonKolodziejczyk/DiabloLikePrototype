using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour {

    #region Singleton

    public static AbilityManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public Ability[] currentAbilities;
    public Ability[] defaultAbilities;

    public float[] abilityCooldowns;

    public delegate void OnAbilityChanged(Ability newAbility, Ability oldAbility, AbilitySlot slot);
    public OnAbilityChanged onAbilityChanged;
    
    // Use this for initialization
    void Start () {
        int numSlots = System.Enum.GetNames(typeof(AbilitySlot)).Length;
        //currentAbilities = new Ability[numSlots];
        currentAbilities = defaultAbilities;

        abilityCooldowns = new float[numSlots];
        //for (int i = 0; i < currentAbilities.Length; i++)
        //{
        //    abilityCooldowns[i] = currentAbilities[i].cooldown;
        //}
    }

    public void Equip(Ability newAbility, AbilitySlot abilitySlot)
    {
        int slotIndex = (int)abilitySlot;

        Ability oldAbility = null;

        if (currentAbilities[slotIndex] != null)
        {
            oldAbility = currentAbilities[slotIndex];
        }

        if (onAbilityChanged != null)
        {
            onAbilityChanged.Invoke(newAbility, oldAbility, abilitySlot);
        }

        currentAbilities[slotIndex] = newAbility;
    }

    public void Unequip(int slotIndex)
    {
        if (currentAbilities[slotIndex] != null)
        {
            Ability oldAbility = currentAbilities[slotIndex];

            currentAbilities[slotIndex] = null;

            if (onAbilityChanged != null)
            {
                onAbilityChanged.Invoke(null, oldAbility, (AbilitySlot)slotIndex);
            }
        }
    }

    // Update is called once per frame
    void Update () {
        for (int i = 0; i < abilityCooldowns.Length; i++)
        {
            if(abilityCooldowns[i] > 0)
            {
                abilityCooldowns[i] -= Time.deltaTime;
            }
        }

        if (Input.GetButtonDown("Ability1") && abilityCooldowns[0] <= 0)
        {
            PlayerManager.instance.player.GetComponent<CharacterCombat>().Ability(currentAbilities[0]);
            abilityCooldowns[0] = currentAbilities[0].cooldown;
        }

        if (Input.GetButtonDown("Ability2") && abilityCooldowns[1] <= 0)
        {
            // Handle ability usage

            //Find objects inside

            //Physics.OverlapSphere()
        }

        if (Input.GetButtonDown("Ability3") && abilityCooldowns[2] <= 0)
        {
            // Handle ability usage

            //Find objects inside

            //Physics.OverlapSphere()
        }

        if (Input.GetButtonDown("Ability4") && abilityCooldowns[3] <= 0)
        {
            // Handle ability usage

            //Find objects inside

            //Physics.OverlapSphere()
        }
    }

    private void OnDrawGizmosSelected()
    {
        foreach (Ability a in defaultAbilities)
        {
            if(a != null)
            {
                a.DrawGizmos();
            }
        }
        
    }
}
