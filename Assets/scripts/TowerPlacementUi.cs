using UnityEngine;

public class TowerPlacementUi : MonoBehaviour
{
    [SerializeField] private GameObject panel; // assign your popup panel in the Inspector
    [SerializeField] private TowerManager towerManager;
   

    public void ShowPopup()
    {
        panel.SetActive(true);
    }

    public void HidePopup()
    {
        panel.SetActive(false);
    }

    // Called by UI buttons
    public void OnSelectTower(int index)
    {
        towerManager.PlaceTower(index);
    }

    void Start()
    {
        HidePopup();
    }
}
