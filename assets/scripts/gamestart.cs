using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Method to start the game
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene"); // Load your game scene
    }

    // Method to quit the application
    public void QuitGame()
    {
        Application.Quit(); // Quit the application (works in standalone builds)
    }
}
