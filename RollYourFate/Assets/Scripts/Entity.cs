using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField]
    float shortDamage;
    [SerializeField]
    float longDamage;
    [SerializeField]
    float health;

    [SerializeField]
    protected float speed;
    protected Vector2 position;


    protected Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    protected abstract void LongAttack();
    protected abstract void ShortAttack();
    protected abstract void Move();
}
