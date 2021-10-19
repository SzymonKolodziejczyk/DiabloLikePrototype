using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable {

    public Item item;

    public float upForce = 1f;
    public float sideForce = .1f;

    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    void PickUp()
    {
        Debug.Log("Picking up " + item.name);
        bool wasPickedUp = Inventory.instance.Add(item);

        if(wasPickedUp)
            Destroy(gameObject);

    }

    public void Drop()
    {
        float xForce = Random.Range(-sideForce, sideForce);
        float yForce = Random.Range(upForce / 2f, upForce);
        float zForce = Random.Range(-sideForce, sideForce);

        Vector3 force = new Vector3(xForce, yForce, zForce);


        GetComponent<Rigidbody>().AddForce(force);
        //GetComponent<Rigidbody>().AddExplosionForce(2f, transform.position, 1f, 0f);
    }
}
