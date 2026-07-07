using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    public List<string> inventoryItems = new List<string>();
    public List<Items> inventoryItemItems = new List<Items>();

    public Items rock;

    void Start()
    {
        inventoryItems.Add("Potion");
        inventoryItems.Add("Sword");
        inventoryItems.Add("Rock");

        // inventoryItemItems.Add(rock);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddNewItemToInventory(Items item)
    {

    }
}
