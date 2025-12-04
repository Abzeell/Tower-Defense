using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene loading

public class MainButtons : MonoBehaviour
{
    // This method will be called when the button is pressed
    public void LoadGameScene()
    {
        // Replace "GameScene" with the exact name of your scene
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game Screen");
    }

    public void LoadTitleScene()
    {
        // Replace "GameScene" with the exact name of your scene
        Time.timeScale = 1f;
        SceneManager.LoadScene("Title Screen");
    }

    public void LoadGameoverScreen()
    {
        // Replace "GameScene" with the exact name of your scene
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game Over Screen");
    }

    public void ExitGame()
    {
        Debug.Log("Exiting the game...");
        Application.Quit();
    }
}
