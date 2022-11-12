using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    // Vectors
    private Vector3 velocity;
    private Vector3 acceleration;
    private Vector3 direction;

    public Vector3 Velocity 
    {
        get { return velocity; }
    }

    public Vector3 Direction
    {
        get { return direction; }
    }

    public Vector3 Position
    {
        get { return transform.position; }
    }

    private float gravityForce = 1f;

    public float mass = 1f;

    public float frictionCoeff = 0.2f;

    public float radius = 1f; 

    public bool bounceOnWalls = false;

    public bool useGravity = false;

    public bool useFriction = false;

    // Start is called before the first frame update
    void Start()
    {
        // Bounds 
        direction = Random.insideUnitCircle.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        // Apply Forces

        //Gravity
        if (useGravity)
        {
            Gravity(Physics.gravity);
        }
        
        //Friction
        if (useFriction)
        {
            ApplyFriction(frictionCoeff);
        }


        // Calculate Velocity based on acc
        velocity += acceleration * Time.deltaTime;

        // Calculate Position based on velocity
        transform.position += velocity * Time.deltaTime;

        if (velocity.sqrMagnitude > Mathf.Epsilon)
        {
            // Store direction
            direction = velocity.normalized;
        }
        

        transform.rotation = Quaternion.LookRotation(Vector3.back, direction);

        // Zero acc for next frame
        acceleration = Vector3.zero;
        if (bounceOnWalls)
        {
            WallBounce();
        }
    }

    /// <summary>
    /// Applies forces to an object with F=ma
    /// </summary>
    /// <param name="force">Vector to be applied to object</param>
    public void ApplyForce(Vector3 force)
    {
        acceleration += force / mass;
    }

    /// <summary>
    /// Applies a friction force to the obj
    /// </summary>
    /// <param name="coeff">coefficient of friction</param>
    private void ApplyFriction(float coeff)
    {
        Vector3 friction = velocity * -1;
        friction.Normalize();
        friction = friction * coeff;

        ApplyForce(friction);
    }

    private void Gravity(Vector3 gravity)
    {
        acceleration += gravity;
    }

    private void WallBounce()
    {
        // Reverse direction if hitting a bound
        if (transform.position.x > AgentManager.Instance.maxPosition.x && velocity.x > 0)
        {
            velocity.x *= -1f;
        }
        if (transform.position.x < AgentManager.Instance.minPosition.x && velocity.x < 0)
        {
            velocity.x *= -1f;
        }

        if (transform.position.y > AgentManager.Instance.maxPosition.y && velocity.y > 0)
        {
            velocity.y *= -1f;
        }
        if (transform.position.y < AgentManager.Instance.minPosition.y && velocity.y < 0)
        {
            velocity.y *= -1f;
        }
    }
}
