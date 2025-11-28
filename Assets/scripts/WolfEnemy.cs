using UnityEngine;

public class WolfEnemy : Enemy
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        moveSpeed = 1.5f;
        damage = 1;
        health = 5;
        bounty = 3;
    }

    protected override void HandlePathEnd()
    {
        RaisePathEndEvent();
        Attack();
    }

    public override void TakeDamage(int amount)
    {

        health -= amount;
        Debug.Log($"{name} took {amount} damage! HP left: {health}");

        if (health <= 0)
        {
            Die(); // Death is handled in parent
        }
    }

    public void Attack()
    {
        Debug.Log($"{name} attacks and deals {damage} damage!");
        Player.Instance.TakeDamage(damage);
    }
}
