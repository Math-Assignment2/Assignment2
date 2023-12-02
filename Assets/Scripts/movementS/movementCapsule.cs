using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementCapsule : MonoBehaviour
{
    public float speed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float dt = speed * Time.deltaTime;
        Vector3 up = transform.up;
        Vector3 forward = transform.right;

        if (Input.GetKey(KeyCode.T))
        {
            transform.position += up * dt;
        
        }

        if (Input.GetKey(KeyCode.G))
        {
            transform.position -= up * dt;
        
        }

        if (Input.GetKey(KeyCode.H))
        {
            transform.position += forward * dt;
        
        }

        if (Input.GetKey(KeyCode.F))
        {
            transform.position -= forward * dt;
        
        }
    }
}
