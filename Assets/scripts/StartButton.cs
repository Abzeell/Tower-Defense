using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene loading

public class StartButton : MonoBehaviour
{
    // This method will be called when the button is pressed
    public void LoadGameScene()
    {
        // Replace "GameScene" with the exact name of your scene
        SceneManager.LoadScene("Game Screen");
    }
}
