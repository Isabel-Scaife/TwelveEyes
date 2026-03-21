using UnityEngine;
using UnityEngine.UIElements;

enum ControlScheme
{
    WASD,
    JIKL,
    Arrows
}

public class Player : Entity
{

    ControlScheme movement;
    ControlScheme attack;
    protected override void Move()
    {
        position = rb.position;

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
        throw new System.NotImplementedException();
    }

    protected override void LongAttack()
    {
        throw new System.NotImplementedException();
    }

    protected override void ShortAttack()
    {
        throw new System.NotImplementedException();
    }
}
