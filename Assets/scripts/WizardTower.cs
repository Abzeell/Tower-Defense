using UnityEngine;

public class WizardTower : Tower
{
    protected override void Start()
    {
        base.Start();
        cost = 20;
        range = 3f;
        fireRate = 0.5f;
        damage = 3;
    }

    
}
