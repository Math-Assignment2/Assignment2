using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circleMass : MonoBehaviour
{
    private Vector3 velocity;
    private float mass = 15f; 
    private float gravity = -9.8f;

    public GameObject groundPlane; 

    void Update()
    {
        ApplyGravity();
        MoveObject();
        CheckCollisionWithGround();
    }

    void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime * mass;
    }

    void MoveObject()
    {
        transform.Translate(velocity * Time.deltaTime);
    }

    void CheckCollisionWithGround()
    {
            float groundHeight = groundPlane.transform.position.y;

            if (transform.position.y <= groundHeight)
            {
                transform.position = new Vector3(transform.position.x, groundHeight, transform.position.z);
                velocity.y = 0;
            }
    }
}


