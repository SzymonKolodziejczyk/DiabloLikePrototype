using UnityEngine;

public class CharacterStats : MonoBehaviour {

    public Stat health;
    public Stat healthRegen;
    public float currentHealth { get; private set; }

    public Stat damage;
    public Stat armor;

    public event System.Action<float, float> OnHealthChanged;

    private void Awake()
    {
        currentHealth = health.GetValue();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            //TakeDamage(10);
        }
        RegenHealth(Time.deltaTime);
    }

    public void TakeDamage(float damage)
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");

        if (OnHealthChanged != null)
            OnHealthChanged(health.GetValue(), currentHealth);

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public void RegenHealth(float dt)
    {
        currentHealth += healthRegen.GetValue() * dt;
        if (OnHealthChanged != null)
            OnHealthChanged(health.GetValue(), currentHealth);
    }

    public virtual void Die()
    {
        // DIE
        Debug.Log(transform.name + " died.");
    }
}
