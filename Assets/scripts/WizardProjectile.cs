using UnityEngine;

public class WizardProjectile : Projectile
{

    public float aoeRadius = 2f;

    void Start()
    {
        speed = 3f;
    }


    protected override void HitTarget()
    {
        if (targetEnemy == null) return;

        // Scan all enemies in the scene (like Tower.UpdateTarget)
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemyGO in enemies)
        {
            Enemy enemyComp = enemyGO.GetComponent<Enemy>();
            if (enemyComp == null || enemyComp.isDead)
                continue;

            float distance = Vector3.Distance(transform.position, enemyGO.transform.position);
            if (distance <= aoeRadius)
            {
                enemyComp.TakeDamage(damage);
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
