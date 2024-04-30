using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{

    public float movementSpeed = 5.0f; // The number of units you want to move
    Vector2 movement = new Vector2();
    public Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetInteger("state", 0);
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        Vector2 newPosition = new Vector2(movement.x * movementSpeed * Time.deltaTime, movement.y * movementSpeed * Time.deltaTime);

        if (movement.x < 0)
        {
            //moving left
            if (anim.GetInteger("state") != 1)
            {
                anim.SetInteger("state", 1);
            }
        } else if (movement.x > 0)
        {
            //moving right
            if (anim.GetInteger("state") != 3)
            {
                anim.SetInteger("state", 3);
            }
        } else if (movement.y > 0)
        {
            //moving up screen
            if (anim.GetInteger("state") != 2)
            {
                anim.SetInteger("state", 2);
            }
        }
        else
        {
            if (anim.GetInteger("state") != 0)
            {
                anim.SetInteger("state", 0);
            }
        }
        transform.Translate(newPosition);
    }
}