using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour {

    public LayerMask enemyMask;

    public float attackSpeed = 1f;
    public float attackCooldown = 0f;
    const float combatCooldown = 5f;
    float lastAttackTime;

    public float attackDelay = .6f;

    public bool InCombat { get; private set; }
    public event System.Action OnAttack;
    public event System.Action<Ability> OnAbility;

    //public delegate void OnAbility(Ability newAbility);
    //public OnAbility onAbility;

    CharacterStats myStats;

    void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }

    void Update()
    {
        attackCooldown -= Time.deltaTime;

        if(Time.time - lastAttackTime > combatCooldown)
        {
            InCombat = false;
        }
    }
    // Auto Attack
    public void Attack (CharacterStats targetStats)
    {
        if(attackCooldown <= 0f)
        {
            StartCoroutine(DoDamage(targetStats, attackDelay));

            if (OnAttack != null)
                OnAttack();

            attackCooldown = 1f / attackSpeed;
            InCombat = true;
            lastAttackTime = Time.time;
        }
        
    }

    // Use Ability
    public void Ability(Ability ability)
    {
        Collider[] enemiesHit = ability.Use(transform.position, enemyMask);

        if (OnAbility != null)
            OnAbility(ability);

        InCombat = true;
        lastAttackTime = Time.time;
        attackCooldown = 1f / attackSpeed;
        //Deal damage
        foreach (Collider enemy in enemiesHit)
        {
            EnemyStats targetStats = enemy.GetComponent<EnemyStats>();
            StartCoroutine(DoDamage(targetStats, attackDelay));
        }
    }

    IEnumerator DoDamage(CharacterStats stats, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (stats != null)
        {
            stats.TakeDamage(myStats.damage.GetValue());
            if (stats.currentHealth <= 0)
            {
                InCombat = false;
            }
        }
        
    }
}
