using UnityEngine;

public class OrcEnemy : Enemy
{
    protected override void Start()
    {
        base.Start();
        moveSpeed = 0.5f;
        bounty = 5;
        damage = 2;
        health = 8;
    }

    protected override void HandlePathEnd()
    {
        RaisePathEndEvent();
        Attack();
    }

    public override void TakeDamage(int amount)
    {
        if (isDead) return;

        health -= amount;
        Debug.Log($"{name} took {amount} damage! HP left: {health}");

        if (health <= 0)
        {
            Die();
        }
    }

    public void Attack()
    {
        Debug.Log($"{name} attacks and deals {damage} damage!");
        Player.Instance.TakeDamage(damage);
    }
}
