using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Tower Stats")]
    public float range = 5f;
    public float fireRate = 1f;
    public int damage = 1;
    public int cost;

    [Header("Targeting")]
    protected Transform target;
    public string enemyTag = "Enemy";

    [Header("Firing")]
    public Transform firePoint;
    public GameObject projectilePrefab;

    [Header("Animation")]
    public Animator animator;

    [Header("Archer Visuals (optional)")]
    [SerializeField] private Animator archerAnimator;

    private float fireCountdown = 0.5f;


    protected virtual void Awake()
    {
        // Main animator (on root)
        if (animator == null)
            animator = GetComponent<Animator>();

        // Archer animator (on child)
        if (archerAnimator == null)
        {
            Animator[] anims = GetComponentsInChildren<Animator>();

            foreach (Animator a in anims)
            {
                if (a != animator) // don't assign the same animator twice
                {
                    archerAnimator = a;
                    break;
                }
            }
        }
    }


    protected virtual void Start()
    {
        InvokeRepeating(nameof(UpdateTarget), 0f, 0.5f);
    }


    protected virtual void Update()
    {
        if (target == null)
        {
            PlayIdle();
            return;
        }

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }


    // -------------------------
    // Target searching
    // -------------------------
    protected virtual void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float closest = Mathf.Infinity;
        GameObject nearest = null;

        foreach (var enemy in enemies)
        {
            Enemy e = enemy.GetComponent<Enemy>();
            if (e == null || e.isDead) 
                continue;

            float d = Vector3.Distance(transform.position, enemy.transform.position);
            if (d < closest)
            {
                closest = d;
                nearest = enemy;
            }
        }

        target = (nearest != null && closest <= range) ? nearest.transform : null;
    }


    // -------------------------
    // Animation Helper
    // -------------------------
    void PlayIdle()
    {
        if (animator)
        {
            animator.ResetTrigger("Shooting");
            animator.SetTrigger("Idle");
        }

        if (archerAnimator && archerAnimator != animator)
        {
            archerAnimator.ResetTrigger("Shooting");
            archerAnimator.SetTrigger("Idle");
        }
    }


    // -------------------------
    // Shooting
    // -------------------------
    protected virtual void Shoot()
    {
        if (target == null || projectilePrefab == null || firePoint == null)
            return;

        Enemy e = target.GetComponent<Enemy>();
        if (e == null)
            return;

        // Play animation
        if (animator)
        {
            animator.ResetTrigger("Idle");
            animator.SetTrigger("Shooting");
        }

        if (archerAnimator && archerAnimator != animator)
        {
            archerAnimator.ResetTrigger("Idle");
            archerAnimator.SetTrigger("Shooting");
        }

        // Spawn projectile
        GameObject projGO = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Projectile proj = projGO.GetComponent<Projectile>();
        if (proj != null)
        {
            proj.Initialize(e, damage);
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
