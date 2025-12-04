using Unity.VisualScripting;
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


    // target searching
    protected virtual void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag); // get all "enemy" in the scene and puts it into an array
        float closest = Mathf.Infinity; 
        GameObject nearest = null;

        foreach (var enemy in enemies) // goes through each of the "enemy" in the array 
        {
            Enemy e = enemy.GetComponent<Enemy>();
            if (e == null || e.isDead)  // checks if "enemy" is dead
                continue;

            float d = Vector3.Distance(transform.position, enemy.transform.position); // gets distance
            if (d < closest)
            {
                closest = d; // sets closest "enemy" distance into closest variable
                nearest = enemy; // sets the nearest "enemy" object to nearest variable to be targeted
            }
        }

        target = (nearest != null && closest <= range) ? nearest.transform : null;
    }


    // animation
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


    // shooting
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
