using UnityEngine;

public class OrcEnemy : Enemy
{
    protected override void Start()
    {
        base.Start();
        moveSpeed = 1f;
        bounty = 10;
        damage = 2;
        health = 20;
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
