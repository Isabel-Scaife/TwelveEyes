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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
