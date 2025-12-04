using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] private int playerHealth = 10;
    [SerializeField] private int playerMoney = 15;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private HealthBar healthBar;

    public MainButtons buttons;


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
        UpdateMoneyUI(); // initialize the money
        healthBar.UpdateHealth(playerHealth); // initialize the health 
    }

    // hp control
    public void TakeDamage(int damageAmount)
{
    playerHealth -= damageAmount; // update player hp to take damage

    AudioManager.instance.PlaySFX(AudioManager.instance.playerDamage); // play take damage sound

    Debug.Log($"💥 Base hit! Current Health: {playerHealth}");

    healthBar.UpdateHealth(playerHealth); // Updates health bar sprite

    if (playerHealth <= 3)
    {
        AudioManager.instance.PlayMusic(AudioManager.instance.lowHealth, 0.5f); // play low health alarm sound
    }

    if (playerHealth <= 0) // die
    {
        HandleGameOver();
    }
}

    // money control

    public void AddMoney(int amount)
    {
        playerMoney += amount; // update money
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
            moneyText.text = $"{playerMoney}"; // update money text
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

        SceneManager.LoadScene("Game Over Screen"); // loading game over scene
    }

}
