using UnityEngine;

public class WizardProjectile : Projectile
{

    [Header("Visuals")]
    public GameObject explosionPrefab;
    public float aoeRadius = 1.2f; // special attributes AOE

    void Start()
    {
        speed = 3f; // override attribute from parent class
    }


    protected override void HitTarget()
    {
        if (targetEnemy == null) return;

        AudioManager.instance.PlaySFX(AudioManager.instance.wizardImpact); // play sfx

        if (explosionPrefab != null)
        {
            // Spawn the explosion at the projectile's current position
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        // Scan all enemies in the scene (like Tower.UpdateTarget)
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); // finds all "enemy" in the scene and stores into an array
        
        foreach (GameObject enemyGO in enemies) // scans through all "enemy" in the array
        {
            Enemy enemyComp = enemyGO.GetComponent<Enemy>();
            if (enemyComp == null || enemyComp.isDead) // check if certain "enemy" is dead or active
                continue;

            float distance = Vector3.Distance(transform.position, enemyGO.transform.position); // gets "enemy" distance
            if (distance <= aoeRadius)
            {
                enemyComp.TakeDamage(damage); // access the "enemy" to deal damage
            }
        }

        // Destroy the projectile after applying AOE damage
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, aoeRadius);
    }
}
