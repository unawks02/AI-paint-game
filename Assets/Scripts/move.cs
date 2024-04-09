using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{

    public float movementSpeed = 5.0f; // The number of units you want to move
    Vector2 movement = new Vector2();

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        Vector2 newPosition = new Vector2(movement.x * movementSpeed * Time.deltaTime, movement.y * movementSpeed * Time.deltaTime);
        transform.Translate(newPosition);
    }
}