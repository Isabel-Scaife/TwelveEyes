using UnityEngine;

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
    // Fields
    [SerializeField] public Sprite weaponSprite;

    public int damage;
    [SerializeField] public RangeType rangeType;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Methods

    /// <summary>
    /// Uses the weapon and deals damage amount to enemy
    /// </summary>
    public void UseWeapon()
    {

    }
}
