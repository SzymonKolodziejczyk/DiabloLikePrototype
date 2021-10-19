using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AOE Circle Ability", menuName = "Ability/AOE Circle")]
public class AOECircle : Ability {

    public float radius = 3f;
    public Transform offset;

    public override Collider[] Use(Vector3 position, LayerMask enemyMask)
    {
        //base.Use(position, enemyMask);
        
        return Physics.OverlapSphere(position, radius, enemyMask);
    }

    public override void DrawGizmos()
    {
        if(PlayerManager.instance != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(PlayerManager.instance.player.transform.position, radius);
        }
        
    }


}
