using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

// Morgan Meys
/// <summary>
/// Enemy follows the player, but will avoid walls.
/// They will use short attack if in range of player,
/// and long attack when out of range.
/// </summary>
public class Enemy : Entity
{
    // Fields
    [SerializeField] public float maxSpeed;
    [SerializeField] public float maxForce;
    private Vector3 velocity;
    private Vector3 acceleration;

    // Attack radii
    [SerializeField] public float longRadius;
    [SerializeField] public float shortRadius;

    // reference to player
    [SerializeField] public Player player;

    // all walls in the game
    [SerializeField] public List<GameObject> walls;

    // radius around enemy that it detects walls
    [SerializeField] public float radius;

    // Radius (range) of when to seek player
    [SerializeField] public float playerRadius;

    // Timer fields
    private float currentTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // sets default radii if none set in inspector
        if (radius <= 0)
        {
            radius = 2;
        }

        if (playerRadius <= 0)
        {
            playerRadius = 10;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // cooldown and inactive weapon timers / conditions
        Timer();

        Move();
        
    
    }

    /// <summary>
    /// Enemies always move towards the player 
    /// but avoid colliding with walls. This manages
    /// acceleration, velocity, and steering forces
    /// </summary>
    protected override void Move()
    {
        acceleration = Vector3.zero;
        Vector3 ultimateForce = Vector3.zero;

        // Enemies only seek player when in range
        if (InRange(this, player.gameObject, playerRadius))
        {
            ultimateForce += Seek(player) * 0.6f;
        }

        // Enemies only flee walls that are in range
        List<GameObject> wallsInRange = DetectWalls(walls, radius);
        for (int i = 0; i < wallsInRange.Count; i++)
        {
            ultimateForce += Flee(wallsInRange[i]) * 0.7f;
        }

        acceleration += Vector3.ClampMagnitude(ultimateForce, maxForce);

        velocity += acceleration * Time.fixedDeltaTime;

        transform.position += velocity * Time.fixedDeltaTime;
    }

    /// <summary>
    /// Enemies attack when in range of the player, and choose
    /// which attack based on how far the player is
    /// </summary>
    protected override void Attack()
    {
        
    }

    protected override void RangedAttack()
    {
        if (rangedCooldown >= rangedMaxCooldown)
        {
            // attack success 
        }
    }

    protected override void MeleeAttack()
    {
        throw new System.NotImplementedException();
    }

    // Moving helper methods

    /// <summary>
    /// Seeks an entity (the player). Constantly tries to go
    /// in the direction of the entity.
    /// </summary>
    /// <param name="player">The player entity to follow</param>
    /// <returns>The steering force</returns>
    protected Vector3 Seek(Entity player)
    {
        Vector3 desiredVelocity = player.transform.position - gameObject.transform.position;

        desiredVelocity = desiredVelocity.normalized * maxSpeed;

        Vector3 steeringForce = desiredVelocity - velocity;

        return steeringForce;
    }

    /// <summary>
    /// Flees a GameObject (in this game, the enemies flee walls).
    /// Constantly tries to go the opposite direction of the object
    /// </summary>
    /// <param name="obj">The object to flee</param>
    /// <returns>The steering force</returns>
    protected Vector3 Flee(GameObject obj)
    {
        Vector3 desiredVelocity = gameObject.transform.position - obj.transform.position;

        desiredVelocity = desiredVelocity.normalized * maxSpeed;

        Vector3 steeringForce = desiredVelocity - velocity;

        return steeringForce;
    }

    /// <summary>
    /// Determines which walls are in radius of the enemy
    /// </summary>
    /// <param name="walls"></param>
    /// <param name="radius"></param>
    protected List<GameObject> DetectWalls(List<GameObject> walls, float radius)
    {
        List<GameObject> wallsInRange = new List<GameObject>();
        for (int i = 0; i < walls.Count; i++)
        {
            if (InRange(this, walls[i], radius))
            {
                wallsInRange.Add(walls[i]);
            }
        }
        return wallsInRange;
    }

    /// <summary>
    /// Updates cooldowns and manages deactivated weapons
    /// </summary>
    private void Timer()
    {
        currentTimer += Time.deltaTime;

        // ranged timer
        if (rangedCooldown > 0)
        {
            rangedCooldown -= Time.deltaTime;
        }

        // short attack timer
        if (meleeCooldown > 0)
        {
            meleeCooldown -= Time.deltaTime;
        }

        // inactive ranged

        if (rangedColliders[0].enabled && 
            rangedCooldown <= rangedMaxCooldown - rangedActiveTimer)
        {
            // turns off all colliders and sprites ("deactivating collider")
            // when inactive
            for (int i = 0; i < rangedColliders.Length; i++)
            {
                rangedColliders[i].enabled = false;
                rangedSprites[i].enabled = false;
            }
        }

        // inactive short attack

        if (meleeColliders[0].enabled &&
            meleeCooldown <= meleeMaxCooldown - meleeActiveTimer)
        {
            // turns off all colliders and sprites ("deactivating collider")
            // when inactive
            for (int i = 0; i < meleeColliders.Length; i++)
            {
                meleeColliders[i].enabled = false;
                meleeColliders[i].enabled = false;
            }
        }
    }
}
