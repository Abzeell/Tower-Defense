using UnityEngine;

public class ArcherTower : Tower
{
    protected override void Start()
    {
        base.Start();
        cost = 10;
        range = 4f;
        fireRate = 2f;
        damage = 1;  
    }
}
