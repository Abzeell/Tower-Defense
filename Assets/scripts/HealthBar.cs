using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthImage;
    [SerializeField] private Sprite[] healthSprites; // size = 6 (0 to 5)

    public void UpdateHealth(int currentHP)
    {
        // Convert 10 HP into range 0–5
        int barIndex = Mathf.CeilToInt(currentHP / 2f);

        // Clamp to avoid errors
        barIndex = Mathf.Clamp(barIndex, 0, 5);

        healthImage.sprite = healthSprites[barIndex];
    }
}
