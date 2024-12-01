/*
Lucas Ayres Student ID 2321346 
All code was created using the following sources:
 Pandemonium(4/12/2020) Unity 2D Platformer for Complete Beginners Playlist Accessed: 20/10/2024
 Game Maker's Toolkit(2/12/2022) The Unity Tutorial For Complete Beginners Accessed: 20/10/2024
 Brackey's(22/1/2017) How to make a Video Game - Getting Started (Unity) Accessed: 20/10/2024
 Stack Overflow Community. (2015). Spawning player at certain point in Unity [Online]. Stack Overflow. 
 Available from: https://stackoverflow.com/questions/31565355/spawning-player-at-certain-point-in-unity?rq=3 [Accessed 27 Oct 2024]
 Unity Documentation was also used but I didn't get anything the series did not cover. 
 ChatGTP was also used to grade Code for maintability and usability - no content generated only gave suggestions
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    private Dictionary<string, int> inventory = new Dictionary<string, int>();

    public TMP_Text InText; // UI Text element to display the inventory

    private void Start()
    {
        if (InText == null)
        {
            InText = GameObject.Find("InText").GetComponent<TMP_Text>();
            if (InText == null)
            {
                Debug.LogError("InText GameObject or TMP_Text component not found!");
                return;
            }
        }
        UpdateInventoryUI();
    }

    // Add an item to the inventory
    public void AddItem(string itemName, int quantity = 1)
    {
        Debug.Log($"Adding {quantity} of {itemName} to inventory.");

        if (inventory.ContainsKey(itemName))
        {
            inventory[itemName] += quantity;
        }
        else
        {
            inventory[itemName] = quantity;
        }

        Debug.Log(itemName + " added. Total: " + inventory[itemName]);

        // Update the inventory display
        UpdateInventoryUI();
    }

    // Use an item from the inventory
    public bool UseItem(string itemName)
    {
        if (inventory.ContainsKey(itemName) && inventory[itemName] > 0)
        {
            inventory[itemName]--;
            Debug.Log(itemName + " used. Remaining: " + inventory[itemName]);

            // Update the inventory display
            UpdateInventoryUI();
            return true;
        }
        else
        {
            Debug.Log("No " + itemName + " left to use!");
            return false;
        }
    }

    // Get item count
    public int GetItemCount(string itemName)
    {
        return inventory.ContainsKey(itemName) ? inventory[itemName] : 0;
    }

    // Update the inventory UI
    private void UpdateInventoryUI()
    {
        if (InText != null)
        {
            InText.text = "Inventory:\n"; // Reset to header
            foreach (var item in inventory)
            {
                InText.text += $"{item.Key}: {item.Value}\n"; // Add each item and its quantity
            }
        }
        else
        {
            Debug.LogWarning("InText (TMP_Text) is not assigned!");
        }
    }
}