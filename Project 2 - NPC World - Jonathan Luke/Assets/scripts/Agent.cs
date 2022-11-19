using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PhysicsObject))]
public abstract class Agent : MonoBehaviour
{
    public PhysicsObject physicsObject;

    public float maxSpeed = 5f;

    public float maxForce = 5f;

    private float wanderAngle = 0f;

    public float maxWanderAngle = 45f;

    public float wanderPerSec = 10f;

    private Vector3 totalForce = Vector3.zero;

    public float personalSpace = 1f;

    public float visionRange = 2f;

    void Awake()
    {
        if (physicsObject == null)
        {
            physicsObject = GetComponent<PhysicsObject>();
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        CalculateSteeringForces();

        totalForce = Vector3.ClampMagnitude(totalForce, maxForce);
        physicsObject.ApplyForce(totalForce);
        totalForce = Vector3.zero;
    }

    protected abstract void CalculateSteeringForces();

    protected void Seek(Vector3 targetPos, float weight = 1f)
    {
        // Calculate the desired velocity
        Vector3 desiredVelocity = targetPos - physicsObject.Position;

        // Set desired velocity magnitude to max speed
        desiredVelocity = desiredVelocity.normalized * maxSpeed;

        // Calculate seek steering force
        Vector3 seekingForce = desiredVelocity - physicsObject.Velocity;

        // Apply seek steering force
        totalForce += seekingForce * weight;
    }

    protected void Flee(Vector3 targetPos, float weight = 1f)
    {
        // Calculate desired velocity 
        Vector3 desiredVelocity = physicsObject.Position - targetPos;

        // Set desired velocity magnitude to max speed
        desiredVelocity = desiredVelocity.normalized * maxSpeed;

        // Calculate flee steering force
        Vector3 fleeingForce = desiredVelocity - physicsObject.Velocity;

        // Apply flee steering force
        totalForce += fleeingForce * weight; 
    }

    protected void Wander(float weight = 1f)
    {
        // update angle of wander
        float wanderPerRealSec = wanderPerSec * Time.deltaTime;

        wanderAngle += Random.Range(-wanderPerRealSec, wanderPerRealSec);

        wanderAngle = Mathf.Clamp(wanderAngle,-maxWanderAngle,maxWanderAngle);

        // get a position that is defined by the wander angle
        Vector3 wanderTarget = Quaternion.Euler(0, 0, wanderAngle) * physicsObject.Direction.normalized + physicsObject.Position;

        // seek towards wander position
        Seek(wanderTarget,weight);
    }

    protected void StayInBounds(float weight = 1f)
    {
        Vector3 futurePosition = GetFuturePosition();
        if (futurePosition.x > AgentManager.Instance.maxPosition.x ||
            futurePosition.x < AgentManager.Instance.minPosition.x ||
            futurePosition.y > AgentManager.Instance.maxPosition.y ||
            futurePosition.y < AgentManager.Instance.minPosition.y)
        {
            Seek(Vector3.zero, weight);
        }
    }

    protected void Seperate<T>(List<T> agents) where T : Agent
    {
        float sqrPersonalSpace = Mathf.Pow(personalSpace, 2);

        // loop through other agents
        foreach (T other in agents)
        {
            // Find squared distance between the two
            float sqrDistance = Vector3.SqrMagnitude(other.physicsObject.transform.position - physicsObject.Position);

            if (sqrDistance < float.Epsilon)
            {
                continue;
            }

            if (sqrDistance < sqrPersonalSpace)
            {
                float weight = sqrPersonalSpace / (sqrDistance + 0.1f);
                Flee(other.physicsObject.Position, weight);
            }
        }
    }

    protected void AvoidObstacle(Obstacle obstacle)
    {
        // Get a vector from this agent to an obstacle
        Vector3 toObstacle = obstacle.Position - physicsObject.Position;

        // use that vector to check if obstacle is behind this agent
        float fwdToObstacleDot = Vector3.Dot(physicsObject.Direction, toObstacle);
        if (fwdToObstacleDot < 0)
        {
            return; // return early if behind
        }

        // check if the obstacle is too far to the left or right
        float rightToObstacleDot = Vector3.Dot(physicsObject.Right, toObstacle);

        if (Mathf.Abs(rightToObstacleDot) > physicsObject.radius + obstacle.radius)
        {
            return; // too far left or right
        }

        // check if it's within vision range
       

        if (fwdToObstacleDot > visionRange)
        {
            return; // not within range
        }

        // avoid the obstacle
        Vector3 desiredVelocity;

        if (rightToObstacleDot > 0)
        {
            // obstacle right? steer left
            desiredVelocity = physicsObject.Right * -maxSpeed;
        }
        else
        {
            // obstacle left? steer right
            desiredVelocity = physicsObject.Right * maxSpeed;
        }

        // Create weight based on how close to the obstacle
        float weight = visionRange / (fwdToObstacleDot + 0.1f);

        // calculate steering force
        Vector3 steeringForce = (desiredVelocity - physicsObject.Velocity) * weight;

        // apply the steering force
        totalForce += steeringForce;
    }

    protected void AvoidAllObstacles()
    {
        foreach (Obstacle obstacle in ObstacleManager.Instance.Obstacles)
        {
            AvoidObstacle(obstacle);
        }
    }

    public Vector3 GetFuturePosition(float timeAhead = 1f)
    {
        return physicsObject.Position + physicsObject.Velocity * timeAhead;
    }

    private void OnDrawGizmosSeleceted()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, physicsObject.radius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, personalSpace);
    }
}
