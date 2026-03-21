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

    ControlScheme movement;
    ControlScheme attack;

    private void FixedUpdate()
    {
        currentTimer += Time.deltaTime;

        // 1. check if player controls swap
        if (currentTimer >= switchTime)
        {
            SwitchControl();
        }

        // 2. move player
        Move();

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

    }

    protected override void Move()
    {
        position = rb.transform.position;
        velocity = Vector2.zero;

        if (movement == ControlScheme.WASD)
        {
            if(Input.GetKey(KeyCode.A)) position.x -= speed;
            else if(Input.GetKey(KeyCode.D)) position.x += speed;
            else if(Input.GetKeyUp(KeyCode.S)) position.y -= speed;
            else if(Input.GetKeyUp(KeyCode.W)) position.y += speed;
        }
        else if (movement == ControlScheme.JIKL)
        {
            if (Input.GetKey(KeyCode.J)) position.x -= speed;
            else if (Input.GetKey(KeyCode.L)) position.x += speed;
            else if (Input.GetKeyUp(KeyCode.K)) position.y -= speed;
            else if (Input.GetKeyUp(KeyCode.I)) position.y += speed;
        }

        else if (movement == ControlScheme.Arrows)
        {
            if (Input.GetKey(KeyCode.LeftArrow)) position.x -= speed;
            else if (Input.GetKey(KeyCode.RightArrow)) position.x += speed;
            else if (Input.GetKeyUp(KeyCode.DownArrow)) position.y -= speed;
            else if (Input.GetKeyUp(KeyCode.UpArrow)) position.y += speed;
        }
    }

    protected override void LongAttack()
    {
        if(longCoolDown >= longAttackSpeed)
        {
            // attack success 
        }
    }

    protected override void ShortAttack()
    {
        throw new System.NotImplementedException();
    }
}
