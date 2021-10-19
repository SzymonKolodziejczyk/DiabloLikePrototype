using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpen : Interactable
{
    public Chest chest;
    public GameObject lootObject;

    public override void Interact()
    {
        base.Interact();

        Open();
    }

    void Open()
    {
        Debug.Log("Open Chest");
        Item[] loot = chest.Open();

        //for (int i = 0; i < loot.Length; i++)
        //{
        //    //lootObject.GetComponent<ItemPickup>;
        //    GameObject lootItem = Instantiate(lootObject, transform.position, Quaternion.identity);
        //    ItemPickup itemPickup = lootItem.GetComponent<ItemPickup>();
        //    itemPickup.item = loot[i];

        //    lootItem.SetActive(true);
        //    itemPickup.Drop();
            
        //}
        GameObject lootItem = Instantiate(lootObject, transform.position, Quaternion.identity);
        ItemPickup itemPickup = lootItem.GetComponent<ItemPickup>();
        itemPickup.item = loot[0];

        lootItem.SetActive(true);
        itemPickup.Drop();
    }
}
