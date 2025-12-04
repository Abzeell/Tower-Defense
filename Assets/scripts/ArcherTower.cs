using UnityEngine;

public class ArcherTower : Tower
{
    protected override void Start()
    {
        base.Start();
        cost = 10;
        range = 2.5f;
        fireRate = 1.7f;
        damage = 1;  
    }

    protected override void Shoot()
    {
        // 1. Call the base method to spawn the projectile and handle animation
        base.Shoot();

        // 2. Play the sound effect ONLY if there was a target to shoot at
        if (target != null)
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.arrowShoot);
        }
}
}
