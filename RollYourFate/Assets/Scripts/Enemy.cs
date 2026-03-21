using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public class Enemy : Entity
{
    // Fields
    [SerializeField] public float maxSpeed;
    [SerializeField] public float maxForce;
    private Vector3 velocity;
    private Vector3 acceleration;

    // reference to player
    [SerializeField] public Player player;

    // all walls in the game
    [SerializeField] public List<GameObject> walls;

    // radius around enemy that it detects walls
    [SerializeField] public float radius;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        acceleration = Vector3.zero;
        Vector3 ultimateForce = Seek(player) * 0.6f;
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
    /// Enemies always move towards the player
    /// </summary>
    protected override void Move()
    {

    }

    protected override void LongAttack()
    {
        if (longCoolDown >= longAttackSpeed)
        {
            // attack success 
        }
    }

    protected override void ShortAttack()
    {
        throw new System.NotImplementedException();
    }

    // Moving helper methods

    protected Vector3 Seek(Entity player)
    {
        Vector3 desiredVelocity = player.transform.position - gameObject.transform.position;

        desiredVelocity = desiredVelocity.normalized * maxSpeed;

        Vector3 steeringForce = desiredVelocity - velocity;

        return steeringForce;
    }

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
            if (Vector3.Distance(gameObject.transform.position, walls[i].transform.position) <= radius)
            {
                wallsInRange.Add(walls[i]);
            }
        }
        return wallsInRange;
    }
}
