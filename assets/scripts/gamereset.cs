using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public GameObject player; // Reference to the player GameObject

    private void Update()
    {
        // Check if the player GameObject is active in the scene
        if (player != null && !player.activeSelf)
        {
            // Restart the current scene
            RestartScene();
        }
    }

    private void RestartScene()
    {
        // Get the index of the current scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Restart the scene by reloading it
        SceneManager.LoadScene(currentSceneIndex);
    }
}
