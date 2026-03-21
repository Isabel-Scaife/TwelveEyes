using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField]
    protected float meleeDamage;
    [SerializeField]
    protected float meleeMaxCooldown;
    [SerializeField]
    protected float meleeCooldown;
    [SerializeField]
    protected float meleeActiveTimer;

    [SerializeField]
    protected SpriteRenderer[] meleeSprites;
    [SerializeField]
    protected Collider2D[] meleeColliders;

    [SerializeField]
    protected float rangedDamage;
    [SerializeField]
    protected float rangedMaxCooldown;
    protected float rangedCooldown;
    [SerializeField]
    protected float rangedActiveTimer;

    [SerializeField]
    protected SpriteRenderer[] rangedSprites;
    [SerializeField]
    protected Collider2D[] rangedColliders;

    [SerializeField]
    protected float maxHealth;
    protected float currentHealth;

    [SerializeField]
    protected float speed;
    protected Vector2 position;
    protected Vector2 velocity;


    protected Rigidbody2D rb;
    //protected SpriteRenderer sprite;
    //protected Collider2D collider;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        
    }
    /// <summary>
    /// Check cooldown and runs attack
    /// </summary>
    protected abstract void RangedAttack();
    /// <summary>
    /// Check cooldown and runs attack
    /// </summary>
    protected abstract void MeleeAttack();
    /// <summary>
    /// Run movement logic
    /// </summary>
    protected abstract void Move();

    /// <summary>
    /// Runs logic for what different attack is used 
    /// </summary>
    protected abstract void Attack();
}
