using UnityEngine;

public class WizardTower : Tower
{
    protected override void Start()
    {
        base.Start();
        cost = 25;
        range = 2f;
        fireRate = 0.2f;
        damage = 3; // override attributes from parent class
    }
}
