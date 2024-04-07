using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    public GameObject objectToDisappear; // Reference to the object to disappear

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the other collider is the player
        if (other.CompareTag("Player"))
        {
            // Make the object disappear
            if (objectToDisappear != null)
            {
                // Uncomment one of the lines below depending on whether you want to deactivate or destroy the object
                objectToDisappear.SetActive(false); // Deactivate the object
                // Destroy(objectToDisappear); // Destroy the object
            }
        }
    }
}
