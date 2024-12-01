using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pickup : MonoBehaviour
{
    public string itemName = "Potion"; // Default item name
    public int quantity = 1; // Number of potions to add to inventory

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"Player picked up {quantity} {itemName}(s).");

            // Find the InventoryManager and add the potion
            InventoryManager inventory = FindObjectOfType<InventoryManager>();
            if (inventory != null)
            {
                inventory.AddItem(itemName, quantity);
            }

            // Destroy the potion pickup
            Destroy(gameObject);
        }
    }
}