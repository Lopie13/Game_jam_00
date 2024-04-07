using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuTrigger : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("main");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object has the tag "Player" (or any other tag you want to use)
        if (other.CompareTag("Player"))
        {
            // Deactivate the colliding object
            SceneManager.LoadScene("main");
        }
    }
}
