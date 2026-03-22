using UnityEngine;

public class Mimic : Enemy
{
    private bool battleMode = false;

    [SerializeField]
    private GameObject itemDropPrefab;

    [SerializeField]
    Sprite attackSprite;

    [SerializeField]
    Collider2D attackCollider;

    [SerializeField]
    Collider2D triggerCollider;

    /// <summary>
    /// Can not attack until interaced with
    /// </summary>
    protected override void Attack()
    {
        if (battleMode)
        {
            base.Attack();
        }
    }

    /// <summary>
    /// Stationary 
    /// </summary>
    protected override void Move()
    {
        return;
    }

    /// <summary>
    /// Can not take damage until interacted with
    /// </summary>
    /// <param name="damage"></param>
    public override void TakeDamage(float damage)
    {
        if (battleMode)
        {
            base.TakeDamage(damage);
        }
    }

    /// <summary>
    /// Player interacted with chest enter mattle mode
    /// </summary>
    public void Interact()
    {
        battleMode = true;
    }

    public void OnDestroy()
    {
        Instantiate(itemDropPrefab, transform.position, Quaternion.identity);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        battleMode = true;

        this.gameObject.GetComponent<SpriteRenderer>().sprite = attackSprite;
        attackCollider.enabled = true;
        triggerCollider.enabled = false;
    }
}
