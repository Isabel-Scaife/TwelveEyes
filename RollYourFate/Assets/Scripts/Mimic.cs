using UnityEngine;

public class Mimic : Enemy
{
    private bool battleMode = false;

    [SerializeField]
    private GameObject itemDrop;

    [SerializeField]
    Sprite attackSprite;

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

    private void OnDestroy()
    {
        // Drop item 
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        battleMode = true;


        this.gameObject.GetComponent<SpriteRenderer>().sprite = attackSprite;
    }
}
