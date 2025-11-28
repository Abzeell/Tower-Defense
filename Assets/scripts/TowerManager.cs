using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public TowerSpot[] towerSpots; // assign in inspector
    public GameObject[] towerPrefabs; // all tower types
    public Player player;


    [SerializeField] private TowerPlacementUi placementUI;

    private TowerSpot currentSpot;

    void Start()
    {
        player = FindFirstObjectByType<Player>();
    }

    public void ShowTowerPopUp(TowerSpot spot)
    {
        if (spot == null) return;

        currentSpot = spot;

        // Show popup UI (e.g. Archer / Wizard tower options)
        if (placementUI != null)
            placementUI.ShowPopup();
        else
            Debug.LogWarning("⚠️ TowerPlacementUI not assigned in TowerManager!");
    }


    public void PlaceTower(int towerIndex)
    {
        if (currentSpot == null) return;
        if (currentSpot.isOccupied) return;

        if (towerIndex < 0 || towerIndex >= towerPrefabs.Length) // check for valid index 
        {
            Debug.LogWarning("⚠️ Invalid tower index!");
            return;
        }

        Tower towerPrefabComponent = towerPrefabs[towerIndex].GetComponent<Tower>();
        int towerCost = towerPrefabComponent != null ? towerPrefabComponent.cost : 0;

        // Use singleton to get player
        if (Player.Instance.GetMoney() < towerCost)
        {
            Debug.LogWarning("Insufficient Funds");
            return;
        }

        Player.Instance.ReduceMoney(towerCost); // always affects the correct Player instance

        GameObject towerGO = Instantiate(towerPrefabs[towerIndex], currentSpot.transform.position, Quaternion.identity); // spawning tower
        currentSpot.PlaceTower(towerGO);

        // Hide popup and clear selection
        if (placementUI != null)
            placementUI.HidePopup();

        currentSpot = null;
    }

    public void CancelPlacement()
    {
        if (placementUI != null)
            placementUI.HidePopup();

        currentSpot = null;
    }
}
