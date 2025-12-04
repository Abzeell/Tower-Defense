using UnityEngine;
using System;

public abstract class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public float moveSpeed;

    [SerializeField]
    protected int health;
    [SerializeField]
    protected int bounty;
    protected int damage;

    [Header("Animation")]
    public Animator animator; // assign in inspector
    public bool isDead = false;

    // Events
    public event Action OnPathReachedEnd; // this event will be triggered by the lambda functions below and will invoke the functions subsribed
    public event Action OnKilled; // this event will be triggered by the lambda functions below and will invoke the functions subsribed

    protected Node<GameObject> currentNode;
    protected Path path;
    

    protected virtual void Start()
    {
        path = FindFirstObjectByType<Path>();

        if (path == null || path.PathNodes.Head == null)
        {
            Debug.LogWarning($"{name}: No Path found!");
            Destroy(gameObject);
            return;
        }

        currentNode = path.PathNodes.Head;
        transform.position = currentNode.Value.transform.position;

        if (animator == null)
            animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        if (isDead) return; // stop moving if dead

        if (currentNode == null || currentNode.Next == null)
        {
            HandlePathEnd();
            Destroy(gameObject); // optional
            return;
        }

        MoveAlongPath();
    }

    protected void MoveAlongPath()
    {
        Vector3 targetPos = currentNode.Next.Value.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPos) < 0.05f)
            currentNode = currentNode.Next;
    }

    protected void RaisePathEndEvent() => OnPathReachedEnd?.Invoke(); // lambda function to trigger the action "OnPathReachedEnd" above
    protected void RaiseKilledEvent() => OnKilled?.Invoke(); // lambda function to trigger the action "OnKilled" above

    protected virtual void Die()
    {
        if (isDead) return;
        isDead = true;

        moveSpeed = 0f; // stop moving

        Player.Instance.AddMoney(bounty);

        if (animator != null)
            animator.SetTrigger("Die"); // trigger death animation

        RaiseKilledEvent();

        // destroy after animation length
        float deathLength = animator != null ? animator.GetCurrentAnimatorStateInfo(0).length : 1f;
        Destroy(gameObject, deathLength);
    }

    protected abstract void HandlePathEnd(); // child will implement since each enemies have different damage
    public abstract void TakeDamage(int amount); // child will implement since each enemies have different hp
}
