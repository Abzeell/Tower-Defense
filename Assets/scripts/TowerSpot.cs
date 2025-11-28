using UnityEngine;

public class TowerSpot : MonoBehaviour
{
    public bool isOccupied = false;
    public GameObject currentTower;
    public GameObject placeholderPrefab;
    private TowerManager towerManager;

    void Start()
    {
        if (!isOccupied && placeholderPrefab != null)
        {
            Instantiate(placeholderPrefab, transform.position, Quaternion.identity, transform);
        }

        towerManager = FindObjectOfType<TowerManager>();
    }

    void OnMouseDown()
    {
        if (isOccupied) return;

        towerManager.ShowTowerPopUp(this);
    }

    public void PlaceTower(GameObject tower)
    {
        currentTower = tower;
        isOccupied = true;
        tower.transform.SetParent(transform);
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
