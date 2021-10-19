using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Chest/New Chest")]
public class Chest : ScriptableObject
{
    public Rarity rarity;

    public Item[] allItems;

    public Item[] Open()
    {
        
        // get items from allitems list depending on rarity
        int lootNum = ((int)rarity + 1) * 2;
        Item[] loot = new Item[lootNum];
        for (int i = 0; i < lootNum; i++)
        {
            int randomIndex = Random.Range(0, allItems.Length);
            loot[i] = allItems[randomIndex];
        }
        return loot;
    }
}

public enum Rarity { Normal, Rare, Magic, Epic, Legendary, Godly }
