using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class squareMass : MonoBehaviour
{
    private Vector3 velocity;
    private float mass = 10f; // Mass of the object
    private float gravity = -9.8f;

    public GameObject groundPlane; // Reference to the ground plane GameObject

    void Update()
    {
        // Simulate gravity and movement
        ApplyGravity();
        MoveObject();
        CheckCollisionWithGround();
    }

    void ApplyGravity()
    {
        // Apply gravity proportional to mass
        velocity.y += gravity * Time.deltaTime * mass;
    }

    void MoveObject()
    {
        // Move the object based on its velocity
        transform.Translate(velocity * Time.deltaTime);
    }

    void CheckCollisionWithGround()
    {
        if (groundPlane != null)
        {
            // Get the y position of the ground plane
            float groundHeight = groundPlane.transform.position.y;

            if (transform.position.y <= groundHeight)
            {
                // Set object on the ground and stop further downward movement
                transform.position = new Vector3(transform.position.x, groundHeight, transform.position.z);
                velocity.y = 0;
            }
        }
        else
        {
            Debug.LogError("Ground plane not assigned!"); // Log an error if groundPlane GameObject is not assigned
        }
    }
}


