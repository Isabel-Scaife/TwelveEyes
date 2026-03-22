using UnityEngine;
using UnityEngine.UI;
using TMPro;


enum ControlScheme
{
    WASD,
    JIKL,
    Arrows
}

public class Player : Entity
{

    bool meleeUpgrade = false;
    bool rangeUpgrade = false;

    [SerializeField]
    float switchTime;
    float currentTimer;

    [SerializeField]
    Slider healthBar;
    [SerializeField]
    Slider switchBar;

    [SerializeField]
    TMP_Text switchText;
    [SerializeField]
    Slider cooldownRanged;
    [SerializeField]
    Slider cooldownMelee;
    [SerializeField]
    TMP_Text cooldownRText;
    [SerializeField]
    TMP_Text cooldownMText;

    [SerializeField]
    ControlScheme movementControl = ControlScheme.WASD;
    [SerializeField]
    ControlScheme attackControl = ControlScheme.JIKL;
    ControlScheme[] openControls = { ControlScheme.Arrows, ControlScheme.JIKL };

    protected override void Start()
    {
        cooldownRanged.gameObject.SetActive(false);
        cooldownMelee.gameObject.SetActive(false);
        cooldownRText.gameObject.SetActive(false);
        cooldownMText.gameObject.SetActive(false);

        // set bar length
        healthBar.maxValue = maxHealth;
        cooldownMelee.maxValue = meleeMaxCooldown;
        cooldownRanged.maxValue = rangedMaxCooldown;
        switchBar.maxValue = switchTime;

        base.Start();
    }

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

        // 5. Update UI
        UpdateUI();

    }

    /// <summary>
    /// Updates timers and checks select timer code every frame
    /// </summary>
    protected override void Timer()
    {
        currentTimer += Time.deltaTime;
        switchText.text = $"Move: {movementControl.ToString()}\t\t\t\t" +
                          $"Attack: {attackControl.ToString()}";

        base.Timer();
    }

    

    /// <summary>
    /// Switches controls to different at certain intervals
    /// </summary>
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
        rb.MovePosition(position);
    }

    /// <summary>
    /// Player takes amount of damage, 
    /// if their health reaches lose game
    /// </summary>
    /// <param name="damage">damage taken</param>
    public override void TakeDamage(float damage)
    {
        
        currentHealth -= damage;
        healthBar.value = currentHealth;

        if (currentHealth < 0)
        {
            ChangeScene changeScene = new ChangeScene();
            changeScene.Change(3);
            // lose game
            
        }

    }

    /// <summary>
    /// Run corresponding attack to correct button press 
    /// </summary>
    protected override void Attack()
    {
        
        if (canAttack)
        {
            
            
            if (attackControl == ControlScheme.WASD)
            {
                if (Input.GetKey(KeyCode.A)) { RangedAttack(); cooldownRanged.gameObject.SetActive(true); cooldownRText.gameObject.SetActive(true); }
                else if (Input.GetKey(KeyCode.D)) { MeleeAttack(); cooldownMelee.gameObject.SetActive(true); cooldownMText.gameObject.SetActive(true); }


            }
            else if (attackControl == ControlScheme.JIKL)
            {
                if (Input.GetKey(KeyCode.J)) { RangedAttack(); cooldownRanged.gameObject.SetActive(true); cooldownRText.gameObject.SetActive(true); }
                else if (Input.GetKey(KeyCode.L)) { MeleeAttack(); cooldownMelee.gameObject.SetActive(true); cooldownMText.gameObject.SetActive(true); }

            }
            else if (attackControl == ControlScheme.Arrows)
            {
                if (Input.GetKey(KeyCode.LeftArrow)) { RangedAttack(); cooldownRanged.gameObject.SetActive(true); cooldownRText.gameObject.SetActive(true); }
                else if (Input.GetKey(KeyCode.RightArrow)) { MeleeAttack(); cooldownMelee.gameObject.SetActive(true); cooldownMText.gameObject.SetActive(true); }

            }
            

        }
    }

    /// <summary>
    /// Activate ranged attack if not on cooldown
    /// </summary>
    protected override void RangedAttack()
    {
        // 1. able to attack
        if (rangedCooldown <= 0)
        {
            
            
            // 2. rotate to facing direction and set position 
            Transform partentTrasform = rangedColliders[0].transform.parent;
            RotateAttack(partentTrasform);

            partentTrasform.position = this.position;

            // 3. turn on colliders and sprites 
            if(rangeUpgrade)
            {
                for (int i = 1; i < rangedColliders.Length; i++)
                {
                    rangedColliders[i].enabled = true;
                    rangedSprites[i].enabled = true;
                }
                rangedColliders[0].enabled = true;
                rangedSprites[0].enabled = true;
            }
            else
            {
                rangedColliders[0].enabled = true;
                rangedSprites[0].enabled = true;
            }

                // 4. reset cool down
                rangedCooldown = rangedMaxCooldown;
            canAttack = false;
        }
    }

    /// <summary>
    /// Activate melee attack if not on cooldown
    /// </summary>
    protected override void MeleeAttack()
    {
        // 1. able to attack
        if (meleeCooldown <= 0)
        {
            
            // 2. rotate to facing direction 
            Transform partentTrasform = meleeColliders[0].transform.parent;
            RotateAttack(partentTrasform);

            // turn on all colliders and sprites 
            if (meleeUpgrade)
            {
                for (int i = 1; i < meleeColliders.Length; i++)
                {
                    meleeColliders[i].enabled = true;
                    meleeSprites[i].enabled = true;
                }
            }
            else
            {
                meleeColliders[0].enabled = true;
                meleeSprites[0].enabled = true;
            }

                // reset cool down
                meleeCooldown = meleeMaxCooldown;
            canAttack = false;
        }

    }

    void UpdateUI()
    {
        // update cooldown bars
        cooldownMelee.value = meleeCooldown;
        cooldownRanged.value = rangedCooldown;
        switchBar.value = switchTime - currentTimer;

        if (rangedCooldown <= 0)
        {
            cooldownRanged.gameObject.SetActive(false);
            cooldownRText.gameObject.SetActive(false);
        }
        if (meleeCooldown <= 0)
        {
            cooldownMelee.gameObject.SetActive(false);
            cooldownMText.gameObject.SetActive(false);
        }
    }

}
