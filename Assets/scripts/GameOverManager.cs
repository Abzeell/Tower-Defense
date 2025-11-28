using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject[] uiToHide;

    private void Start()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false); // hide panel initially
    }

    public void ShowGameOver()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        if (uiToHide != null)
        {
            foreach (var ui in uiToHide)
            ui.SetActive(false);
        }

        // Stop the game if needed
        Time.timeScale = 0f; 
    }

    public void Retry()
    {
        Time.timeScale = 1f; // resume time
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToTitle()
    {
        Time.timeScale = 1f; // resume time
        SceneManager.LoadScene("Title Screen"); // replace with your main menu scene name
    }
}
