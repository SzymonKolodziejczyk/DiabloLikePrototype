using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lootable : MonoBehaviour {

    public float radius = 3f;
    public Transform interactionTransform;

    bool isFocus = false;
    Transform player;

    bool hasLooted = false;

    public virtual void Loot()
    {
        Debug.Log("Interacting with " + transform.name);
    }

    public virtual void Drop()
    {
        Debug.Log("Dropping " + transform.name);
    }

    void Start()
    {
        player = PlayerManager.instance.player.transform;
    }

    void Update()
    {
        if (!hasLooted)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (distance <= radius)
            {
                FollowPlayer();
            }
        }
    }

    void FollowPlayer()
    {

    }


    private void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
        {
            interactionTransform = transform;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
