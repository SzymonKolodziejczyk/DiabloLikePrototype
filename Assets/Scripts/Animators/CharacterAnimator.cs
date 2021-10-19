using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour {

    public AnimationClip replaceableAttackAnim;
    public AnimationClip[] defaultAttackAnimSet;
    protected AnimationClip[] currentAttackAnimSet;

    const float animationSmoothTime = .1f;

    NavMeshAgent agent;
    protected Animator anim;
    protected CharacterCombat combat;
    protected AnimatorOverrideController overrideController;

    // Use this for initialization
    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        combat = GetComponent<CharacterCombat>();

        overrideController = new AnimatorOverrideController(anim.runtimeAnimatorController);
        anim.runtimeAnimatorController = overrideController;

        currentAttackAnimSet = defaultAttackAnimSet;
        combat.OnAttack += OnAttack;
        combat.OnAbility += OnAbility;

    }

    // Update is called once per frame
    void Update () {
        float speedPercent = agent.velocity.magnitude / agent.speed;
        anim.SetFloat("speedPercent", speedPercent, animationSmoothTime, Time.deltaTime);

        anim.SetBool("inCombat", combat.InCombat);
    }

    protected virtual void OnAttack()
    {
        Debug.Log("Attack anim");
        anim.SetTrigger("attack");
        //int attackIndex = Random.Range(0, currentAttackAnimSet.Length);
        int attackIndex = 0;
        overrideController[replaceableAttackAnim.name] = currentAttackAnimSet[attackIndex]; 
    }

    protected virtual void OnAbility(Ability ability)
    {
        Debug.Log("Ability anim");
        anim.SetTrigger("attack");
        int abilityIndex = Random.Range(0, ability.anim.Length);
        overrideController[replaceableAttackAnim.name] = ability.anim[abilityIndex];
    }
}
