using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject firstCanvas;
    public GameObject secondCanvas;

    // Method to activate the second canvas and deactivate the first canvas
    public void ShowCredits()
    {
        firstCanvas.SetActive(false);
        secondCanvas.SetActive(true);
    }

    // Method to deactivate the second canvas and activate the first canvas
    public void CloseCredits()
    {
        secondCanvas.SetActive(false);
        firstCanvas.SetActive(true);
    }
} 
