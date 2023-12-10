using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch : MonoBehaviour
{
    private Vector3 position;
    private bool isClicked = false;

    private float initialVelocityY = 10f;
    public float mass = 10f;
    private float gravity = -9.8f;

    private float velocityY; 

    private GameObject plane;

    void Start()
    {
        position = transform.position;
        velocityY = initialVelocityY; 

        plane = GameObject.Find("Plane");
    }

    void Update()
    {
        if ((Input.GetMouseButtonDown(0)) || Input.GetKeyDown(KeyCode.Space))
        {
            isClicked = true;
        }

        if (isClicked)
        {
            ApplyImpulse();
        }
    }

    bool IsClicked()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject;
    }

    void ApplyImpulse()
    {
        float timeStep = Time.deltaTime;

        velocityY += gravity * timeStep;

        position.y += velocityY * timeStep + 0.5f * gravity * timeStep * timeStep * mass;

        if (position.y <= plane.transform.position.y)
        {
            position.y = plane.transform.position.y; 
            velocityY = 0f;
            isClicked = false; 
        }

        transform.position = position;
    }
}

