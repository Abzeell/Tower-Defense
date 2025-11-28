using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] private int playerHealth = 10;
    [SerializeField] private int playerMoney = 5;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private HealthBar healthBar;


    private void Awake()
    {
        // Ensure only one Player instance exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        UpdateHealthUI();
        UpdateMoneyUI();
        healthBar.UpdateHealth(playerHealth);
    }

    // --- HEALTH CONTROL ---

    public void TakeDamage(int damageAmount)
{
    playerHealth -= damageAmount;

    Debug.Log($"💥 Base hit! Current Health: {playerHealth}");

    UpdateHealthUI();          // Updates TMP text
    healthBar.UpdateHealth(playerHealth); // Updates health bar sprite

    if (playerHealth <= 0)
    {
        HandleGameOver();
    }
}


    private void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = $"Health: {playerHealth}";
        }
        else
        {
            Debug.LogWarning("⚠️ Health Text UI not assigned in the Inspector!");
        }
    }

    // --- MONEY CONTROL ---

    public void AddMoney(int amount)
    {
        playerMoney += amount;
        UpdateMoneyUI();
    }

    public void ReduceMoney(int amount)
    {
        playerMoney -= amount;
        UpdateMoneyUI();
    }

    private void UpdateMoneyUI()
    {
        if (moneyText != null)
        {
            moneyText.text = $"Money: {playerMoney}";
        }
        else
        {
            Debug.LogWarning("⚠️ Money Text UI not assigned in the Inspector!");
        }
    }

    public int GetMoney()
    {
        return playerMoney;
    }

    private void HandleGameOver()
    {
        Debug.Log("💀 GAME OVER!");

        // Find the GameOverManager in the scene
        GameOverManager gameOverManager = FindObjectOfType<GameOverManager>();
        if (gameOverManager != null)
    {
        // Show the Game Over panel
        gameOverManager.ShowGameOver();
    }

        // Optionally stop the game (freeze everything)
        Time.timeScale = 0f;
    }

}
