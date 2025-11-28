using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected Enemy targetEnemy;
    protected float speed = 5f;
    protected int damage = 1;

    public void Initialize(Enemy enemy, int damageAmount)
    {
        targetEnemy = enemy;
        damage = damageAmount;
    }

    protected virtual void Update()
    {
        if (targetEnemy == null)
        {
            Destroy(gameObject);
            return;
        }

        // Move toward enemy position
        Vector3 direction = (targetEnemy.transform.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime, Space.World);


        // Check hit distance
        if (Vector3.Distance(transform.position, targetEnemy.transform.position) < 0.2f)
        {
            HitTarget();
        }
    }

    protected virtual void HitTarget()
    {
        if (targetEnemy != null)
        {
            targetEnemy.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
