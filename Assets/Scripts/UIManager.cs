using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject SkillBar;

    Image healthGlobe;
    Image resourceGlobe;

    Image[] abilities;
    Image ability2;
    Image ability3;
    Image ability4;

    // Use this for initialization
    void Start () {
        healthGlobe = SkillBar.transform.Find("HealthUI").GetChild(0).GetComponent<Image>();
        resourceGlobe = SkillBar.transform.Find("ResourceUI").GetChild(0).GetComponent<Image>();


        abilities = new Image[4];
        for (int i = 0; i < abilities.Length; i++)
        {
            abilities[i] = SkillBar.transform.Find("AbilityParent").GetChild(i).GetComponent<Image>();
        }

        PlayerManager.instance.player.GetComponent<CharacterStats>().OnHealthChanged += OnHealthChanged;
        //GetComponent<AbilityManager>().onAbilityChanged += OnAbilityChanged;
    }

    //void OnAbilityChanged(Ability newAbility, Ability oldAbility, AbilitySlot slot)
    //{
    //    throw new NotImplementedException();
    //}

    void OnHealthChanged(float maxHealth, float currentHealth)
    {
        if(healthGlobe != null)
        {
            float healthPercent = currentHealth / maxHealth;
            healthGlobe.fillAmount = healthPercent;
        }
    }

    // Update is called once per frame
    void LateUpdate () {
        float[] abilityCooldowns = GetComponent<AbilityManager>().abilityCooldowns;
        Ability[] currentAbilities = GetComponent<AbilityManager>().currentAbilities;
        for (int i = 0; i < abilityCooldowns.Length; i++)
        {
            if(currentAbilities[i] != null)
            {
                float cooldownPercent = 1 - (abilityCooldowns[i] / currentAbilities[i].cooldown);
                abilities[i].fillAmount = cooldownPercent;
            }
        }

    }
}
