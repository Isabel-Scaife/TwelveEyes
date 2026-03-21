using UnityEngine;


enum ControlScheme
{
    WASD,
    JIKL,
    Arrows
}

public class Player : Entity
{

    [SerializeField]
    float switchTime;
    float currentTimer;

    ControlScheme movementControl = ControlScheme.WASD;
    ControlScheme attackControl = ControlScheme.JIKL;
    ControlScheme[] openControls = { ControlScheme.Arrows, ControlScheme.JIKL };

    private void FixedUpdate()
    {
        // 1. Update timers
        Timer();

        // 2. player controls swap
        SwitchControl();

        // 3. move player
        Move();

        // 4. attack 
        Attack();

    }

    private void Timer()
    {
        currentTimer += Time.deltaTime;

        // ================ ranged timers ======================
        if(rangedCooldown > 0 )
        {
            rangedCooldown -= Time.deltaTime;
        }

        // deactive weapon
        if (rangedColliders[0].enabled &&
                 rangedCooldown <= rangedMaxCooldown - rangedActiveTimer)
        {
            // turn off all colliders and sprites 
            for (int i = 0; i < rangedColliders.Length; i++)
            {
                rangedColliders[i].enabled = false;
                rangedSprites[i].enabled = false;
            }
        }

        // ================== melee timers ====================== 
        if (meleeCooldown > 0)
        {
            meleeCooldown -= Time.deltaTime;
        }

        // deactive weapon
        if (meleeColliders[0].enabled &&
                 meleeCooldown <= meleeMaxCooldown - meleeActiveTimer)
        {
            // turn off all colliders and sprites 
            for (int i = 0; i < meleeColliders.Length; i++)
            {
                meleeColliders[i].enabled = false;
                meleeSprites[i].enabled = false;
            }
        }

    }

    void SwitchControl()
    {
        if (currentTimer >= switchTime)
        {
            // 1. hold the new randomized scheme
            // 2. add old control scheme to avaible contorl slots
            // 3. replace old scheme with new 
            int rng = Random.Range(0, 2);
            ControlScheme tempScheme = openControls[rng];
            openControls[rng] = movementControl;
            movementControl = tempScheme;

            // 4. new movement can be anything else 
            rng = Random.Range(0, 2);
            attackControl = openControls[rng];

            // 5. reset timer
            currentTimer = 0;
        }
    }

    /// <summary>
    /// Move when player press correct buttons
    /// </summary>
    protected override void Move()
    {
        position = rb.transform.position;
        velocity = Vector2.zero;

        if (movementControl == ControlScheme.WASD)
        {
            if(Input.GetKey(KeyCode.A)) velocity.x -= 1;
            if(Input.GetKey(KeyCode.D)) velocity.x += 1;
            if(Input.GetKey(KeyCode.S)) velocity.y -= 1;
            if(Input.GetKey(KeyCode.W)) velocity.y += 1;
        }
        else if (movementControl == ControlScheme.JIKL)
        {
            if (Input.GetKey(KeyCode.J)) velocity.x -= 1;
            if (Input.GetKey(KeyCode.L)) velocity.x += 1;
            if (Input.GetKey(KeyCode.K)) velocity.y -= 1;
            if (Input.GetKey(KeyCode.I)) velocity.y += 1;
        }

        else if (movementControl == ControlScheme.Arrows)
        {
            if (Input.GetKey(KeyCode.LeftArrow)) velocity.x -= 1;
            if (Input.GetKey(KeyCode.RightArrow)) velocity.x += 1;
            if (Input.GetKey(KeyCode.DownArrow)) velocity.y -= 1;
            if (Input.GetKey(KeyCode.UpArrow)) velocity.y += 1;
        }

        velocity.Normalize();
        position += velocity * speed * Time.deltaTime;
        rb.transform.position = position;
    }
    /// <summary>
    /// Run corresponding attack to correct button press 
    /// </summary>
    protected override void Attack()
    {
        if (attackControl == ControlScheme.WASD)
        {
            if (Input.GetKey(KeyCode.A)) RangedAttack();
            else if (Input.GetKey(KeyCode.D)) MeleeAttack();
        }
        else if (attackControl == ControlScheme.JIKL)
        {
            if (Input.GetKey(KeyCode.J)) RangedAttack();
            else if (Input.GetKey(KeyCode.L)) MeleeAttack();
        }
        else if (attackControl == ControlScheme.JIKL)
        {
            if (Input.GetKey(KeyCode.LeftArrow)) RangedAttack();
            else if (Input.GetKey(KeyCode.RightArrow)) MeleeAttack();
        }
    }
    protected override void RangedAttack()
    {
        // 1. able to attack
        if (rangedCooldown <= 0)
        {
            // turn on all colliders and sprites 
            for (int i = 0; i < rangedColliders.Length; i++)
            {
                rangedColliders[i].enabled = true;
                rangedSprites[i].enabled = true;
            }

            // reset cool down
            rangedCooldown = rangedMaxCooldown; 
        }
    }

    protected override void MeleeAttack()
    {
        // 1. able to attack
        if (meleeCooldown <= 0)
        {
            // turn on all colliders and sprites 
            for (int i = 0; i < meleeColliders.Length; i++)
            {
                meleeColliders[i].enabled = true;
                meleeSprites[i].enabled = true;
            }

            // reset cool down
            meleeCooldown = meleeMaxCooldown;
        }
        
    }

    
}
