using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Ability/New Ability")]
public class Ability : ScriptableObject
{
    new public string name = "New Ability";
    public Sprite icon = null;
    public float cooldown = 0f;

    public AnimationClip[] anim = null;

    //Returns enemies hit when using ability
    public virtual Collider[] Use(Vector3 position, LayerMask enemyMask)
    {

        Debug.Log("Using " + name);
        return Physics.OverlapSphere(position, 1, enemyMask);
    }

    public virtual void DrawGizmos()
    {
        Debug.Log("draw ability");
    }
}

public enum AbilitySlot { Ability1, Ability2, Ability3, Ability4 }
