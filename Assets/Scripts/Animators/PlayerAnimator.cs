using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : CharacterAnimator {

    public AnimationClip[] abilityAnimations;

    protected override void Start()
    {
        base.Start();
        AbilityManager.instance.onAbilityChanged += OnAbilityChanged;
    }

    void OnAbilityChanged(Ability newAbility, Ability oldAbility, AbilitySlot slot)
    {
        //abilityAnimations[(int)slot] = newAbility.anim;
    }

    protected override void OnAbility(Ability ability)
    {
        base.OnAbility(ability);
    }
}
