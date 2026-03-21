using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField]
    protected float shortDamage;
    [SerializeField]
    protected float shortAttackSpeed;
    protected float shortCoolDown;

    [SerializeField]
    protected float longDamage;
    [SerializeField]
    protected float longAttackSpeed;
    protected float longCoolDown;

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
    protected abstract void LongAttack();
    /// <summary>
    /// Check cooldown and runs attack
    /// </summary>
    protected abstract void ShortAttack();
    /// <summary>
    /// Run movement logic
    /// </summary>
    protected abstract void Move();

    /// <summary>
    /// Runs logic for what different attack is used 
    /// </summary>
    protected abstract void Attack();
}
