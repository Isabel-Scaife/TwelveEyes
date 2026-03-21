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
    float speed;

    protected abstract void LongAttack();
    protected abstract void ShortAttack();

    protected abstract void Move();
}
