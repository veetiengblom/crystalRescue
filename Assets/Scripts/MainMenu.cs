using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject instructionsPanel;

    public void PlayGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void ShowInstructions()
    {
        if (instructionsPanel != null)
        {
            instructionsPanel.SetActive(true);
        }
    }

    public void HideInstructions()
    {
        if (instructionsPanel != null)
        {
            instructionsPanel.SetActive(false);
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit game");
        Application.Quit();
    }
}