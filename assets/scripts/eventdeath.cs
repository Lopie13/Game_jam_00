using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object has the tag "Player" (or any other tag you want to use)
        if (other.CompareTag("Player"))
        {
            // Deactivate the colliding object
            other.gameObject.SetActive(false);
            Debug.Log("Player died!");
        }
    }
}
