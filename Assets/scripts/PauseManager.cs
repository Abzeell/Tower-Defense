using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject pausePanel; // for pause panel

    // Internal state to track if we are paused
    public static bool isPaused = false;

    private void Start()
    {
        //Ensure the Pause Panel is hidden when the game starts
        if (pausePanel != null)
            pausePanel.SetActive(false);
        
        //Ensure the game time is running normal speed
        Time.timeScale = 1f;
        isPaused = false;
    }

    private void Update()
    {
        // Optional: Allow the "Escape" key to toggle pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    // for pause button
    public void Pause()
    {
        isPaused = true;

        // Show the menu
        if (pausePanel != null)
            pausePanel.SetActive(true);

        // Freeze the game (Stops enemies and towers)
        Time.timeScale = 0f;

        // Play Sound
        if (AudioManager.instance != null)
            AudioManager.instance.PlaySFX(AudioManager.instance.buttonClick);

        ToggleAllColliders(false);
    }

    // for resume button
    public void Resume()
    {
        isPaused = false;

        // Hide the menu
        if (pausePanel != null)
            pausePanel.SetActive(false);

        // Unfreeze the game
        Time.timeScale = 1f;

        // Play Sound
        if (AudioManager.instance != null)
            AudioManager.instance.PlaySFX(AudioManager.instance.buttonClick);

        ToggleAllColliders(true);
    }

    // for retry button
    public void RestartGame()
    {
        // Unpause everything
        Time.timeScale = 1f;
        isPaused = false;

        // Play Click Sound
        if (AudioManager.instance != null)
            AudioManager.instance.PlaySFX(AudioManager.instance.buttonClick);

        // Reload the current active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ToggleAllColliders(bool state)
    {
        // Find every 2D Collider in the scene (TowerSpots, Enemies, Towers)
        Collider2D[] allColliders = FindObjectsByType<Collider2D>(FindObjectsSortMode.None);

        foreach (Collider2D col in allColliders)
        {
            col.enabled = state;
        }
    }

}