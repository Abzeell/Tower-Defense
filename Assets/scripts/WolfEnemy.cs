using UnityEngine;

public class WolfEnemy : Enemy
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        moveSpeed = 2f;
        damage = 1;
        health = 4;
        bounty = 2; // override attributes from parent class
    }

    protected override void HandlePathEnd()
    {
        RaisePathEndEvent();
        Attack();
    }

    public override void TakeDamage(int amount)
    {

        health -= amount; // damage is passed from projectile object
        Debug.Log($"{name} took {amount} damage! HP left: {health}");

        if (health <= 0)
        {
            Die(); // Death is handled in parent
        }
    }

    public void Attack()
    {
        Debug.Log($"{name} attacks and deals {damage} damage!");
        Player.Instance.TakeDamage(damage); // access the player instance to deal damage
    }
}
