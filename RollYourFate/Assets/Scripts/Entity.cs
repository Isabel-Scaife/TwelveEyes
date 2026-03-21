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
    protected float health;

    [SerializeField]
    protected float speed;
    protected Vector2 position;
    protected Vector2 velocity;


    protected Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    protected abstract void LongAttack();
    protected abstract void ShortAttack();
    protected abstract void Move();
}
