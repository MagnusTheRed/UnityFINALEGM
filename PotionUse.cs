using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PotionUse : MonoBehaviour
{
    public string potionName = "Potion"; // Name of item
    public int potionScoreValue = 100; // Effect of potion (add score in this case)
    public ParticleSystem potionEffect; // Reference to the particle system

    private void Update()
    {
        // Input to use the potion
        if (Input.GetKeyDown(KeyCode.P))
        {
            UsePotion();
        }
    }

    private void UsePotion()
    {
        // Check InventoryManager to find and use potions
        InventoryManager inventory = FindObjectOfType<InventoryManager>();
        if (inventory != null)
        {
            int potionCount = inventory.GetItemCount(potionName);
            if (potionCount > 0)
            {
                inventory.UseItem(potionName); // Decrease potions
                ApplyPotionEffect();
            }
            else
            {
                Debug.LogWarning($"No {potionName} left to use!");
            }
        }
        else
        {
            Debug.LogError("InventoryManager not found!");
        }
    }

    private void ApplyPotionEffect()
    {
        // Add score (or potentially other effects) for using the potion
        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager != null)
        {
            scoreManager.AddScore(potionScoreValue);
            Debug.Log($"{potionName} used! {potionScoreValue} points added.");
        }

        // Play the potion particle effect
        if (potionEffect != null)
        {
            potionEffect.Play();

            // Optionally stop the particle effect after its duration
            StartCoroutine(StopPotionEffectAfterDuration(potionEffect.main.duration));
        }
        else
        {
            Debug.LogWarning("Potion particle effect is not assigned!");
        }
    }

    private IEnumerator StopPotionEffectAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        if (potionEffect != null)
        {
            potionEffect.Stop();
        }
    }
}
