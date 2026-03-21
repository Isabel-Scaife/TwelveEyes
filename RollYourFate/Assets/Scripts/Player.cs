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

        if(longCoolDown != 0 )
        {
            longCoolDown = Time.deltaTime;
        }

        if (shortAttackSpeed != 0)
        {
            shortCoolDown = Time.deltaTime;
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

            // same as above but can randomize to the same contorls 
            rng = Random.Range(0, 2);
            tempScheme = openControls[rng];
            openControls[rng] = attackControl;
            attackControl = tempScheme;
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
            if(Input.GetKeyUp(KeyCode.S)) velocity.y -= 1;
            if(Input.GetKeyUp(KeyCode.W)) velocity.y += 1;
        }
        else if (movementControl == ControlScheme.JIKL)
        {
            if (Input.GetKey(KeyCode.J)) velocity.x -= 1;
            if (Input.GetKey(KeyCode.L)) velocity.x += 1;
            if (Input.GetKeyUp(KeyCode.K)) velocity.y -= 1;
            if (Input.GetKeyUp(KeyCode.I)) velocity.y += 1;
        }

        else if (movementControl == ControlScheme.Arrows)
        {
            if (Input.GetKey(KeyCode.LeftArrow)) velocity.x -= 1;
            if (Input.GetKey(KeyCode.RightArrow)) velocity.x += 1;
            if (Input.GetKeyUp(KeyCode.DownArrow)) velocity.y -= 1;
            if (Input.GetKeyUp(KeyCode.UpArrow)) velocity.y += 1;
        }

        velocity.Normalize();
        position = velocity * speed * Time.deltaTime;
        rb.transform.position = position;
    }
    /// <summary>
    /// Run corresponding attack to correct button press 
    /// </summary>
    protected override void Attack()
    {
        if (attackControl == ControlScheme.WASD)
        {
            if (Input.GetKey(KeyCode.A)) LongAttack();
            else if (Input.GetKey(KeyCode.D)) ShortAttack();
        }
        else if (attackControl == ControlScheme.JIKL)
        {
            if (Input.GetKey(KeyCode.J)) LongAttack();
            else if (Input.GetKey(KeyCode.L)) ShortAttack();
        }
        else if (attackControl == ControlScheme.JIKL)
        {
            if (Input.GetKey(KeyCode.LeftArrow)) LongAttack();
            else if (Input.GetKey(KeyCode.RightArrow)) ShortAttack();
        }
    }
    protected override void LongAttack()
    {
        if(longCoolDown >= longAttackSpeed)
        {
            // attack success 
            // turn on collider 
            // turn on sprite render 

            // reset cool down
        }
    }

    protected override void ShortAttack()
    {
        if (shortCoolDown >= shortAttackSpeed)
        {
            // attack success 
            // turn on collider 
            // turn on sprite render 

            // reset cool down
        }
    }
}
