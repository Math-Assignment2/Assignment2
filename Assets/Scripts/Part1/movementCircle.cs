using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class movementCircle : MonoBehaviour
{
    public float speed = 5.0f;
    void Update()
    {
        float dt = speed * Time.deltaTime;
        Vector3 up = transform.up;
        Vector3 forward = transform.right;

        if (Input.GetKey(KeyCode.I))
        {
            transform.position += up * dt;
        
        }

        if (Input.GetKey(KeyCode.K))
        {
            transform.position -= up * dt;
        
        }

        if (Input.GetKey(KeyCode.L))
        {
            transform.position += forward * dt;
        
        }

        if (Input.GetKey(KeyCode.J))
        {
            transform.position -= forward * dt;
        
        }
    }
}
