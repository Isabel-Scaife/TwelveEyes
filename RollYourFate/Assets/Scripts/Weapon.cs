using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Weapons are short or long attacks
/// </summary>
public enum RangeType
{
    shortAttack,
    longAttack
}

// Morgan Meys
/// <summary>
/// Weapons to kill other entities, each has a range type and a damage amount
/// </summary>
public class Weapon : MonoBehaviour
{

    [SerializeField] public RangeType rangeType;
    [SerializeField]
    private float damage;

    [SerializeField]
    private float hitDelay;
    private float currentDelay = 0;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Entity entityHit = collision.GetComponent<Entity>();

        if(entityHit != null )
        {
            entityHit.TakeDamage(damage);
            currentDelay = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // ranged attack stays on ground 
        if (rangeType == RangeType.longAttack)
        {
            currentDelay += Time.deltaTime;

            // take damage after delay over 
            if (currentDelay > hitDelay)
            {
                Entity entityHit = collision.GetComponent<Entity>();

                if (entityHit != null)
                {
                    entityHit.TakeDamage(damage);
                    currentDelay = 0;
                }
            }
        }
    }
}
