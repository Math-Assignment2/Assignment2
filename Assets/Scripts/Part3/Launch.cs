using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch : MonoBehaviour
{
    public GameObject circle;
    public GameObject capsule;
    public GameObject square;

    public float upwardForce = 10f; // Adjust this value to set the force applied upwards
    public float gravity = 0.5f;    // Adjust this value to set the downward acceleration due to gravity
    public float maxYPosition = 10f; // Adjust this value to set the maximum height of the object

    private Vector3 initialPosition;
    private bool isLaunched = false;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void OnMouseDown()
    {
        if (!isLaunched)
        {
            isLaunched = true;
            launch();
        }
    }

    private void launch()
    {
        float initialVelocity = Mathf.Sqrt(2f * upwardForce); // Initial velocity for an object projected upwards
        float timeToMaxHeight = initialVelocity / gravity; // Time to reach the maximum height
        float timeToGround = 2f * timeToMaxHeight; // Total time to reach the ground

        float totalTime = 0f;
        while (totalTime < timeToGround)
        {
            float newY = initialPosition.y + initialVelocity * totalTime - 0.5f * gravity * totalTime * totalTime;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            totalTime += Time.deltaTime;
        }

        isLaunched = false;
        transform.position = initialPosition;
    }
}
